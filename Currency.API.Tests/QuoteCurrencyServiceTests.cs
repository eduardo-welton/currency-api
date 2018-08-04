using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Providers;
using Currency.API.Services;
using NSubstitute;
using Xunit;

namespace Currency.API.Tests
{
    public class CurrencyLayerServiceTests
    {
        [Fact]
        public async Task ParsedResultTestOk()
        {
            var quoteProvider = Substitute.For<IQuoteProvider>();

            var currencySource = "BRL";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            var fakeResult = new CurrencyLayerResult()
            {
                Success = true,
                Terms = "https://currencylayer.com/terms",
                Privacy = "https://currencylayer.com/privacy",
                Timestamp = 1533403523,
                Source = currencySource,
                Quotes = new Dictionary<string, double> {
                        { "BRLUSD" , 1.300102 },
                        { "BRLAUD", 1.756633 },
                        { "BRLCAD", 1.691108 },
                        { "BRLPLN", 4.790361 },
                        { "BRLMXN", 24.124743 }
                    },
                Error = null
            };

            quoteProvider
                .GetCurrencyValues(currencySource, currencyQuotes)
                .Returns(fakeResult);

            var currencyLayerService = new QuoteCurrencyService(quoteProvider);
            var quoteResult = await currencyLayerService.GetQuote(currencySource, currencyQuotes);

            Assert.Equal(currencySource, quoteResult.Source);

            foreach (var quoteItem in quoteResult.Quotes)
            {
                var quoteName = $"{currencySource}{quoteItem.CurrencyName}";
                Assert.Equal(fakeResult.Quotes[quoteName], quoteItem.CurrencyValue);
            }
        }

    }
}
