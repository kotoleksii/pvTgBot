using System;
using System.Globalization;
using System.Xml;

namespace pvTgBot.Services
{
    public class NBUCurrencyAPI
    {
        private static XmlTextReader reader;

        public static string GetExchangeRateNBU()
        {
            decimal? usd = null, eur = null;
            using (reader = new XmlTextReader("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange"))
            {
                while (reader.ReadToFollowing("rate"))
                {
                    decimal rate = decimal.Parse(reader.ReadElementContentAsString(), CultureInfo.InvariantCulture);
                    if (reader.ReadToFollowing("cc"))
                    {
                        switch (reader.ReadElementContentAsString())
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
