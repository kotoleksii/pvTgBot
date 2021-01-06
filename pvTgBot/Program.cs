﻿using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Net.Http;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace pvTgBot
{
    class Program
    {
        private static TelegramBotClient _bot;
        private static System.Net.WebClient _wc;
        private static Random _rnd;

        static void Main(string[] args)
        {
            _wc = new System.Net.WebClient();
            _wc.Encoding = Encoding.UTF8;

            _rnd = new Random();

            _bot = new TelegramBotClient("1466263903:AAH11p2p6Ha3NB44GPkgN1_6fslGiz-8IJc");

            _bot.OnMessage += BotOnMessageReceived;
            //_bot.OnCallbackQuery += BotOnCallBackQueryReceived;
       
            var me = _bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            _bot.StartReceiving();
            Console.ReadLine();
            _bot.StopReceiving();                 
        }     

        private async static void BotOnMessageReceived(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {          
            string pictureUrl = "https://www.pragimtech.com/wp-content/uploads/2019/02/ado.jpg";

            var message = e.Message;

            if (message == null || message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";

            Console.WriteLine($"{name} send message: '{message.Text}'");

            switch (message.Text)
            {                          
                case "Start 🚀":
                case "🔙 Back":
                case "/start":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("📚 Homework"),
                            new KeyboardButton("📖 Materials")
                        },
                        new[] { new KeyboardButton("👨🏼‍💻 Classwork") },
                        new[] { new KeyboardButton("🚪 Exit") }
                        #region
                        //new []
                        //{
                        //    new KeyboardButton("picture"),
                        //    new KeyboardButton("Contact") { RequestContact = true},
                        //    new KeyboardButton("Geo"){ RequestLocation = true}
                        //},
#endregion
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"Привіт, {message.From.FirstName}👋\nЩо робитимемо? ⬇", replyMarkup: replyKeyboard);
                    break;
                case "/weather":
                    
                    await _bot.SendTextMessageAsync(message.Chat.Id, weather("Zaporizhia").Result);                 
                    break;
                case "👨🏼‍💻 Classwork":
                    var replyKeyboardCW = new ReplyKeyboardMarkup(new[]
                    {
                        new [] { new KeyboardButton("ADO.net") },
                        new [] { new KeyboardButton("🔙 Back") }                       
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "👨‍🏫", replyMarkup: replyKeyboardCW);                 
                    break;
                case "ADO.net":
                    var link1 = "https://github.com/itstep-org/itstep_pv912_ado_net/tree/master/20201201_intro";
                    var link2 = "https://github.com/itstep-org/itstep_pv912_ado_net/tree/master/20201201_intro";

                    var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                        new[] { InlineKeyboardButton.WithUrl("1️⃣ intro (01.12.2020)", link1) },
                        new[] { InlineKeyboardButton.WithUrl("2️⃣ detached (03.12.2020)", link2) }
                    });
                    await _bot.SendPhotoAsync(message.From.Id, pictureUrl, "Choose the lesson you need 👇", replyMarkup: inlineKeyboard);
                    break;
                case "📖 Materials":
                    var replyKeyboardEM = new ReplyKeyboardMarkup(new[]
                   {
                        new[] { new KeyboardButton("📗 ADO.net #2") },
                        new[] { new KeyboardButton("📗 ADO.net #1")},
                        new[] { new KeyboardButton("🔙 Back"),}
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Pump your skill! 💪", replyMarkup: replyKeyboardEM);
                    break;
                case "📗 ADO.net #1":
                    string bookLink1 = "https://drive.google.com/file/d/168N055TmxoJjQBLkahxwtc85hasWgoM8/view?usp=sharing";                 
                    newPostMystat(bookLink1, "1", pictureUrl, "", e);
                    break;
                case "📗 ADO.net #2":
                    string bookLink2 = "https://drive.google.com/file/d/168N055TmxoJjQBLkahxwtc85hasWgoM8/view?usp=sharing";
                    newPostMystat(bookLink2, "2", pictureUrl, "", e);                   
                    break;
                case "📚 Homework":
                    var replyKeyboardHomeWork = new ReplyKeyboardMarkup(new[]
                   {
                        new[] { new KeyboardButton("📄 Homework #2") },
                        new[] { new KeyboardButton("📄 Homework #1")},
                        new[] { new KeyboardButton("🔙 Back")}
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Good Luck! 👌", replyMarkup: replyKeyboardHomeWork);
                    break;
                case "📄 Homework #1":
                    string textLink1 = "https://fsx1.itstep.org/api/v1/files/67SquyJh0Lzhvwbo2V6a60AaFdswwxiw";
                    newPostMystat(textLink1, "", "", "", e);
                    break;
                case "📄 Homework #2":
                    string textLink2 = "https://fsx1.itstep.org/api/v1/files/924Db9acPdOya-65NwQk71c0sNyfyh_3";
                    newPostMystat(textLink2, "", pictureUrl, "", e);
                    break;
                case "🚪 Exit":
                    var replyKeyboardStart = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("Start 🚀") },
                    }, true);
                    var me = _bot.GetMeAsync().Result;
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"🤖{me.FirstName} Вітає!\nДля початку натисніть Start 🚀", replyMarkup: replyKeyboardStart);
                    break;
                case "/kurs":
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, newPostExchangeRates());
                    break;
                #region
                //case "/time":
                //    var response = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                //    await _bot.SendTextMessageAsync(e.Message.Chat.Id, response);
                //    break; 
                #endregion
                default:                                   
                    await _bot.SendStickerAsync(message.Chat.Id, stickersErr());          
                    break;
            }
        }

        public async static Task<string> weather(string city)
        {
            string url = $@"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid=c9beb94d133fd77596790d7f7d1c3fcf&lang=ua";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            string response;

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                response = await streamReader.ReadToEndAsync();
            }

            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(response);

            string smile = "";
            if (weather.Weather[0].Description == "рвані хмари")
                smile = "☁";

            return 
                $"{smile} {weather.Name} - {weather.Weather[0].Description}\n" +        
                $"🌡️ {weather.Main.Temp} °C\n\n" +
                $"Відчуття: {weather.Main.Feels_Like} °C\n" +
                $"Вітер: {weather.Wind.Speed} м/с\n" +
                $"Вологість: {weather.Main.Humidity} %\n" +
                $"Тиск: {weather.Main.Pressure} hPa\n" +
                $"{DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()}\n";
        }       

        public static string newPostExchangeRates()
        {
            return $"📊 Актуальні курси валют:\n\n{GetExchangeRate()}\n\n" +
                        $"{GetExchangeDigitRate("BTC", "USD").Result}" +
                            $"{GetExchangeDigitRate("ETH", "USD").Result}" +
                                $"{GetExchangeDigitRate("LTC", "USD").Result}" +
                            $"{GetExchangeDigitRate("ZEC", "USD").Result}" +
                        $"{GetExchangeDigitRate("XRP", "USD").Result}";
        }

        public static string GetExchangeRate()
        {
            decimal? usd = null, eur = null;
            using (XmlTextReader reader = new XmlTextReader("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange"))
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

        public async static Task<string> GetExchangeDigitRate(string from, string to)
        {
            try
            {
                var client = new RestClient("https://api.exmo.com/v1.1/required_amount");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
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

        private async static void newPostMystat(string link, string numberBook, string pictureLink, string filePath, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (numberBook != string.Empty/*&& filePath == string.Empty*/)
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                    InlineKeyboardButton.WithUrl($"Open ADO.net book #{numberBook}", link)
                    });

                if (pictureLink == String.Empty)
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, ". . .", replyMarkup: inlineKeyboard);
                else
                    await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, replyMarkup: inlineKeyboard);
            }
            else
            {
                string fileName = "";

                if (link != String.Empty)
                    fileName = _wc.DownloadString(link);

                if (filePath != String.Empty)
                    await writeFileAsync(filePath, fileName);

                var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                    InlineKeyboardButton.WithUrl($"{GetFilenameFromWebServer(link)}", link)
                    });

                if (pictureLink == String.Empty)
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, fileName, replyMarkup: inlineKeyboard);
                else
                    await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, fileName, replyMarkup: inlineKeyboard);
            }
            
        }           

        private static string GetFilenameFromWebServer(string url)
        {
            string result = "";

            var req = System.Net.WebRequest.Create(url);
            req.Method = "HEAD";
            using (System.Net.WebResponse resp = req.GetResponse())
            {
                if (!string.IsNullOrEmpty(resp.Headers["Content-Disposition"]))
                {
                    result = resp.Headers["Content-Disposition"].Substring(resp.Headers["Content-Disposition"].IndexOf("filename=") + 9).Replace("\"", "");
                }
            }

            return result;
        }

        private static async Task writeFileAsync(string path, string fileName)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                await sw.WriteLineAsync(fileName);
            }
        }

        private static string stickersErr()
        {
            string[] stickers = new string[] { "CAACAgIAAxkBAAKe11_wucfsL89gl0A3dmym3ifNsdpJAALIjQACY4tGDHjHpsKiUIbZHgQ",
                        "CAACAgIAAxkBAAKe2l_wuqLp7KrmkHrLffW1AamtC1b5AAKFEQACPLPFB9QtAAFHuClv0x4E",
                    "CAACAgIAAxkBAAKe3V_wvT_0SoSQXsorY1z71DllxJX2AAKQEQACPLPFB_LLZcaKbmyGHgQ",
                    "CAACAgIAAxkBAAKe4F_wvXckhsdicvTp52ake9PSL-IzAAK0jQACY4tGDB_-KlQJwUFhHgQ",
                    "CAACAgIAAxkBAAKe41_wva3XnNWfUUY4qpAr1TB2sn_qAAIDjgACY4tGDLVBmwhgjPuuHgQ"
                    };
            int i = _rnd.Next(0, stickers.Length);
            return stickers[i];
        }

        //private async static void BotOnCallBackQueryReceived(object sender, CallbackQueryEventArgs e)
        //{
        //    string btnText = e.CallbackQuery.Data;
        //    string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";

        //    Console.WriteLine($"{name} send message: '{btnText}'");

        //    try
        //    {
        //        if (btnText == "picture")
        //        {
        //            await _bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "https://www.imgonline.com.ua/examples/bee-on-daisy.jpg");

        //        }
        //        else if (btnText == "video")
        //        {
        //            await _bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "https://www.youtube.com/watch?v=91JOUGJBTac");
        //        }

        //        await _bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"You press button {btnText}");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}
        //private static async Task<string> readFileAsync(string fileName)
        //{
        //    using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open))
        //    {
        //        byte[] data = new byte[fs.Length];
        //        await fs.ReadAsync(data, 0, (int)fs.Length);
        //        string str = Encoding.UTF8.GetString(data);
        //        return str;
        //    }
        //}
        //private async static void bookEntry(string bookLink, int numberBook, string pictureLink, MessageEventArgs e)
        //{
        //    var inlineKeyboard = new InlineKeyboardMarkup(new[] {
        //            InlineKeyboardButton.WithUrl($"Open ADO.net book #{numberBook.ToString()}", bookLink)
        //            });

        //    if (pictureLink == String.Empty)
        //        await _bot.SendTextMessageAsync(e.Message.Chat.Id, ". . .", replyMarkup: inlineKeyboard);
        //    else
        //        await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, replyMarkup: inlineKeyboard);
        //}
        //public async static Task<string> GetExchangeRateCrypto(string from, string to)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        try
        //        {
        //            client.BaseAddress = new Uri(" https://free.currconv.com");
        //            var response = await client.GetAsync($"api/v7/convert?q={from}_{to}&compact=ultra&apiKey=1ea33486692fa74acc21");

        //            var stringResult = await response.Content.ReadAsStringAsync();

        //            string word = stringResult.Substring(stringResult.LastIndexOf(':') + 1).Trim('}');

        //            return $"{from}: {Math.Round(Double.Parse(word, CultureInfo.InvariantCulture))} $";
        //        }
        //        catch (HttpRequestException httpRequestException)
        //        {
        //            Console.WriteLine(httpRequestException.StackTrace);
        //            return "Error calling API. Please do manual lookup.";
        //        }
        //    }
        //}
    }
}