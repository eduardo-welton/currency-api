using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Exceptions;
using Xunit;

namespace Currency.API.Tests
{
    public class CurrencyLayerProviderTest
    {
        [Fact]
        public async Task ConvertToBRL()
        {
            var currencyLayerProvider = new Currency.API.Providers.CurrencyLayerProvider();

            var currencySource = "GBP";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            var currencyResult = await currencyLayerProvider.GetCurrencyValues(currencySource, currencyQuotes);
            // TODO: Realizar as validações do teste
        }

        [Fact]
        public async Task UnknownCurrencySource()
        {
            var currencyLayerProvider = new Currency.API.Providers.CurrencyLayerProvider();

            var wrongCurrencySource = "EDU";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            QuoteException quoteException = await Assert.ThrowsAsync<QuoteException>(() => currencyLayerProvider.GetCurrencyValues(wrongCurrencySource, currencyQuotes));
            Assert.Equal(201, quoteException.ErrorCode);
        }
    }
}
