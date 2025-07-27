using Autofac;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Application.UseCases;
using NewsAggregator.Infrastructure.Services;

namespace NewsAggregator.API
{
    public class WebModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Register your controllers
            // Application UseCases
            builder.RegisterType<FetchNewsUseCase>()
                     .As<IFetchNewsUseCase>()
                     .InstancePerLifetimeScope();

            // Infrastructure Services
            builder.RegisterType<NewsScraperService>()
                     .As<INewsScraperService>()
                     .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
