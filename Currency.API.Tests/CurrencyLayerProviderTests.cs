using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Controllers;
using Currency.API.Exceptions;
using Xunit;

namespace Currency.API.Tests
{
    public class CurrencyLayerProviderTest
    {
        [Fact]
        public async Task ConvertToBRL()
        {
            var currencyLayerProvider = new Providers.CurrencyLayerProvider();

            var currencySource = "GBP";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            var currencyResult = await currencyLayerProvider.GetCurrencyValues(currencySource, currencyQuotes);

            Assert.True(currencyResult.Success);
            Assert.Equal(currencySource, currencyResult.Source);
            Assert.Equal(currencyQuotes.Count, currencyResult.Quotes.Count);

            foreach (var currencyQuote in currencyQuotes)
            {
                var currencyQuoteKey = $"{currencySource}{currencyQuote}";
                Assert.True(currencyResult.Quotes.ContainsKey(currencyQuoteKey));
            }
        }

        [Fact]
        public async Task UnknownCurrencySource()
        {
            var currencyLayerProvider = new Providers.CurrencyLayerProvider();

            var wrongCurrencySource = "EDU";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            QuoteException quoteException = await Assert.ThrowsAsync<QuoteException>(() => currencyLayerProvider.GetCurrencyValues(wrongCurrencySource, currencyQuotes));
            Assert.Equal(201, quoteException.ErrorCode);
        }

        [Fact]
        public async Task UnknownCurrencySourceOnController()
        {
            var controller = new QuoteController(null);
            var result = await controller.Get("BRL", "USD");
            Console.WriteLine(result);
        }
    }
}
