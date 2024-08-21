using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Aplication.Catalogar.Audio.Jobs;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pgvector;

namespace AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;

public class EmbeddingsAudioCommandHandler(
    //IOptions<ApiKeysSettings> apiKeysSettingsOptions,
    ILogger<EmbeddingsAudioCommandHandler> logger,
    ICatalogarAudioRepository catalogarAudioRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<EmbeddingsAudioCommand>
{
    public async Task Handle(EmbeddingsAudioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await catalogarAudioRepository.UpdateTranscricao(
                x => x.UIdCatalogarAudio == request.UIdCatalogarAudio,
                x => x.Embedding, x => new Vector(new float[] { 1, 1, 1 }));
            await unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.LogError(message: "erro ao TranscreverAudio", exception: e);
            throw;
        }
    }
}
