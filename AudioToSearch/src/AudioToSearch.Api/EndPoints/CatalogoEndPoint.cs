﻿using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
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

        group.MapPost("enviar-audio", [DisableRequestSizeLimit] async (
            [FromForm] string descricao,
            [FromForm] string titulo,
            IFormFile fileAudio,
            [FromServices] IMediator bus,
            [FromServices] IOptions<PathSettings> pathSettings) =>
        {
            await ExecutarEnvioArquivo(descricao, titulo, fileAudio, bus, pathSettings);
            return Results.Ok();
        })
        .DisableAntiforgery();


        group.MapGet("transcricoes", [DisableRequestSizeLimit] async (
            [FromServices] ICatalogarAudioRepository catalogarAudioRepository
            ) =>
        {
            var all = await catalogarAudioRepository.GetAllAsync();

            return Results.Ok(all.Select(x => new
            {
                x.UId,
                x.Descricao,
                x.Titulo,
                Transcricaoes = x.Transcricaoes.Select(x => new { x.Final, x.Inicio, x.Texto, x.Embedding })
            }));
        });

        return app;
    }

    private static async Task ExecutarEnvioArquivo(string descricao, string titulo, IFormFile fileAudio, IMediator bus, IOptions<PathSettings> pathSettings)
    {

        var caminho = Path.Combine(
                pathSettings.Value.DiretorioCatalogoAudios,
                $"{Guid.NewGuid().ToString()}{Path.GetExtension(fileAudio.FileName)}"
            );

        using (var stream = new FileStream(caminho, FileMode.Create))
        {
            fileAudio.CopyTo(stream);
        }

        await bus.Send(new CatalogarAudioCommand()
        {
            CaminhoArquivo = caminho,
            Descricao = descricao,
            Nome = fileAudio.FileName,
            Titulo = titulo,
        });
    }
}
