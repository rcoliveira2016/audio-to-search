using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Aplication.Consultas.Dtos.Requests;
using AudioToSearch.Aplication.Consultas.Dtos.Responses;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using AudioToSearch.Infra.Data.UnitOfWorks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Api.Endpoints;

public static class ConsultaEndpoint
{
    public static WebApplication AddConsultaEndpoint(this WebApplication app)
    {
        var group = app
            .MapGroup("/consulta");

        group.MapGet("/", async (
            [FromQuery] string termo,
            [FromServices] IMediator bus
            ) =>
        {
            return await bus.Send<ConsultarDadosResponse>(new ConsultarDadosRequest { Termo = termo });
        });

        return app;
    }

}
