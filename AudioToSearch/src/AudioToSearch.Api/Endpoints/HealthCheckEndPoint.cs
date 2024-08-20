using AudioToSearch.Api.Configurations.HealthCheck;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Api.Endpoints;

public static class HealthCheckEndPoint
{
    public static WebApplication AddHealthCheck(this WebApplication app)
    {
        app.MapHealthChecks("/health-check", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = HealthCheckExtensions.WriteResponse,
        });

        return app;
    }
}
