using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LegacyApp.Models;
using LegacyApp.Services;

namespace LegacyApp.Validators
{
    public class UserValidator

    {
        private const int MinAge = 21;
        private const int MinCreditLimit = 500;  
        private readonly ICurrentTimeService _currentTimeService;

        public UserValidator(ICurrentTimeService currentTimeService)
        {
            _currentTimeService = currentTimeService;
        }
        public bool HasValidFullName(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        public bool HasValidEmail(string email)
        {
            return email.Contains("@") || email.Contains(".");
        }

        public bool HasAtLeast21Years(DateTime dateOfBirth)
        {
            var now = _currentTimeService.GetCurrentDateTme();
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
           return age >= MinAge;
        }

        public bool HasCreditLimitBelow500(User user)
        {
            return user.HasCreditLimit && user.CreditLimit < MinCreditLimit;
        }
    }
}
