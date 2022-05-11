using System;

namespace LegacyApp.Services
{
    public class UserCreditService : IDisposable, IUserCreditService

    {
        public void Dispose()
        {
            //...
        }

        public int GetCreditLimit(string firstName, string lastName, DateTime dateOfBirth)
        {
            //Fetching the data...
            return 10000;
        }
    }
}