using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using vttk_auto.Infractructure.Context;

namespace vttk_auto.Extensions;

public static class ServiceCollectionsExtensions
{
    public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Car info",
                Version = "v1",
            });
        });
        return builder;
    }
    
    public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<CarDbContext>(option =>
        {
            option.UseNpgsql(builder.Configuration.GetConnectionString(nameof(CarDbContext)));
        });
        
        return builder;
    }
    
    public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<HtmlWeb>();
        return builder;
    }

    public static WebApplicationBuilder AddIntegrationServices(this WebApplicationBuilder builder)
    {
        return builder;
    }
}