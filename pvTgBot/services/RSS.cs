using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Xml;

namespace pvTgBot.Services
{
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
                        Title = itm.Title.Text +"\n",
                        Link = itm.Id + "\n"
                    }).ToList().Take(5);
        }

        public class FeedItem
        {
            public string Title { get; set; }
            public string Link { get; set; }
        }
    }
}
