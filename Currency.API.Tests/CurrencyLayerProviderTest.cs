using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Currency.API.Tests
{
    public class CurrencyLayerProviderTest
    {
        [Fact]
        public async Task ConvertToBRL()
        {
            //Console.WriteLine("asdfdasf");
            var currencyLayerProvider = new Currency.API.Providers.CurrencyLayerProvider();

            var currencySource = "GBP";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            var currencyResult =  await currencyLayerProvider.GetCurrencyValues(currencySource, currencyQuotes);
            Console.WriteLine(currencyResult);
        }
    }
}
