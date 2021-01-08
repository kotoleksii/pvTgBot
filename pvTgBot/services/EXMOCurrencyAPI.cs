using RestSharp;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace pvTgBot.Services
{
    public class EXMOCurrencyAPI
    {
        private static RestClient client;
        private static RestRequest request;

        public async static Task<string> GetExchangeDigitRateEXMO(string from, string to)
        {
            try
            {
                client = new RestClient("https://api.exmo.com/v1.1/required_amount");
                client.Timeout = -1;

                request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddParameter("pair", $"{from}_{to}");
                request.AddParameter("quantity", "1");

                IRestResponse response = await client.ExecuteAsync(request);
                string word = response.Content.Substring(response.Content.LastIndexOf(':') + 1).Trim(new Char[] { '}', '"' });

                return $"{from}: { Math.Round(Double.Parse(word, CultureInfo.InvariantCulture), 2)} $\n";
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine(httpRequestException.StackTrace);
                return "Error calling API. Please do manual lookup.";
            }
        }
    }
}
