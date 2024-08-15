using AudioToSearch.Api.Endpoints;
using Serilog;

namespace AudioToSearch.Api.Configurations;

public static class WebApplicationConfiguration
{
    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddMapIocService(builder.Configuration);

        builder.AddLogger();

        return builder.Build();
    }

    public static void ConfigureWebAplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app
            .AddCatalogarEndPoint()
            .AddHealthCheck();

        app.Run();
    }
}
