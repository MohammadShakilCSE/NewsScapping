using NewsAggregator.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAggregator.Application.UseCases
{
    public interface IFetchNewsUseCase
    {
        Task<List<NewsArticleDto>> ExecuteAsync();
    }
}
