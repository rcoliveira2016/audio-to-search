using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Aplication.Catalogar.Audio.Jobs;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.ApiKeys;
using AudioToSearch.Infra.Data.Repositorires;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Pgvector;

namespace AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;

public class EmbeddingsAudioCommandHandler(
    ILogger<EmbeddingsAudioCommandHandler> logger,
    IUnitOfWork unitOfWork,
    ICatalogarAudioTranscricaoRepository catalogarAudioTranscricaoRepository
    ) : IRequestHandler<EmbeddingsAudioCommand>
{
    public async Task Handle(EmbeddingsAudioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var transcricoes = catalogarAudioTranscricaoRepository.GetByUIdCatalogarAudio(request.UIdCatalogarAudio).ToList();
            foreach (var item in transcricoes)
            {
                item.Embedding = new Vector(new float[] { 1, 2, 1 });
            }
            await catalogarAudioTranscricaoRepository.Update(transcricoes);
            await unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.LogError(message: "erro ao TranscreverAudio", exception: e);
            throw;
        }
  
    }
}
