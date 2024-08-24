using AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AudioToSearch.Infra.ServiceAgents.ProvaderAI;

public static class ProvaderAIBootstrapper
{
    public static IServiceCollection RegisterProvaderAIBootstrapper(this IServiceCollection services)
    {
        services.AddScoped<ITextEmbeddingService, TextEmbeddingMicrosoftMLService>();

        return services;
    }
}
