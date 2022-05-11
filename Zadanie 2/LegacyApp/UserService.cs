using System;
using LegacyApp.DataAccess;
using LegacyApp.Models;
using LegacyApp.Providers;
using LegacyApp.Repositories;
using LegacyApp.Services;
using LegacyApp.Validators;

namespace LegacyApp
{
    public class UserService
    {
        private readonly IClienRepository _clientRepository;
        private readonly IUserDataAccess _userDataAccess;
        private readonly UserValidator _userValidator;
        private readonly CreditLimitProviderFactory _creditLimitProviderFactory;

        public UserService(IClienRepository clientRepository, IUserDataAccess userDataAccess,UserValidator userValidator, CreditLimitProviderFactory creditLimitProviderFactory)
        {
            _clientRepository = clientRepository;
            _userDataAccess = userDataAccess;
            _userValidator = userValidator;
            _creditLimitProviderFactory = creditLimitProviderFactory;
        }

        public UserService()
         :this(new ClientRepository(), new UserDataAccessProxy(), new UserValidator(new CurrentTimeService()),
              new CreditLimitProviderFactory(new UserCreditService())) 
        {
        }
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!ValidateParameters(firstName, lastName, email, dateOfBirth)) return false;

            var client = _clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            var provider = _creditLimitProviderFactory.GetProviderByClientName(client.Name);
            var (hasCreditLimit, creditLimit) = provider.GetCreditLimit(user);
            user.HasCreditLimit = hasCreditLimit;
            user.CreditLimit = creditLimit;

            if (_userValidator.HasCreditLimitBelow500(user))
            {
                return false;
            }
            _userDataAccess.AddUser(user);
            return true;
        }

        private bool ValidateParameters(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (!_userValidator.HasValidFullName(firstName, lastName))
            {
                return false;
            }

            return _userValidator.HasValidEmail(email) && _userValidator.HasAtLeast21Years(dateOfBirth);
          
        }
    }
}
