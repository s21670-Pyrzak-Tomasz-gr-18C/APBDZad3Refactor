using LegacyApp.Models;
using LegacyApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers
{
    public class DefaultCreditLimitProvider : ICreditLimitProvider
    {
        private IUserCreditService _userCreditService;

        public DefaultCreditLimitProvider(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }
        public string Name => "DefaultClient";

        public (bool hasCreditLimit, int creditLimit) GetCreditLimit(User user)
        {
            var creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
            return (true, creditLimit);
        }
    }
}
