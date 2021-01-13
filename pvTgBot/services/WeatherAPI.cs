using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace pvTgBot.Services
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

    public class WeatherAPI
    {
        private static HttpWebRequest _httpWebRequest;
        private static HttpWebResponse _httpWebResponse;
        private static StreamReader _streamReader;       

        public async static Task<string> GetWeather(string city)
        {
            string url = $@"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=c9beb94d133fd77596790d7f7d1c3fcf&lang=ua";

            _httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            _httpWebResponse = (HttpWebResponse)_httpWebRequest.GetResponse();

            string response;

            using (_streamReader = new StreamReader(_httpWebResponse.GetResponseStream()))
            {
                response = await _streamReader.ReadToEndAsync();
            }

            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(response);

            string smile = "🏙";

            if (weather.Weather[0].Description == "рвані хмари" || weather.Weather[0].Description == "хмарно" ||
                weather.Weather[0].Description == "уривчасті хмари")
                smile = "☁";
            if (weather.Weather[0].Description == "туман")
                smile = "🌁";
            if (weather.Weather[0].Description == "легка злива" || weather.Weather[0].Description == "слабка мряка" ||
                weather.Weather[0].Description == "легкий дощ" || weather.Weather[0].Description == "помірний дощ")
                smile = "🌧";
            if (weather.Weather[0].Description == "чисте небо")
                smile = "☀";
            if (weather.Weather[0].Description == "димка")
                smile = "🌫";


            return
                $"{smile} {weather.Name} | {weather.Weather[0].Description}\n\n" +
                $"🌡 {weather.Main.Temp}° " +
                $"(відчувається як {weather.Main.Feels_Like}°)\n" +
                $"🌬 {weather.Wind.Speed} м/с\n" +
                $"💧 {weather.Main.Humidity} %\n" +
                $"🧘🏻‍♂️ {weather.Main.Pressure} hPa\n\n" +
                $"🗓 {DateTime.Now.ToLongDateString() + " | " + DateTime.Now.ToLongTimeString()}\n";
        }
    }
}
