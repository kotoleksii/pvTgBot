using System;
using System.Globalization;
using System.Xml;

namespace pvTgBot.Services
{
    public class NBUCurrencyAPI
    {
        private static XmlTextReader _reader;

        public static string GetExchangeRateNBU()
        {
            decimal? usd = null, eur = null;
            using (_reader = new XmlTextReader("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange"))
            {
                while (_reader.ReadToFollowing("rate"))
                {
                    decimal rate = decimal.Parse(_reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
                    if (_reader.ReadToFollowing("cc"))
                    {
                        switch (_reader.ReadElementContentAsString())
                        {
                            case "USD": usd = rate; break;
                            case "EUR": eur = rate; break;
                        }
                    }
                }
            }
            return $"💵USD: { Math.Round((decimal)usd, 2)} ₴\n💶EUR: { Math.Round((decimal)eur, 2)} ₴";
        }
    }
}
