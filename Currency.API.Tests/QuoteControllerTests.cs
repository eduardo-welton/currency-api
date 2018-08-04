using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Currency.API.Controllers;
using Currency.API.Exceptions;
using Currency.API.Models;
using Currency.API.Providers;
using Currency.API.Services;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Currency.API.Tests
{
    public class QuoteControllerTests
    {
        [Fact]
        public async void TestWithSuccess()
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
                        { "BRLUSD" , 0.269757 }
                },
                Error = null
            };

            quoteProvider
                .GetCurrencyValues(currencySource, currencyQuotes)
                .ReturnsForAnyArgs(fakeResult);

            var currencyLayerService = new QuoteCurrencyService(quoteProvider);
            var controller = new QuoteController(currencyLayerService);
            var controllerResult = await controller.GetQuotes(currencySource, "USD");

            Assert.IsType<OkObjectResult>(controllerResult);
            var quoteCurrencyResult = (QuoteCurrencyResult)((OkObjectResult)controllerResult).Value;

            Assert.Equal(currencySource, quoteCurrencyResult.Source);
            Assert.Single(quoteCurrencyResult.Quotes);
            Assert.Equal("USD", quoteCurrencyResult.Quotes[0].CurrencyName);
            Assert.Equal(0.269757, quoteCurrencyResult.Quotes[0].CurrencyValue);
        }

        [Fact]
        public async void TestWithException()
        {
            var quoteProvider = Substitute.For<IQuoteProvider>();

            quoteProvider
                .WhenForAnyArgs(x => x.GetCurrencyValues("", null))
                .Do(x =>
                {
                    throw new QuoteException(201, "You have supplied an invalid Source Currency. [Example: source=BRL]");
                });

            var currencySource = "HUEHUE";
            var currencyTarget = "USD";

            var currencyLayerService = new QuoteCurrencyService(quoteProvider);
            var controller = new QuoteController(currencyLayerService);
            var controllerResult = await controller.GetQuotes(currencySource, currencyTarget);

            Assert.IsType<ObjectResult>(controllerResult);

            var formattedControllerResult = ((ObjectResult)controllerResult);
            Assert.Equal(500, formattedControllerResult.StatusCode);

            var quoteException = (QuoteException) formattedControllerResult.Value;
            
            Assert.Equal(201, quoteException.ErrorCode);
            Assert.Equal("You have supplied an invalid Source Currency. [Example: source=BRL]", quoteException.Message);
        }

    }
}
