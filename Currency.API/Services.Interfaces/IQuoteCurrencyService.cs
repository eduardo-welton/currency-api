using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Models;

namespace Currency.API.Services
{
    public interface IQuoteCurrencyService
    {
        Task<QuoteCurrencyResult> GetQuote(string sourceCurrency, List<string> destinationCurrencies);
    }
}