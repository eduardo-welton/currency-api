using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Flurl.Http;

namespace Currency.API.Providers
{
    public class CurrencyLayerProvider
    {
        public async Task<CurrencyLayerResult> GetCurrencyValues(string currencySource, List<string> currencyQuotes)
        {
            var accessKey = "0000642ea2bb13d939b969fdcd56f240";

            var source = currencySource;
            var currencies = string.Join(",", currencyQuotes);

            var apiUrl = $"http://www.apilayer.net/api/live?access_key={accessKey}&source={source}&currencies={currencies}&format=1";
            var result = await apiUrl.GetJsonAsync<CurrencyLayerResult>();

            return result;
        }
    }
}
