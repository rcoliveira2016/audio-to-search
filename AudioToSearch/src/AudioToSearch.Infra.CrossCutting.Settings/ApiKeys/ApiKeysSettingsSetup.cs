using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;

public class ApiKeysSettingsSetup(
    IConfiguration configuration
    ) : IConfigureOptions<ApiKeysSettings>
{
    public void Configure(ApiKeysSettings options)
    {
        configuration.GetSection("ApiKeys").Bind(options);
    }
}
