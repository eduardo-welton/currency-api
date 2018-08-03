using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Exceptions;
using Currency.API.Services;
using Xunit;

namespace Currency.API.Tests
{
    public class CurrencyLayerServiceTests
    {
        [Fact]
        public async Task ParsedResultTest()
        {
            var currencyLayerService = new QuoteCurrencyService();

            var currencySource = "BRL";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            var quote = await currencyLayerService.GetQuote(currencySource, currencyQuotes);

            // TODO: Realizar as validações do teste
        }

    }
}
