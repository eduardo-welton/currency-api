using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Exceptions;
using Currency.API.Providers;
using Xunit;

namespace Currency.API.Tests
{
    /*
     * ATENÇÃO: ESSA CLASSE TEM O OBJETIVO DE TESTAR O RETORNO DA API DA CURRENCYLAYER, SENDO ASSIM, 
     *          PROPOSITALMENTE NÃO ESTAMOS UTILIZANDO MOCK PARA OS TESTES ABAIXO
    */
    public class CurrencyLayerProviderTest
    {
        [Fact]
        public async Task ConvertToBRL()
        {
            var currencyLayerProvider = new CurrencyLayerProvider();

            var currencySource = "BRL";
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
            var currencyLayerProvider = new CurrencyLayerProvider();

            var wrongCurrencySource = "EDU";
            var currencyQuotes = new List<string>() { "USD", "AUD", "CAD", "PLN", "MXN" };

            QuoteException quoteException = await Assert.ThrowsAsync<QuoteException>(() => currencyLayerProvider.GetCurrencyValues(wrongCurrencySource, currencyQuotes));
            Assert.Equal(201, quoteException.ErrorCode);
        }

        [Fact]
        public async Task UnknownCurrencyTarget()
        {
            var currencyLayerProvider = new CurrencyLayerProvider();

            var currencySource = "BRL";
            var currencyQuotes = new List<string>() { "BlaBla" };

            QuoteException quoteException = await Assert.ThrowsAsync<QuoteException>(() => currencyLayerProvider.GetCurrencyValues(currencySource, currencyQuotes));
            Assert.Equal(202, quoteException.ErrorCode);
        }

    }
}
