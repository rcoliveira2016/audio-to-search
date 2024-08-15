using AudioToSearch.Aplication;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings;
using AudioToSearch.Infra.Data;
using AudioToSearch.Infra.Data.Repositorires;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AudioToSearch.Api.Configurations;

public static class MapIocService
{
    public static void AddMapIocService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyReference).Assembly);
        });
        services.AddSettings();
        services.AddScoped<ISpeechToTextService, SpeechToTextService>();
        services.AddData();
        services.AddHangfire(configuration);
        services.ConfigureHealthChecks(configuration);
    }

    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("AudioToSearchDatabase")!, 
                name: "PostgreSql", 
                tags: ["Database", "Essencial"])
            .AddSqlite(configuration.GetConnectionString("AudioToSearchDatabase")!,
                name: "Sqlite",
                tags: ["Database", "Essencial"])
            .AddHangfire(null, name: "Hangfire", tags: ["Database", "Essencial", "Processos"]);
    }

    private static void AddData(this IServiceCollection services)
    {
        services.AddDbContext<AudioToSearchContext>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICatalogarAudioRepository, CatalogarAudioRepository>();
    }

    private static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("HangfireConnection"));

        // Add the processing server as IHostedService
        services.AddHangfireServer();
    }
}
