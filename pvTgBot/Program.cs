using System;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Net.Http;
using pvTgBot.Services;
using System.Text.RegularExpressions;
using Telegram.Bot.Args;

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
            string pictureSpUrl = "https://fsx1.itstep.org/api/v1/files/eWXXHaiS0ttTYARWZ8d0dX7Y5MGJsfi2";
            string pictureBookSPUrl = "https://mystatfiles.itstep.org/index.php?view_key=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqdzNSpc0vHZUe86hVSyQqWkepFnUfyUoVzFt8Dz5ZbKSnZu2okV2GVfpS70IlpasachTEYmmjQS%2F%2BibhfucijLEk2LG7k3Du5Vc21Gpqnu4YA%3D%3D";
            string pictureBookNPUrl = "https://mystatfiles.itstep.org/index.php?view_key=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqfYr8rOlSUjSjyCoa%2F0K0Yl6sUPaVQELJc01L0mRmp7b5l7wZ7K5CZbSCCvBRPOPSLWvBORWouXtsJibPNqJyfo";
            string pictureNpUrl = "https://fsx1.itstep.org/api/v1/files/30VVsrmWOSGLz53d3qy21Uisl7OmGYdR";


            var message = e.Message;

            if (message == null || message.Type != MessageType.Text)
                return;

            string name = $"{message.From.FirstName} {message.From.LastName}";    
            string logText = $"{DateTime.Now.ToShortTimeString()}\t{ name} send message: '{message.Text}'";       
            string logFileName = $"{DateTime.Now.ToShortDateString()}.txt";

            Console.WriteLine(logText);
                 
            File.AppendAllText(logFileName, logText + "\n");

            switch (message.Text)
            {
                case "Start 🚀":
                case "🔙 Back":
                case "/start":
                    var replyKeyboard = new ReplyKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            new KeyboardButton("📝 Homework"),
                            new KeyboardButton("📚 Materials")
                        },
                        new[]
                        {
                            new KeyboardButton("👨🏼‍💻 Classwork"),
                            new KeyboardButton("🌐 Services")
                        }
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
                    var replyKeyboardCW = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("👨🏼‍🏫 NP"), new KeyboardButton("👨🏻‍🏫 SP") },
                        new [] { new KeyboardButton("🔙 Back") }
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "👨‍🏫", replyMarkup: replyKeyboardCW);
                    break;
                case "👨🏼‍🏫 NP":
                    GetNPcwCase(pictureNpUrl, e);
                    break;
                case "👨🏻‍🏫 SP":
                    GetSPcwCase(pictureSpUrl, e);
                    break;
                case "📚 Materials":
                    var replyKeyboardEM = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("📕 NP"), new KeyboardButton("📗 SP") },
                        new[] { new KeyboardButton("🔙 Back") }
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Pump your skill! 💪", replyMarkup: replyKeyboardEM);
                    break;
                case "📕 NP":
                    var replyKeyboardNP = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("📕 NP #6"), new KeyboardButton("📕 NP #5") },
                        new[] { new KeyboardButton("📕 NP #1-2"), new KeyboardButton("📕 NP #3"), new KeyboardButton("📕 NP #4") },
                        new[] { new KeyboardButton("📚 Materials") }
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Select a book to work with 👇", replyMarkup: replyKeyboardNP);
                    break;
                case "📕 NP #1-2":
                    string bookLinkNP1_2 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqeVLkiJsVaQFA%2F7NUiU7iUjh533WWymIuNeiYU4n7d4%2FhyqogEF5sJfyrj8ShwsVJE%3D";
                    GetPostMystat(bookLinkNP1_2, true, pictureBookNPUrl, "", "NP", e);
                    break;
                case "📕 NP #3":
                    string bookLinkNP3 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7Jtqe5yJDyu3qQ5pbLGY%2F9dnPluCpD7EfptIXiylozywJ3lmb6%2FNQW9SwiSpm%2BJkSC0l8%3D";
                    GetPostMystat(bookLinkNP3, true, pictureBookNPUrl, "", "NP", e);
                    break;
                case "📕 NP #4":
                    string bookLinkNP4 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqfjfadggiQwlQzepaBy9tknWuLdF4Bdbhm76UR2%2BUivjhCk3LoV4%2F340zsDCP3%2BLgE%3D";
                    GetPostMystat(bookLinkNP4, true, pictureBookNPUrl, "", "NP", e);
                    break;
                case "📕 NP #5":
                    string bookLinkNP5 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqfurE8%2FPwAo%2BQGBnK2VZVYliOc7ZUykl7LGZ1OW282uNNFxYFE21%2FqbssP3jpz6nak%3D";
                    GetPostMystat(bookLinkNP5, true, pictureBookNPUrl, "", "NP", e);
                    break;
                case "📕 NP #6":
                    string bookLinkNP6 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7Jtqd10%2FeHoa3CTQy7DaZnXvqwH7WI47xpVVlqfx1lVw9nWHOX06VuZqHz5jlKY5PXtaE%3D";
                    GetPostMystat(bookLinkNP6, true, pictureBookNPUrl, "", "NP", e);
                    break;
                #region -==== SP|Materials ====-
                case "📗 SP":
                    var replyKeyboardSP = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("📗 SP #1"), new KeyboardButton("📗 SP #2"), new KeyboardButton("📗 SP #4")},
                        new[] { new KeyboardButton("📚 Materials") }
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Select a book to work with 👇", replyMarkup: replyKeyboardSP);
                    break;
                case "📗 SP #1":
                    string bookLink1 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7Jtqfo8w3wjEFbvR3coeKkqeGPoGE3U030ZGvLMHzFqMIorp%2FGWycLn7ftU7GDHPm5p3s%3D";
                    GetPostMystat(bookLink1, true, pictureBookSPUrl, "", "SP", e);
                    break;
                case "📗 SP #2":
                    string bookLink2 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7JtqfTmCiBPBVvaQRGS9IFDr3eLoe%2B8L36tAfmM2y6UJjyoqwKGZGyeobvs76lLbsVfnE%3D";
                    GetPostMystat(bookLink2, true, pictureBookSPUrl, "", "SP", e);
                    break;
                case "📗 SP #4":
                    string bookLink3 = "https://mystatfiles.itstep.org/index.php?download=rtILv2awXkYrSQ7WVzOr0I8Q3wN1fIYWXbiFzN7Jtqfaot031Ha0EcSi5ViGxQDkmQZY9wMaU5xhPNleSvJW2faOfxpKPOWTvP8c%2BWBA1Wc%3D";
                    GetPostMystat(bookLink3, true, pictureBookSPUrl, "", "SP", e);
                    break;
                #endregion
                case "🌐 Services":
                    var replyKeyboardServices = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("💸 Exchange Rate"),
                        new KeyboardButton("📰 News") },
                        new[] { new KeyboardButton("☀ Weather"),
                        new KeyboardButton("🔙 Back")}
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Simple services are always with you 💜", replyMarkup: replyKeyboardServices);
                    break;
                case "📝 Homework":
                    var replyKeyboardHomeWork = new ReplyKeyboardMarkup(new[]
                   {
                        new[] { new KeyboardButton("📄 NP"),  new KeyboardButton("📄 SP") },
                        new[] { new KeyboardButton("🔙 Back") }
                        }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Good Luck! 👌", replyMarkup: replyKeyboardHomeWork);
                    break;
                case "📄 NP":
                    var replyKeyboardHomeWorkNP = new ReplyKeyboardMarkup(new[]
                    {
                        new[] { new KeyboardButton("📄 NP #8"), new KeyboardButton("📄 NP #7")},
                        new[] { new KeyboardButton("📄 NP #6"), new KeyboardButton("📄 NP #5"), new KeyboardButton("📄 NP #4") },
                        new[] { new KeyboardButton("📄 NP #3"), new KeyboardButton("📄 NP #2"), new KeyboardButton("📄 NP #1") },
                        new[] { new KeyboardButton("📝 Homework") }
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Select a task to work with 👇", replyMarkup: replyKeyboardHomeWorkNP);
                    break;
                case "📄 NP #1":
                    string textLinkNP1 = "https://fsx1.itstep.org/api/v1/files/VUFOeJ-WrmCMfDTEJo3WsSMzPuGCbB3k";
                    string dueDateNP1 = new DateTime(2021, 02, 09).ToShortDateString();
                    GetPostMystat(textLinkNP1, false, pictureNpUrl, "", dueDateNP1, e);
                    break;
                case "📄 NP #2":
                    string textLinkNP2 = "https://fsx1.itstep.org/api/v1/files/A6tMWi04qVMjIl8JG3ncQbc4HAn5Tckc";
                    string dueDateNP2 = new DateTime(2021, 02, 09).ToShortDateString();
                    GetPostMystat(textLinkNP2, false, pictureNpUrl, "", dueDateNP2, e);
                    break;
                case "📄 NP #3":
                    string textLinkNP3 = "https://fsx1.itstep.org/api/v1/files/Clm-AaUXiGlEx7KDor1OYPREUKmOoIPj";
                    string dueDateNP3 = new DateTime(2021, 02, 11).ToShortDateString();
                    GetPostMystat(textLinkNP3, false, pictureNpUrl, "", dueDateNP3, e);
                    break;
                case "📄 NP #4":
                    string textLinkNP4 = "https://fsx1.itstep.org/api/v1/files/rHbHhtg35SnYSpV49rfYlHZItpp3bE76";
                    string dueDateNP4 = new DateTime(2021, 02, 15).ToShortDateString();
                    GetPostMystat(textLinkNP4, false, pictureNpUrl, "", dueDateNP4, e);
                    break;
                case "📄 NP #5":
                    string textLinkNP5 = "https://fsx1.itstep.org/api/v1/files/81BMX8inUo-kdL0jUbpFUEj1KALY2Tj7";
                    string dueDateNP5 = new DateTime(2021, 02, 16).ToShortDateString();
                    GetPostMystat(textLinkNP5, false, pictureNpUrl, "", dueDateNP5, e);
                    break;
                case "📄 NP #6":
                    string textLinkNP6 = "https://fsx1.itstep.org/api/v1/files/ain_6Goy5wBbcz_VRBkLWb3PYfXgKKrX";
                    string dueDateNP6 = new DateTime(2021, 02, 18).ToShortDateString();
                    GetPostMystat(textLinkNP6, false, pictureNpUrl, "", dueDateNP6, e);
                    break;
                case "📄 NP #7":
                    string textLinkNP7 = "https://fsx1.itstep.org/api/v1/files/BxRKRq71_J9MFY-siepbBGn3fLq9rvg7";
                    string dueDateNP7 = new DateTime(2021, 02, 18).ToShortDateString();
                    GetPostMystat(textLinkNP7, false, pictureNpUrl, "", dueDateNP7, e);
                    break;
                case "📄 NP #8":
                    string textLinkNP8 = "https://fsx1.itstep.org/api/v1/files/m8fQNO5Ki7JNShgfLjyycvNUYM5z0B9u";
                    string dueDateNP8 = new DateTime(2021, 02, 19).ToShortDateString();
                    GetPostMystat(textLinkNP8, false, pictureNpUrl, "", dueDateNP8, e);
                    break;
                #region -==== SP|HomeWorks ====-
                case "📄 SP":
                    var replyKeyboardHomeWorkSP = new ReplyKeyboardMarkup(new[]
                   {
                        new[] {
                                new KeyboardButton("📄 SP #9"),
                                new KeyboardButton("📄 SP #8"),
                                new KeyboardButton("📄 SP #7")
                        },
                        new[] {
                                new KeyboardButton("📄 SP #6"),
                                new KeyboardButton("📄 SP #5"),               
                                new KeyboardButton("📄 SP #4")
                        },
                        new[] {
                            new KeyboardButton("📄 SP #3"),
                            new KeyboardButton("📄 SP #2"),
                            new KeyboardButton("📄 SP #1")},
                        new[]{new KeyboardButton("📝 Homework") },
                    }, true);
                    await _bot.SendTextMessageAsync(message.Chat.Id, "Select a task to work with 👇", replyMarkup: replyKeyboardHomeWorkSP);
                    break;
                case "📄 SP #1":
                    string textLink1 = "https://fsx1.itstep.org/api/v1/files/-aMk6utGlPy3l0PxfqtpRDoxJzyWcbLk";
                    string dueDate1 = new DateTime(2021, 01, 18).ToShortDateString();
                    GetPostMystat(textLink1, false, pictureSpUrl, "", dueDate1, e);
                    break;
                case "📄 SP #2":
                    string textLink2 = "https://fsx1.itstep.org/api/v1/files/jJCWYhF4rdJ0yp-wAVrtYUeH72yqZhgZ";
                    string dueDate2 = new DateTime(2021, 01, 19).ToShortDateString();
                    GetPostMystat(textLink2, false, pictureSpUrl, "", dueDate2, e);
                    break;
                case "📄 SP #3":
                    string textLink3 = "https://fsx1.itstep.org/api/v1/files/psdBHL_YV6E1DfctijguCB5KD8RhD0MT";
                    string dueDate3 = new DateTime(2021, 01, 26).ToShortDateString();
                    GetPostMystat(textLink3, false, pictureSpUrl, "", dueDate3, e);
                    break;
                case "📄 SP #4":
                    string textLink4 = "https://fsx1.itstep.org/api/v1/files/8G-c-2vggbe2CpOwRt6wxdd-xSJI814g";
                    string dueDate4 = new DateTime(2021, 01, 27).ToShortDateString();
                    GetPostMystat(textLink4, false, pictureSpUrl, "", dueDate4, e);
                    break;
                case "📄 SP #5":
                    string textLink5 = "https://fsx1.itstep.org/api/v1/files/3ixQLMs4EZUCObCwf5nw4_mimm_mkloE";
                    string dueDate5 = new DateTime(2021, 01, 29).ToShortDateString();
                    GetPostMystat(textLink5, false, pictureSpUrl, "", dueDate5, e);
                    break;
                case "📄 SP #6":
                    string textLink6 = "https://fsx1.itstep.org/api/v1/files/5gEsrZsqWnB4_CiD916STpCllOZzOeta";
                    string dueDate6 = new DateTime(2021, 01, 29).ToShortDateString();
                    GetPostMystat(textLink6, false, pictureSpUrl, "", dueDate6, e);
                    break;
                case "📄 SP #7":
                    string textLink8 = "https://fsx1.itstep.org/api/v1/files/ler8U2_bfCcRU2rhDuMQtY0CZJtFfgbe";
                    string dueDate8 = new DateTime(2021, 01, 29).ToShortDateString();
                    GetPostMystat(textLink8, false, pictureSpUrl, "", dueDate8, e);
                    break;
                case "📄 SP #8":
                    string textLink7 = "https://fsx1.itstep.org/api/v1/files/Qjz0XA9P_JdyKgzBimVW1hFzINtK2ita";
                    string dueDate7 = new DateTime(2021, 01, 29).ToShortDateString();
                    GetPostMystat(textLink7, false, pictureSpUrl, "", dueDate7, e);
                    break;
                case "📄 SP #9":
                    string textLink9 = "https://fsx1.itstep.org/api/v1/files/SYbKvlAWVm1hS5j_E3YxLZa03bwWVJmh";
                    string dueDate9 = new DateTime(2021, 01, 31).ToShortDateString();
                    GetPostMystat(textLink9, false, pictureSpUrl, "", dueDate9, e);
                    break;
                #endregion
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
                case "💸 Exchange Rate":
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, ExchangeRatesCase()/*, replyMarkup: inlineKeyboardRates*/);
                    break;
                case "/mono":
                    MonoBankExchangeRatesCase(e);                   
                    break;
                case "/privat":
                    string privatLink = "https://privatbank.ua/uploads/media/default/0001/14/a6507601ef7e311f4d5af21ea9b8e0ce69105850.png";
                    await _bot.SendPhotoAsync(e.Message.Chat.Id, privatLink,
                            PrivatCurrencyAPI.GetPrivatExchangeRate().Result + "\n👇🏻 підтримати автора бота\n📑 4149 4991 1185 5175");
                    break;
                case "/weather":
                case "☀ Weather":
                    await _bot.SendTextMessageAsync(message.Chat.Id, WeatherAPI.GetWeather("Zaporizhia").Result);
                    break;
                case "/news":
                case "📰 News":
                    string logo = "https://www.radiosvoboda.org/Content/responsive/RFE/uk-UA/img/logo.png";                   
                    await _bot.SendPhotoAsync(message.Chat.Id, logo, RSS.GetPostNews(), ParseMode.Html);
                    break;
                case "/np":                
                    await _bot.SendTextMessageAsync(message.Chat.Id, NovaPoshta.GetTrackingData("20450328027569", "0504538315").Result);
                    break;
                default:                                   
                    await _bot.SendStickerAsync(message.Chat.Id, stickersErr());          
                    break;
            }
        }

        private async static void GetNPcwCase(string pictureUrl, Telegram.Bot.Args.MessageEventArgs e)
        {
            var link1 = "https://github.com/itstep-org/itstep_pv912_np/tree/master/20210204_socket";
            var link2 = "https://github.com/itstep-org/itstep_pv912_np/tree/master/20210208_dgrm_socket";
            var link2_1 = "https://github.com/itstep-org/itstep_pv912_np/tree/master/20210208_tcpClient_tcpListener";
            var link3 = "https://github.com/itstep-org/itstep_pv912_np/tree/master/20210209_chat";
            var link4 = "https://github.com/itstep-org/itstep_pv912_np/tree/master/20210211_UdpClient";

            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                new[] {InlineKeyboardButton.WithUrl(link4.Remove(0, 67).Trim('_').Replace('_', ' '), link4) },
                new[] {InlineKeyboardButton.WithUrl(link3.Remove(0, 67).Trim('_').Replace('_', ' '), link3) },
                new[] {InlineKeyboardButton.WithUrl(link2.Remove(0, 67).Trim('_').Replace('_', ' '), link2),
                InlineKeyboardButton.WithUrl(link2_1.Remove(0, 67).Trim('_').Replace('_', ' '), link2_1)},
                new[] {InlineKeyboardButton.WithUrl(link1.Remove(0, 67).Trim('_').Replace('_', ' '), link1) } });

            await _bot.SendPhotoAsync(e.Message.From.Id, pictureUrl, "Choose the lesson you need 👇", replyMarkup: inlineKeyboard);
        }

        private async static void GetSPcwCase(string pictureUrl, Telegram.Bot.Args.MessageEventArgs e)
        {
            var link1 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210111_um_reg_dll";
            var link2 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210112_proc";
            var link2_1 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210112_proc2";
            var link3 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210119_domains";
		    var link4 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210120_treads_1";
            var link5 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210121_sync";
            var link6 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210126_monitor_mutex";
            var link7 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/20210127_semaphore_events_pool";
            var link8 = "https://github.com/itstep-org/itstep_pv912_sp/tree/master/bonus";

            var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                new[] {InlineKeyboardButton.WithUrl(link8.Remove(0, 58), link8) },
                new[] {InlineKeyboardButton.WithUrl(link7.Remove(0, 67).Trim('_').Replace('_', ' '), link7) },
                new[] {InlineKeyboardButton.WithUrl(link6.Remove(0, 67).Trim('_').Replace('_', ' '), link6) },
                new[] {InlineKeyboardButton.WithUrl(link5.Remove(0, 67).Trim('_').Replace('_', ' '), link5) },
                new[] {InlineKeyboardButton.WithUrl(link4.Remove(0, 67).Trim('_').Replace('_', ' '), link4) },
                new[] {InlineKeyboardButton.WithUrl(link3.Remove(0, 67).Trim('_').Replace('_', ' '), link3) },
                new[] { InlineKeyboardButton.WithUrl(link2_1.Remove(0, 67).Trim('_').Replace('_', ' '), link2_1) ,
                InlineKeyboardButton.WithUrl(link2.Remove(0, 67).Trim('_').Replace('_', ' '), link2)},
                new[] { InlineKeyboardButton.WithUrl(link1.Remove(0, 67).Trim('_').Replace('_', ' '), link1) }
            });
            await _bot.SendPhotoAsync(e.Message.From.Id, pictureUrl, "Choose the lesson you need 👇", replyMarkup: inlineKeyboard);
        }

        private async static void GetPostMystat(string link, bool isMaterial, string pictureLink, string filePath, string dueDate, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (isMaterial)
            {
                string fileName = GetFilenameFromWebServer(link);                
                //fileName = fileName.Substring(0, fileName.LastIndexOf('_') + 0).Replace('_', ' '); 
                
                if(fileName.StartsWith("NP"))
                    fileName = fileName.Substring(0, fileName.LastIndexOf('_') - 1).Replace('_', ' ');
                else
                    fileName = fileName.Substring(0, fileName.LastIndexOf('_') + 0).Replace('_', ' ');


                var inlineKeyboard = new InlineKeyboardMarkup(new[] {
                    //InlineKeyboardButton.WithUrl($"Open {dueDate} book #{numberBook}", link)
                    InlineKeyboardButton.WithUrl($"{fileName}", link)
                    });

                if (pictureLink == String.Empty)
                    await _bot.SendTextMessageAsync(e.Message.Chat.Id, ". . .", replyMarkup: inlineKeyboard);
                else
                    await _bot.SendPhotoAsync(e.Message.Chat.Id, pictureLink, replyMarkup: inlineKeyboard);
            }
            else
            {
                string fileName = "";

                if (link != String.Empty && dueDate != String.Empty)
                    fileName = _wc.DownloadString(link) + $"\n\nDue date: {dueDate}";
                else
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

        public static string ExchangeRatesCase()
        {
            return $"📊 Актуальні курси валют:\n\nНБУ\n{NBUCurrencyAPI.GetExchangeRateNBU()}\n\n" +
                $"MonoBank  /mono\n" +
                $"ПриватБанк  /privat\n\n" +
                        $"EXMO\n{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("BTC", "USD").Result}" +
                            $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("ETH", "USD").Result}" +
                                $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("LTC", "USD").Result}" +
                            $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("ZEC", "USD").Result}" +
                        $"{EXMOCurrencyAPI.GetExchangeDigitRateEXMO("XRP", "USD").Result}";
        }

        private async static void MonoBankExchangeRatesCase(Telegram.Bot.Args.MessageEventArgs e)
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

                string logText = $"{DateTime.Now.ToShortTimeString()}\tException: { ex.Message}";
                Console.WriteLine(logText);

                string logFileName = $"{DateTime.Now.ToShortDateString()}.txt";

                File.AppendAllText(logFileName, logText + "\n");
            }
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
        //        if (btnText == "privat")
        //        {
        //            string privatLink = "https://privatbank.ua/uploads/media/default/0001/14/a6507601ef7e311f4d5af21ea9b8e0ce69105850.png";
        //            await _bot.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, privatLink,
        //                    PrivatCurrencyAPI.GetPrivatExchangeRate().Result + "\n👇🏻 підтримати автора бота\n📑 4149 4991 1185 5175");
        //        }
        //        else if (btnText == "mono")
        //        {
        //            var maxRetryAttempts = 3;

        //            try
        //            {
        //                await RetryHelper.RetryOnExceptionAsync<HttpRequestException>
        //                    (maxRetryAttempts, async () =>
        //                    {
        //                        string monoLink = "https://psm7.com/awards-2020/wp-content/uploads/2020/11/1604584696-image-480x230-c-default.png";
        //                        string monoRef = "https://monobank.ua/r/GsbX";
        //                        string monoDonate = "send.monobank.ua/jar/5JfMjg4P5K";
        //                        var inlineKeyboardMono = new InlineKeyboardMarkup(new[] {
        //                            new[] { InlineKeyboardButton.WithUrl("💳 отримати картку в 2 кліки", monoRef) },
        //                            new[] { InlineKeyboardButton.WithUrl("🐈 підтримати автора бота", monoDonate)}
        //                        });
        //                        await _bot.SendPhotoAsync(e.CallbackQuery.Message.Chat.Id, monoLink,
        //                            MonoBankCurrencyAPI.GetMonoExchangeRate().Result, replyMarkup: inlineKeyboardMono);
        //                        //await _bot.SendTextMessageAsync(e.Message.Chat.Id, mono().Result, replyMarkup: inlineKeyboardMono);
        //                    });
        //            }
        //            catch (Exception ex)
        //            {
        //                await _bot.SendTextMessageAsync(e.CallbackQuery.Message.Chat.Id, "📡 Між запитами необхідно трохи зачекати," +
        //                    " така вимога сервера. Спробуйте пізніше 🤷🏻‍♂️");

        //                string logText = $"{DateTime.Now.ToShortTimeString()}\tException: { ex.Message}";
        //                Console.WriteLine(logText);

        //                string logFileName = $"{DateTime.Now.ToShortDateString()}.txt";

        //                File.AppendAllText(logFileName, logText + "\n");
        //            }
        //        }

        //        await _bot.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
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