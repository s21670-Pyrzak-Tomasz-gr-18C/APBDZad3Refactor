using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers
{
    public class VeryImportantClientCreditLimitProvider : ICreditLimitProvider
    {
        public string Name => "VeryImportantClient";

        public (bool hasCreditLimit, int creditLimit) GetCreditLimit(User user)
        {
            return (false, 0);
        }
    }
}
