using System.Collections.Generic;
using System.Threading.Tasks;

namespace Currency.API.Providers
{
    public interface IQuoteProvider
    {
        Task<CurrencyLayerResult> GetCurrencyValues(string currencySource, List<string> currencyQuotes);
    }
}