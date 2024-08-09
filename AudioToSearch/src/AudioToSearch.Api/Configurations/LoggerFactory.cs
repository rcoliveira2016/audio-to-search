using Serilog;

namespace AudioToSearch.Api.Configurations;

public static class LoggerFactory
{
    public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddSerilog(config =>
        {
            config.ReadFrom.Configuration(builder.Configuration);
        });

        return builder;
    }


}
