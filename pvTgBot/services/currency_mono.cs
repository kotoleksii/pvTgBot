using System;
using System.Collections.Generic;

namespace pvTgBot
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
}
