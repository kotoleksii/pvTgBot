using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Linq;
using pvTgBot.Services;

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
            string logs = $"{name} send message: '{message.Text}'";

            Console.WriteLine(logs);

            File.AppendAllText("logs.txt", logs + "\n");


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
                case "👨🏼‍💻 Classwork":
                    //var replyKeyboardCW = new ReplyKeyboardMarkup(new[]
                    //{
                    //    new [] { new KeyboardButton("ADO.net") },
                    //    new [] { new KeyboardButton("🔙 Back") }                       
                    //}, true);
                    //await _bot.SendTextMessageAsync(message.Chat.Id, "👨‍🏫", replyMarkup: replyKeyboardCW);  

                    adoNETcwCase(pictureUrl, e);
                    break;
                case "ADO.net":
                    adoNETcwCase(pictureUrl, e);
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
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"🤖{me.FirstName} Вітає!\nДля початку натисніть Start 🚀",
                        replyMarkup: replyKeyboardStart);
                    break;
                case "/kurs":                  
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, newPostExchangeRates());
                    break;
                case "/mono":
                    monoCase(e);                   
                    break;
                case "/privat":
                    string privatLink = "https://privatbank.ua/uploads/media/default/0001/14/a6507601ef7e311f4d5af21ea9b8e0ce69105850.png";
                    await _bot.SendPhotoAsync(e.Message.Chat.Id, privatLink,
                            PrivatCurrencyAPI.GetPrivatExchangeRate().Result + "\n👇🏻 підтримати автора бота\n📑 4149 4991 1185 5175");
                    break;
                case "/weather":                  
                    await _bot.SendTextMessageAsync(message.Chat.Id, WeatherAPI.GetWeather("Zaporizhia").Result);
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

        private async static void adoNETcwCase(string pictureUrl,Telegram.Bot.Args.MessageEventArgs e)
        {
            var link1 = "https://github.com/itstep-org/itstep_pv912_ado_net/tree/master/20201201_intro";
            var link2 = "https://github.com/itstep-org/itstep_pv912_ado_net/tree/master/20201201_intro";

            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                new[] { InlineKeyboardButton.WithUrl("1️⃣ intro (01.12.2020)", link1) },
                new[] { InlineKeyboardButton.WithUrl("2️⃣ detached (03.12.2020)", link2) }
            });
            await _bot.SendPhotoAsync(e.Message.From.Id, pictureUrl, "Choose the lesson you need 👇", replyMarkup: inlineKeyboard);
        }

        private async static void monoCase(Telegram.Bot.Args.MessageEventArgs e)
        {
            var maxRetryAttempts = 3;           

            try
            {
                await RetryHelper.RetryOnExceptionAsync<HttpRequestException>
                    (maxRetryAttempts, async () =>
                    {
                        string monoLink = "https://psm7.com/awards-2020/wp-content/uploads/2020/11/1604584696-image-480x230-c-default.png";
                        string monoRef = "https://monobank.ua/r/GsbX";
                        string monoDonate = "send.monobank.ua/jar/5JfMjg4P5K";
                        var inlineKeyboardMono = new InlineKeyboardMarkup(new[] {
                                    new[] { InlineKeyboardButton.WithUrl("💳 отримати картку в 2 кліки", monoRef) },
                                    new[] { InlineKeyboardButton.WithUrl("🐈 підтримати автора бота", monoDonate)}
                        });
                        await _bot.SendPhotoAsync(e.Message.Chat.Id, monoLink,
                            MonoBankCurrencyAPI.GetMonoExchangeRate().Result, replyMarkup: inlineKeyboardMono);
                        //await _bot.SendTextMessageAsync(e.Message.Chat.Id, mono().Result, replyMarkup: inlineKeyboardMono);
                    });
            }
            catch (Exception ex)
            {
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, "📡 Між запитами необхідно трохи зачекати," +
                    " така вимога сервера. Спробуйте пізніше 🤷🏻‍♂️");
                Console.WriteLine("Exception: " + ex.Message);
                File.AppendAllText("logs.txt", "Exception: " + ex.Message + "\n");
            }
        }       
        
        public static string newPostExchangeRates()
        {
            return $"📊 Актуальні курси валют:\n\nНБУ\n{NBUCurrencyAPI.GetExchangeRateNBU()}\n\n" +
                $"/mono  MonoBank\n" +
                $"/privat  ПриватБанк\n\n" +
                        $"EXMO\n{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("BTC", "USD").Result}" +
                            $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("ETH", "USD").Result}" +
                                $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("LTC", "USD").Result}" +
                            $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("ZEC", "USD").Result}" +
                        $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("XRP", "USD").Result}";
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
            string[] stickers = new string[] { 
                        "CAACAgIAAxkBAAKe2l_wuqLp7KrmkHrLffW1AamtC1b5AAKFEQACPLPFB9QtAAFHuClv0x4E",                  
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
        public static class RetryHelper
        {
            public static async Task RetryOnExceptionAsync(int maxRetryAttempts, Func<Task> operation)
            {
                await RetryOnExceptionAsync<Exception>(maxRetryAttempts, operation);
            }

            public static async Task RetryOnExceptionAsync<TException>(int maxRetryAttempts, Func<Task> operation) where TException : Exception
            {
                if (maxRetryAttempts <= 0)
                    throw new ArgumentOutOfRangeException(nameof(maxRetryAttempts));

                var retryattempts = 0;
                do
                {
                    try
                    {
                        retryattempts++;
                        await operation();
                        break;
                    }
                    catch (TException ex)
                    {
                        if (retryattempts == maxRetryAttempts)
                            throw;

                        await CreateRetryDelayForException(maxRetryAttempts, retryattempts, ex);
                    }
                } while (true);
            }

            private static Task CreateRetryDelayForException(int maxRetryAttempts, int attempts, Exception ex)
            {
                int delay = IncreasingDelayInSeconds(attempts);
                Console.WriteLine("Attempt {0} of {1} failed. New retry after {2} seconds.", attempts.ToString(), maxRetryAttempts.ToString(), delay.ToString());
                return Task.Delay(delay);
            }

            internal static int[] DelayPerAttemptInSeconds =
            {
            (int) TimeSpan.FromSeconds(5).TotalSeconds,
            (int) TimeSpan.FromSeconds(30).TotalSeconds,
            (int) TimeSpan.FromMinutes(3).TotalSeconds,
            (int) TimeSpan.FromMinutes(10).TotalSeconds,
            (int) TimeSpan.FromMinutes(30).TotalSeconds
        };

            static int IncreasingDelayInSeconds(int failedAttempts)
            {
                if (failedAttempts <= 0) throw new ArgumentOutOfRangeException();

                return failedAttempts >= DelayPerAttemptInSeconds.Length ? DelayPerAttemptInSeconds.Last() : DelayPerAttemptInSeconds[failedAttempts];
            }
        }
    }
}