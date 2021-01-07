using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pvTgBot
{
    public class TemperatureInfo
    {
        public float Temp { get; set; }
        public float Temp_Min { get; set; }
        public float Temp_Max { get; set; }
        public int Humidity { get; set; }
        public float Feels_Like { get; set; }
        public int Pressure { get; set; }
    }

    public class WeatherInfo
    {
        public string Main { get; set; }
        public string Description { get; set; }
    }

    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public WeatherInfo[] Weather { get; set; }
        public string Name { get; set; }
    }

    public class WindInfo
    {
        public float Speed { get; set; }
        public string Direction { get; set; }
    }
}
