using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace ElasticSearchLogging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>(hostingContext, loggerConfigurations)=>
            {
                loggerConfigurations.MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System.Net.Http.Httpclient", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.LifeTime", LogEventLevel.Information);

                var elasticURL = hostingContext.Configuration["ElasticConfiguration:Uri"];
                var serviceName = hostingContext.Configuration["ElasticConfiguration:ServiceName"];
                if (!String.IsNullOrEmpty(elasticURL))
                {
                    loggerConfigurations.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticURL))
                    {
                        AutoRegisterTemplate = true,
                        MinimumLogEventLevel = LogEventLevel.Information,
                        IndexFormat = String.Concat(serviceName , "-logs-{0:yyyy.MM.dd}")
                    }); ;
                }
                else
                {
                    loggerConfigurations.WriteTo.Console();
                }
            };
    }
}
