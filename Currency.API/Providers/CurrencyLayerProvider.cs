using System;
using Flurl.Http;

namespace Currency.API.Providers
{
    public class CurrencyLayerProvider
    {
        async public void aaa()
        {
            var accessKey = "0000642ea2bb13d939b969fdcd56f240";
            var source="GBP";
            var currencies="USD,AUD,CAD,PLN,MXN";
            var apiUrl = $"http://www.apilayer.net/api/live?access_key={accessKey}&source={source}&currencies={currencies}&format=1";

            var result = await apiUrl.GetJsonAsync<Object>();
        }
    }
}
