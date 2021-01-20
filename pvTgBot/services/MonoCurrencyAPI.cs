using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace pvTgBot.Services
{
    public enum MonoCurrencyCode
    {
        BYN = 933,
        CAD = 124,
        CHF = 756,
        CZK = 203,
        DKK = 208,
        EUR = 978,
        GBP = 826,
        HUF = 348,
        PLN = 985,
        RUB = 643,
        TRY = 949,
        UAH = 980,
        USD = 840
    }

    public class MonoCurrencyInfo
    {
        public int CurrencyCodeA { get; set; }
        public int CurrencyCodeB { get; set; }
        public float RateSell { get; set; }
        public float RateBuy { get; set; }
        public float RateCross { get; set; }
    }

    public class MonoCurrencyResponse
    {
        public MonoCurrencyInfo[] Currencies { get; set; }
    }

    public class MonoError
    {
        public string Description { get; set; }
    }

    public class MonoBankCurrencyAPI
    {
        private static HttpWebRequest _httpWebRequest;
        private static HttpWebResponse _httpWebResponse;
        private static StreamReader _streamReader;

        public async static Task<string> GetMonoExchangeRate()
        {
            string url = $@"https://api.monobank.ua/bank/currency";

            _httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            _httpWebResponse = (HttpWebResponse)_httpWebRequest.GetResponse();

            string response;

            using (_streamReader = new StreamReader(_httpWebResponse.GetResponseStream()))
            {
                response = await _streamReader.ReadToEndAsync();
            }

            List<MonoCurrencyInfo> myDeserializedObjList =
                (List<MonoCurrencyInfo>)JsonConvert.DeserializeObject(response, typeof(List<MonoCurrencyInfo>));

            string USrateBuy = Math.Round(float.Parse(myDeserializedObjList[0].RateBuy.ToString()), 2).ToString("0.00");
            string USrateSell = Math.Round(float.Parse(myDeserializedObjList[0].RateSell.ToString()), 2).ToString("0.00");

            string EUrateBuy = Math.Round(float.Parse(myDeserializedObjList[1].RateBuy.ToString()), 2).ToString("0.00");
            string EUrateSell = Math.Round(float.Parse(myDeserializedObjList[1].RateSell.ToString()), 2).ToString("0.00");

            string PLrateBuy = Math.Round(float.Parse(myDeserializedObjList[4].RateBuy.ToString()), 2).ToString("0.00");
            string PLrateSell = Math.Round(float.Parse(myDeserializedObjList[4].RateSell.ToString()), 2).ToString("0.00");

            return
                //$"💰Курс валют MonoBank\n" +
                $"🗓 {DateTime.Now.ToLongDateString()}\n\n" +
                $"Валюта     Купівля     Продаж\n" +
                $"🇺🇸{MonoCurrencyCode.USD}       {USrateBuy}          {USrateSell}\n" +
                $"🇪🇺{MonoCurrencyCode.EUR}       {EUrateBuy}          {EUrateSell}\n" +
                $"🇵🇱{MonoCurrencyCode.PLN}        {PLrateBuy}             {PLrateSell}\n";
        }
    }    
}
