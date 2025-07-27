using NewsAggregator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Application.Interfaces
{
    public interface INewsScraperService
    {
        Task<List<NewsArticle>> ScrapeTopNewsAsync();
    }
}
