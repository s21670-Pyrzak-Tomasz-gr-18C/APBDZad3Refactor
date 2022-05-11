using LegacyApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegacyApp.Providers
{
    public class CreditLimitProviderFactory
    {
        private const string DefaultCreditLimitProviderName = "DefaultClient";
        private readonly ReadOnlyDictionary<string, ICreditLimitProvider> _providers;

        public CreditLimitProviderFactory(UserCreditService userCreditService)
        {
            var veryImportantClientProvider = new VeryImportantClientCreditLimitProvider();
            var importandClientProvider = new ImportantCreditLimitProvider(userCreditService);
            var defaultProvider = new DefaultCreditLimitProvider(userCreditService);

            _providers = new ReadOnlyDictionary<string, ICreditLimitProvider>(
                new Dictionary<string, ICreditLimitProvider>()
                {
                    {veryImportantClientProvider.Name, veryImportantClientProvider},
                    {importandClientProvider.Name, importandClientProvider},
                    {defaultProvider.Name, defaultProvider},
                });
        }

        public ICreditLimitProvider GetProviderByClientName(string clientName)
        {
            return _providers.ContainsKey(clientName)
                ? _providers[clientName]
                : _providers[DefaultCreditLimitProviderName];
        }

    }
}
