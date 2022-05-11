using LegacyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers
{
    public interface ICreditLimitProvider
    {
        string Name { get; }
        (bool hasCreditLimit, int creditLimit) GetCreditLimit(User user);
    }
}
