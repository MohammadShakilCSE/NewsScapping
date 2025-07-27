using NewsAggregator.Application.DTOs;
using NewsAggregator.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Application.UseCases
{
    public class FetchNewsUseCase : IFetchNewsUseCase
    {
        private readonly INewsScraperService _newsScraperService;

        public FetchNewsUseCase(INewsScraperService newsScraperService)
        {
            _newsScraperService = newsScraperService;
        }
        public Task<List<NewsArticleDto>> ExecuteAsync()
        {
            var news =_newsScraperService.ScrapeTopNewsAsync();
            return news.ContinueWith(task =>
            {
                return task.Result.Select(article => new NewsArticleDto
                {
                    Title = article.Title,
                    PublishedAt=article.PublishedAt,
                    Url = article.Url,
                    Content=article.Content
                }).ToList();
            }); 
        }
    }
}
