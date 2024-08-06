using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Infra.CrossCutting.Settings.Paths;

public class PathSettingsSetup(
        IConfiguration configuration
    ) : IConfigureOptions<PathSettings>
{
    public void Configure(PathSettings options)
    {
        configuration.GetSection("Path").Bind(options);
    }
}
