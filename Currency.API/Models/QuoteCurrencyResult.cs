using System;
using System.Collections.Generic;

namespace Currency.API.Models
{
    public class QuoteCurrencyResult
    {
        public string Source { get; set; }
        public List<Quote> Quotes { get; set; }
    }
}
