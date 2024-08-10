using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using AudioToSearch.Infra.Data;
using AudioToSearch.Infra.Data.UnitOfWorks;
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

        group.MapPost("adicionar", async (
            [FromServices] ICatalogarAudioRepository catalogarAudioRepository,
            [FromServices] IUnitOfWork unitOfWork) =>
        {
            var result = new CatalogarAudioEntity { Descricao = "Test1", Titulo = "teste1", UId = new Guid() };
            await catalogarAudioRepository.AddAsync(result);
            await unitOfWork.Commit();
            return Results.Ok(result);
        });

        group.MapPost("consultar", (
            [FromServices] ICatalogarAudioRepository catalogarAudioRepository) =>
        {
            var dataSet = catalogarAudioRepository.GetAllAsync();
            return Results.Ok(dataSet);
        });


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
