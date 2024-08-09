using AudioToSearch.Api.Endpoints;

namespace AudioToSearch.Api.Configurations;

public static class WebApplicationConfiguration
{
    public static WebApplication BuildApp(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddMapIocService();

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

        app.AddCatalogarEndPoint();

        app.Run();
    }
}
