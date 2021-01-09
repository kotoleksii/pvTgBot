using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace pvTgBot.Services
{   
    public class PrivatCurrencyInfo
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public float buy { get; set; }
        public float sale { get; set; }
    }

    public class PrivatCurrencyAPI
    {
        private static HttpWebRequest httpWebRequest;
        private static HttpWebResponse httpWebResponse;
        private static StreamReader streamReader;

        public async static Task<string> GetPrivatExchangeRate()
        {
            string url = $@"https://api.privatbank.ua/p24api/pubinfo?json&exchange&coursid=5";

            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }         

            List<PrivatCurrencyInfo> myDeserializedObjList =
                (List<PrivatCurrencyInfo>)JsonConvert.DeserializeObject(response, typeof(List<PrivatCurrencyInfo>));


            string USD = myDeserializedObjList[0].ccy;
            string USrateBuy = Math.Round(float.Parse(myDeserializedObjList[0].buy.ToString()), 2).ToString();
            string USrateSell = Math.Round(float.Parse(myDeserializedObjList[0].sale.ToString()), 2).ToString();

            string EUR = myDeserializedObjList[1].ccy;
            string EUrateBuy = Math.Round(float.Parse(myDeserializedObjList[1].buy.ToString()), 2).ToString();
            string EUrateSell = Math.Round(float.Parse(myDeserializedObjList[1].sale.ToString()), 2).ToString();

            string BTC = myDeserializedObjList[3].ccy;
            string BTCrateBuy = Math.Round(float.Parse(myDeserializedObjList[3].buy.ToString())).ToString();
            string BTCrateSell = Math.Round(float.Parse(myDeserializedObjList[3].sale.ToString())).ToString();

            return
                $"🗓 {DateTime.Now.ToLongDateString()}\n\n" +
                $"Валюта     Купівля     Продаж\n" +
                $"🇺🇸{USD}       {USrateBuy}          {USrateSell}\n" +
                $"🇪🇺{EUR}       {EUrateBuy}          {EUrateSell}\n" +
                $"💹{BTC}       {BTCrateBuy}        {BTCrateSell}\n";
        }
    }

    
}
