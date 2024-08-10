using AudioToSearch.Aplication;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings;
using AudioToSearch.Infra.Data;
using AudioToSearch.Infra.Data.Repositorires;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;

namespace AudioToSearch.Api.Configurations;

public static class MapIocService
{
    public static void AddMapIocService(this IServiceCollection services)
    {
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyReference).Assembly);
        });
        services.AddSettings();
        services.AddScoped<ISpeechToTextService, SpeechToTextService>();
        services.AddData();
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
}
