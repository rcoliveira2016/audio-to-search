using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using Microsoft.Extensions.DependencyInjection;
namespace AudioToSearch.Infra.CrossCutting.Settings;

public static class BootstrapperSettings
{
    public static void AddSettings(this IServiceCollection services)
    {
        services.ConfigureOptions<PathSettingsSetup>();
    }
}
