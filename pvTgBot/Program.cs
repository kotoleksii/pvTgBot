using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using File = Telegram.Bot.Types.File;
using System.Collections.Generic;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using static System.Net.WebRequestMethods;
using Autofac.Core;

namespace pvTgBot
{
    class Program
    {
        static TelegramBotClient _bot;
        static System.Net.WebClient _wc;
        static Random _rnd;      

        static void Main(string[] args)
        {
            _wc = new System.Net.WebClient();
            _wc.Encoding = Encoding.UTF8;

            _rnd = new Random();

            _bot = new TelegramBotClient("1466263903:AAH11p2p6Ha3NB44GPkgN1_6fslGiz-8IJc");

            _bot.OnMessage += BotOnMessageReceived;
            _bot.OnCallbackQuery += BotOnCallBackQueryReceived;

            var me = _bot.GetMeAsync().Result;

            Console.WriteLine(me.FirstName);

            _bot.StartReceiving();
            Console.ReadLine();
            _bot.StopReceiving();                 
        }

        static async Task writeFileAsync(string path,string fileName)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                await sw.WriteLineAsync(fileName); 
            }
        }

        static async Task<string> readFileAsync(string fileName)
        {
            using (FileStream fs = System.IO.File.Open(fileName, FileMode.Open))
            {
                byte[] data = new byte[fs.Length];
                await fs.ReadAsync(data, 0, (int)fs.Length);
                string str = Encoding.UTF8.GetString(data);
                return str;
            }
        }

        private async static void BotOnCallBackQueryReceived(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            string btnText = e.CallbackQuery.Data;
            string name = $"{e.CallbackQuery.From.FirstName} {e.CallbackQuery.From.LastName}";

            Console.WriteLine($"{name} send message: '{btnText}'");

            try
            {               
                if (btnText == "picture")
                {
                    await _bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "https://www.imgonline.com.ua/examples/bee-on-daisy.jpg");
                    
                }
                else if (btnText == "video")
                {
                    await _bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "https://www.youtube.com/watch?v=91JOUGJBTac");
                }     

                await _bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id, $"You press button {btnText}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async static void BotOnMessageReceived(object sender, MessageEventArgs e)
        {
            string pictureUrl = "https://www.pragimtech.com/wp-content/uploads/2019/02/ado.jpg";

            var message = e.Message;

            if (message == null || message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";

            Console.WriteLine($"{name} send message: '{message.Text}'");
        
            switch(message.Text)
            {
                #region
                //                case "/start":
                //                    string text =
                //@"Перелік команд:
                ///start - запуск бота
                ///inline - показати меню
                ///keyboard - вивести клавіатуру";
                //                    await _bot.SendTextMessageAsync(message.From.Id, text);

                //                    break;
                //case "/inline":
                //    var inlineKeyboard = new InlineKeyboardMarkup(new[] {     
                //        new[]
                //        {
                //            InlineKeyboardButton.WithUrl("fb", "fb.com"),
                //            InlineKeyboardButton.WithUrl("instagram", "instagram.com")
                //        },
                //        new[]
                //        {
                //            InlineKeyboardButton.WithCallbackData("picture"),
                //            InlineKeyboardButton.WithCallbackData("video")
                //        }
                //        });
                //    await _bot.SendTextMessageAsync(message.From.Id, "Choose menu button", replyMarkup: inlineKeyboard);                  
                //    break;
                #endregion
                case "🚪 Exit":
                    var replyKeyboardStart = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("Start 🚀")
                        },                
                    }, true);
                    var me = _bot.GetMeAsync().Result;
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"🤖{me.FirstName} Вітає!\nДля початку натисніть Start 🚀", replyMarkup: replyKeyboardStart);
                    break;
                case "Start 🚀":
                case "🔙 Back":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("📚 Homework"),
                            new KeyboardButton("📖 Educational Materials")
                        },
                        new[]
                        {
                            new KeyboardButton("🚪 Exit")
                        },
                        #region
                        //new []
                        //{
                        //    new KeyboardButton("picture"),
                        //    new KeyboardButton("Contact") { RequestContact = true},
                        //    new KeyboardButton("Geo"){ RequestLocation = true}
                        //},
