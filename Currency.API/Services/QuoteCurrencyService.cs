using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Models;
using Currency.API.Providers;

namespace Currency.API.Services
{
    public class QuoteCurrencyService : IQuoteCurrencyService
    {
        public IQuoteProvider quoteProvider { get; }

        public QuoteCurrencyService(IQuoteProvider quoteProvider)
        {
            this.quoteProvider = quoteProvider;
        }

        private QuoteCurrencyResult ParseCurrencyLayerResult(CurrencyLayerResult currencyLayerResult)
        {
            var result = new QuoteCurrencyResult()
            {
                Quotes = new List<Quote>()
            };
            result.Source = currencyLayerResult.Source;

            foreach (KeyValuePair<string, double> quote in currencyLayerResult.Quotes)
            {
                var currencyName = quote.Key;
                currencyName = currencyName.Replace(result.Source, "");

                result.Quotes.Add(new Quote()
                {
                    CurrencyName = currencyName,
                    CurrencyValue = quote.Value
                });
            }

            return result;
        }

        public async Task<QuoteCurrencyResult> GetQuote(string sourceCurrency, List<string> destinationCurrencies)
        {
            CurrencyLayerResult currencyLayerResult = await quoteProvider.GetCurrencyValues(sourceCurrency, destinationCurrencies);
            var result = ParseCurrencyLayerResult(currencyLayerResult);
            return result;
        }
    }
}
