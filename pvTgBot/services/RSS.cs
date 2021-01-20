using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text.RegularExpressions;
using System.Xml;

namespace pvTgBot.Services
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Link { get; set; }
    }

    public class RSS
    {
        private static XmlReader _reader;
        private static SyndicationFeed _feed;
        private static string _rssLink = "https://www.radiosvoboda.org/api/zkk-iqemm$ii";

        public static IEnumerable<FeedItem> GetLatestFivePosts()
        {
            _reader = XmlReader.Create(_rssLink);
            _feed = SyndicationFeed.Load(_reader);
            _reader.Close();
            return (from itm in _feed.Items
                    select new FeedItem
                    {
                        Title = itm.Title.Text + "\n",
                        Link = itm.Id + "\n"
                    }).ToList().Take(5);
        }

        public static string GetPostNews()
        {
            var full = new List<string>
                    {"🔥 Головне за день в Україні та світі\n\n",
                        $"🗞{GetHyperLink($"{RSS.GetLatestFivePosts().ToList()[0].Link}", $"{RSS.GetLatestFivePosts().ToList()[0].Title}")}",
                                $"🗞{GetHyperLink($"{RSS.GetLatestFivePosts().ToList()[1].Link}", $"{RSS.GetLatestFivePosts().ToList()[1].Title}")}",
                                        $"🗞{GetHyperLink($"{RSS.GetLatestFivePosts().ToList()[2].Link}", $"{RSS.GetLatestFivePosts().ToList()[2].Title}")}",
                                $"🗞{GetHyperLink($"{RSS.GetLatestFivePosts().ToList()[3].Link}", $"{RSS.GetLatestFivePosts().ToList()[3].Title}")}",
                        $"🗞{GetHyperLink($"{RSS.GetLatestFivePosts().ToList()[4].Link}", $"{RSS.GetLatestFivePosts().ToList()[4].Title}")}"
                    };

            return string.Join(" ", full);
        }

        public static string GetHyperLink(string link, string text)
        {
            Regex regex = new Regex(@"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

            MatchCollection matches = regex.Matches(link);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    link = link.Replace(match.Value, "<a href=\"" + match.Value + "\">" + text + "</a>");
            }
            return link;
        }
    }
}