using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace pvTgBot.Services
{
    public enum CurrencyCode
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

    public class CurrencyInfo
    {
        public int CurrencyCodeA { get; set; }
        public int CurrencyCodeB { get; set; }
        public float RateSell { get; set; }
        public float RateBuy { get; set; }
        public float RateCross { get; set; }
    }

    public class CurrencyResponse
    {
        public CurrencyInfo[] Currencies { get; set; }      
    }

    public class Error
    {        
        public string Description { get; set; }
    }

    public class MonoBankCurrencyAPI
    {
        private static HttpWebRequest httpWebRequest;
        private static HttpWebResponse httpWebResponse;
        private static StreamReader streamReader;

        public async static Task<string> GetMonoExchangeRate()
        {
            string url = $@"https://api.monobank.ua/bank/currency";

            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }

            List<CurrencyInfo> myDeserializedObjList =
                (List<CurrencyInfo>)JsonConvert.DeserializeObject(response, typeof(List<CurrencyInfo>));

            string USrateBuy = Math.Round(float.Parse(myDeserializedObjList[0].RateBuy.ToString()), 2).ToString();
            string USrateSell = Math.Round(float.Parse(myDeserializedObjList[0].RateSell.ToString()), 2).ToString();

            string EUrateBuy = Math.Round(float.Parse(myDeserializedObjList[1].RateBuy.ToString()), 2).ToString();
            string EUrateSell = Math.Round(float.Parse(myDeserializedObjList[1].RateSell.ToString()), 2).ToString();

            string PLrateBuy = Math.Round(float.Parse(myDeserializedObjList[4].RateBuy.ToString()), 2).ToString();
            string PLrateSell = Math.Round(float.Parse(myDeserializedObjList[4].RateSell.ToString()), 2).ToString();

            return
                //$"💰Курс валют MonoBank\n" +
                $"🗓 {DateTime.Now.ToLongDateString()}\n\n" +
                $"Валюта     Купівля     Продаж\n" +
                $"🇺🇸{CurrencyCode.USD}       {USrateBuy}         {USrateSell}\n" +
                $"🇪🇺{CurrencyCode.EUR}       {EUrateBuy}         {EUrateSell}\n" +
                $"🇵🇱{CurrencyCode.PLN}       {PLrateBuy}            {PLrateSell}\n";
        }
    }
}
