using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Currency.API.Models;
using Currency.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Currency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteCurrencyService quoteCurrencyService;

        public QuoteController(IQuoteCurrencyService quoteCurrencyService) {
            this.quoteCurrencyService = quoteCurrencyService;
        }

        [HttpGet("{sourceCurrency}/{destinationCurrency}")]
        public async Task<QuoteCurrencyResult> Get(string sourceCurrency, string destinationCurrency)
        {
            var quoteCurrencyService = new QuoteCurrencyService();
            var destionationCurrencyList = new List<string>() {
                destinationCurrency
            };

            QuoteCurrencyResult result = await quoteCurrencyService.GetQuote(sourceCurrency, destionationCurrencyList);
            return result;
        }
    }
}
