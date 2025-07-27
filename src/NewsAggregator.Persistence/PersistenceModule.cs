using Autofac;
using NewsAggregator.Application.Interfaces;
using NewsAggregator.Persistence.DbContexts;

namespace NewsAggregator.Persistence
{
    public class PersistenceModule : Module
    {
        public readonly string? _connectionString;
        public readonly string? _assemblyName;

        public PersistenceModule(string? connectionString = null,string? assemblyName=null)
        {
            _connectionString = connectionString;
            _assemblyName = assemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("assemblyName", _assemblyName)
               .InstancePerLifetimeScope();

            builder.RegisterType<ApplicationDbContext>().As<IApplicationDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("assemblyName", _assemblyName)
                .InstancePerLifetimeScope();

          
            base.Load(builder);
        }
    }
}
