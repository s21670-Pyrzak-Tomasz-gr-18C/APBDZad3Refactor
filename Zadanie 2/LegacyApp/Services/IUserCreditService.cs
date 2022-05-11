using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Services
{
    public interface IUserCreditService
    {
        int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth);
    }
}
