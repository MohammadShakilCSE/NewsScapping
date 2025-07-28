using HtmlAgilityPack;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

            // var newsNodes = doc.DocumentNode.SelectNodes("//div[contains(@class, 'news_with_item')]");
            var articleNode = doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'mgbH5')]");


            //First item in the page
            if (articleNode != null)
            {
                var titleSpan = articleNode.SelectSingleNode(".//h1[contains(@class, 'headline-title')]//span");
                var linkNode = articleNode.SelectSingleNode(".//h1//a[@class='title-link']");
                var excerptNode = articleNode.SelectSingleNode(".//a[contains(@class, 'excerpt')]");
                var timeNode = articleNode.SelectSingleNode(".//time[contains(@class, 'published-time')]");

                string title = titleSpan?.InnerText.Trim() ?? "(no title)";
                string link = linkNode?.GetAttributeValue("href", "") ?? "(no link)";
                string excerpt = excerptNode?.InnerText.Trim() ?? "(no excerpt)";
                string time = timeNode?.InnerText.Trim() ?? "(no time)";

                newsArticles.Add(new NewsArticle
                {
                    Title = title,
                    Url = link,
                    Content = excerpt,
                    PublishedAt = time,
                    ImagePath = ""
                });


            }

            var news_item = doc.DocumentNode.SelectNodes("//div[contains(@class, 'news_item')]");
            if (news_item != null)
            {
                newsArticles.AddRange(await ScrapeNewsByCategoryAsync(news_item));
            }

            var newsWithNoitems = doc.DocumentNode.SelectNodes("//div[contains(@class, 'news_with_no_image')]");
            if(newsWithNoitems != null)
            {
                newsArticles.AddRange(await ScrapeNewsByCategoryAsync(newsWithNoitems));
            }


            return await Task.FromResult(newsArticles);
        }
        private async Task<List<NewsArticle>> ScrapeNewsByCategoryAsync(dynamic news)
        {
            List<NewsArticle> newsArticles = new List<NewsArticle>();
            if (news != null)
            {
                foreach (var item in news)
                {
                    var titleSpan = item.SelectSingleNode(".//h3[contains(@class, 'headline-title')]//span");
                    string title = titleSpan?.InnerText.Trim() ?? "(no title)";
                    if (newsArticles.Any(n => n.Title == title))
                    {
                        continue; // Skip if the title already exists
                    }

                    var linkNode = item.SelectSingleNode(".//h3//a[@class='title-link']");
                    string link = linkNode?.GetAttributeValue("href", "") ?? "(no link)";

                    var excerptNode = item.SelectSingleNode(".//a[contains(@class, 'excerpt')]");
                    string excerpt = excerptNode?.InnerText.Trim() ?? "(no excerpt)";


                    var timeNode = item.SelectSingleNode(".//time[contains(@class, 'published-time')]");
                    string time = timeNode?.InnerText.Trim() ?? "(no time)";

                    newsArticles.Add(new NewsArticle
                    {
                        Title = title,
                        Url = link,
                        Content = excerpt,
                        PublishedAt = time,
                        ImagePath = ""
                    });
                }
            }
            return await Task.FromResult(newsArticles);
        }
    }
}