#endregion
                    }, true) ;                    
                    await _bot.SendTextMessageAsync(message.Chat.Id, $"Привіт, {message.From.FirstName}👋\nЩо робитимемо? ⬇", replyMarkup: replyKeyboard);                   
                    break;
                case "📖 Educational Materials":
                    var replyKeyboardEM = new ReplyKeyboardMarkup(new[]
                   {
                        new[] { new KeyboardButton("📗 ADO.net #2") },
                        new[] { new KeyboardButton("📗 ADO.net #1"),},
                        new[] { new KeyboardButton("🔙 Back"),},

                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Pump your skill! 💪", replyMarkup: replyKeyboardEM);
                    break;
                case "📗 ADO.net #1":
                    string bookLink1 = "https://drive.google.com/file/d/168N055TmxoJjQBLkahxwtc85hasWgoM8/view?usp=sharing";
                    bookEntry(bookLink1, 1, pictureUrl, e); 
                    break;
                case "📗 ADO.net #2":
                    string bookLink2 = "https://drive.google.com/file/d/168N055TmxoJjQBLkahxwtc85hasWgoM8/view?usp=sharing";
                    bookEntry(bookLink2, 2, pictureUrl, e);
                    break;
                case "📚 Homework":
                    var replyKeyboardHomeWork = new ReplyKeyboardMarkup(new[]
                   {
                        new[] { new KeyboardButton("📄 Homework #2") },
                        new[] { new KeyboardButton("📄 Homework #1"),},
                        new[] { new KeyboardButton("🔙 Back"),},

                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Good Luck! 👌", replyMarkup: replyKeyboardHomeWork);
                    break;
                case "📄 Homework #1":
                    string textLink1 = "https://fsx1.itstep.org/api/v1/files/67SquyJh0Lzhvwbo2V6a60AaFdswwxiw";
                    homeWorkEntry(textLink1, 1, "", "2224.txt", e);
                    break;
                case "📄 Homework #2":
                    string textLink2 = "https://fsx1.itstep.org/api/v1/files/924Db9acPdOya-65NwQk71c0sNyfyh_3";
                    homeWorkEntry(textLink2, 2, pictureUrl, "2225.txt", e);
                    break;
                #region
                //case "/time":
                //    var response = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
                //    await _bot.SendTextMessageAsync(e.Message.Chat.Id, response);
                //    break; 
                #endregion
                default:                   
                    string[] stickers = new string[] { "CAACAgIAAxkBAAKe11_wucfsL89gl0A3dmym3ifNsdpJAALIjQACY4tGDHjHpsKiUIbZHgQ",
                        "CAACAgIAAxkBAAKe2l_wuqLp7KrmkHrLffW1AamtC1b5AAKFEQACPLPFB9QtAAFHuClv0x4E",
                    "CAACAgIAAxkBAAKe3V_wvT_0SoSQXsorY1z71DllxJX2AAKQEQACPLPFB_LLZcaKbmyGHgQ",
                    "CAACAgIAAxkBAAKe4F_wvXckhsdicvTp52ake9PSL-IzAAK0jQACY4tGDB_-KlQJwUFhHgQ",
                    "CAACAgIAAxkBAAKe41_wva3XnNWfUUY4qpAr1TB2sn_qAAIDjgACY4tGDLVBmwhgjPuuHgQ"
                    };
                    int i = _rnd.Next(0, stickers.Length);
                    await _bot.SendStickerAsync(message.Chat.Id, stickers[i]);          
                    break;
            }
        }

        public static string GetFilenameFromWebServer(string url)
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

        private async static void homeWorkEntry(string textLink, int numberWork, string pictureLink, string filePath, MessageEventArgs e)
        {
            string fileName = "";          

            if (textLink != String.Empty)
                fileName = _wc.DownloadString(textLink);          

            if (filePath != String.Empty)
                await writeFileAsync(filePath, fileName);
         
            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                    InlineKeyboardButton.WithUrl($"{GetFilenameFromWebServer(textLink)}", textLink)
                    });

            if (pictureLink == String.Empty)
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, fileName, replyMarkup: inlineKeyboard);
            else
                await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, fileName, replyMarkup: inlineKeyboard);
        }
       
        private async static void bookEntry(string bookLink, int numberBook, string pictureLink, MessageEventArgs e)
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                    InlineKeyboardButton.WithUrl($"Open ADO.net book #{numberBook.ToString()}", bookLink)
                    });

            if (pictureLink == String.Empty)
                await _bot.SendTextMessageAsync(e.Message.Chat.Id, ". . .", replyMarkup: inlineKeyboard);
            else
                await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, replyMarkup: inlineKeyboard);
        }
    }
}