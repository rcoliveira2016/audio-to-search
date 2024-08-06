using AudioToSearch.Aplication;
using AudioToSearch.Infra.CrossCutting.Settings;
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
    }
}
