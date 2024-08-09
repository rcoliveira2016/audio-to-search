﻿using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Api.Endpoints;

public static class CatalogoEndPoint
{
    public static WebApplication AddCatalogarEndPoint(this WebApplication app)
    {
        var group = app
            .MapGroup("/catalogar");

        group.MapPost("enviar-audio", [DisableRequestSizeLimit] (
            [FromForm] string descricao,
            IFormFile fileAudio,
            [FromServices]IMediator bus,
            [FromServices]IOptions<PathSettings> pathSettings) =>
        {
            ExecutarEnvioArquivo(descricao, fileAudio, bus, pathSettings);
            return Results.Ok();
        })
            .DisableAntiforgery();


        return app;
    }

    private static void ExecutarEnvioArquivo(string descricao, IFormFile fileAudio, IMediator bus, IOptions<PathSettings> pathSettings)
    {

        var caminho = Path.Combine(
                pathSettings.Value.DiretorioCatalogoAudios,
                $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileAudio.FileName)}"
            );

        using (var stream = new FileStream(caminho, FileMode.Create))
        {
            fileAudio.CopyTo(stream);
        }

        Task.Run(async () =>
        {
            await bus.Send(new CatalogarAudioCommand()
            {
                CaminhoArquivo = caminho,
                Descricao = descricao,
                Nome = fileAudio.FileName
            });
        });
    }
}
