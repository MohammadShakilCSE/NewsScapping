using HtmlAgilityPack;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Infrastructure.Services
{
    public class NewsScraperService : INewsScraperService
    {
        public async Task<List<NewsArticle>> ScrapeTopNewsAsync()
        {
           List<NewsArticle> newsArticles = new List<NewsArticle>();

            string url = "https://www.prothomalo.com/";

            HttpClient httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html); ;

            var newsNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'news_with_item')]");
            var allNewsNode = doc.DocumentNode.SelectNodes("//div");
            //foreach (var newsNode in newsNodes)
            //{
            //    var titleNode = newsNode.SelectSingleNode(".//h3[contains(@class, 'headline-title')]//span[contains(@class, 'sub-title')]");
            //    var linkNode = newsNode.SelectSingleNode(".//a[@href]");

            //    string title = titleNode?.InnerText?.Trim();
            //    string link = linkNode?.GetAttributeValue("href", string.Empty);

            //    if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(link))
            //    {
            //        Make full URL if it's relative
            //        if (link.StartsWith("/"))
            //            link = "https://www.prothomalo.com" + link;

            //        newsArticles.Add(new NewsArticle() { Content = title, Url = link, PublishedAt = DateTime.Now, Title = title, ImagePath = string.Empty });
            //        newsArticles.Add(new NewsArticle() { Content = title, Url = link, PublishedAt = DateTime.Now, Title = title, ImagePath = string.Empty });
            //    }
            //}

            foreach (var newsNode in allNewsNode)
            {
              
                newsArticles.Add(new NewsArticle() { Content = newsNode.InnerText });
            }

            return await Task.FromResult(newsArticles);
        }
    }
}
