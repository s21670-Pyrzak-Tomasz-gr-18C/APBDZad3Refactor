using System;
using LegacyApp.DataAccess;
using LegacyApp.Models;
using LegacyApp.Repositories;
using LegacyApp.Services;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClienRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;
        private readonly ICurrentTimeService _currentTimeService;
        private readonly IUserDataAccess _userDataAccess;

        public UserService(IClienRepository clientRepository, IUserCreditService userCreditService, ICurrentTimeService currentTimeService, IUserDataAccess userDataAccess)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
            _currentTimeService = currentTimeService;
            _userDataAccess = userDataAccess;
        }

        public UserService()
         :this(new ClientRepository(), new UserCreditService(), new CurrentTimeService(), new UserDataAccessProxy()) 
        {
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (!email.Contains("@") && !email.Contains("."))
            {
                return false;
            }

            var now = _currentTimeService.GetCurrentDateTme();
            int age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            if (age < 21)
            {
                return false;
            }

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

             if (client.Name == "VeryImportantClient")
            {
                //Skip credit limit
                user.HasCreditLimit = false;
            }
            else if (client.Name == "ImportantClient")
            {
                int creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;    
            }
            else
            {
                //Do credit check
                user.HasCreditLimit = true;
                int creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;   
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }
            _userDataAccess.AddUser(user);
            return true;
        }
    }
}
