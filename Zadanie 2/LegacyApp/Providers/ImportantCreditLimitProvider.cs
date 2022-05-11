using LegacyApp.Models;
using LegacyApp.Services;

namespace LegacyApp.Providers
{
    public class ImportantCreditLimitProvider : ICreditLimitProvider
    {
        private readonly IUserCreditService _userCreditService;

        public ImportantCreditLimitProvider(IUserCreditService userCreditService)
        {
            _userCreditService = userCreditService;
        }

        public string Name => "ImportantClient";

        public (bool hasCreditLimit, int creditLimit) GetCreditLimit(User user)
        {
            int creditLimit = _userCreditService.GetCreditLimit(user.FirstName, user.LastName, user.DateOfBirth);
            creditLimit *= 2;
            return (true, creditLimit);
        }
    }
}