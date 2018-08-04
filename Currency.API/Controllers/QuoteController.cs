using System;
using System.Collections.Generic;
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

        public QuoteController(IQuoteCurrencyService quoteCurrencyService)
        {
            this.quoteCurrencyService = quoteCurrencyService;
        }

        [HttpGet("{sourceCurrency}/{destinationCurrency}")]
        [ProducesResponseType(typeof(QuoteCurrencyResult), 200)]
        [ProducesResponseType(typeof(Exception), 500)]
        public async Task<IActionResult> GetQuotes(string sourceCurrency, string destinationCurrency)
        {
            var destionationCurrencyList = new List<string>() {
                destinationCurrency
            };

            try
            {
                QuoteCurrencyResult result = await quoteCurrencyService.GetQuote(sourceCurrency, destionationCurrencyList);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception);
            }
        }
    }
}
