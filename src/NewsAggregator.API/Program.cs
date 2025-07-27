using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using NewsAggregator.API;
using Serilog;
using System.Reflection;
using NewsAggregator.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var assemblyName = Assembly.GetExecutingAssembly().FullName!;
var webHostEnvironment = builder.Environment.WebRootPath;

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new WebModule());
    containerBuilder.RegisterModule(new PersistenceModule(connectionString, assemblyName));
});


//Serilog Configuration
//builder.Host.userse((ctx, lc) => lc
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .ReadFrom.Configuration(builder.Configuration));

//Localhost HTTPS port configuration
//builder.WebHost
//.ConfigureKestrel(options =>
//{
//    options.ListenLocalhost(7058, opts => opts.UseHttps());
//    //get your localhost htttps port number from launch settings
//});

//builder.WebHost.UseUrls("http://*:80");

builder.Services.AddControllers();

// Add API versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0); // Default to v1.0
    options.AssumeDefaultVersionWhenUnspecified = true; // If client does not specify version, use default
    options.ReportApiVersions = true; // Adds API-supported versions to response headers
    options.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.HeaderApiVersionReader("x-api-version");
    // You can change ApiVersionReader to Query string, Header, or URL segment (see below)
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
