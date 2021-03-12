using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using AboutMe.Models;

namespace AboutMe.Controllers
{
    public class AboutMeController : Controller
    {
        public IActionResult Index()
        {
            int helper = 0;
            var items = LoadRssContent("https://news.google.com/rss?hl=pl&gl=PL&ceid=PL:pl");
            Dictionary<int, RssItem[]> news = new Dictionary<int, RssItem[]>();
            for (int i = 0; i < items.Count / 2; i++)
            {
                news[i] = new[] { items[helper], items[helper + 1] };

                if (helper <= items.Count)
                    helper += 2;
            }

            ViewBag.News = news;

            return View();
        }

        private List<RssItem> LoadRssContent(string rssPath)
        {
            var xml = XElement.Load(rssPath);

            var items = xml.Descendants("item").Select(n => new RssItem
            {
                Title = n.Element("title").Value,
                Description = n.Element("description").Value,
                PubDate = n.Element("pubDate").Value
            }).ToList();

            return items;
        }
    }
}
