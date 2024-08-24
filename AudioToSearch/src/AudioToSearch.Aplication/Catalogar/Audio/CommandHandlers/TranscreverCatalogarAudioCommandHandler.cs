using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using AudioToSearch.Aplication.Catalogar.Audio.Jobs;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.Data.Repositorires;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using Hangfire;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;

public class TranscreverCatalogarAudioCommandHandler(
    ISpeechToTextService speechToTextService,
    ILogger<CatalogarAudioCommandHandler> logger,
    ICatalogarAudioRepository catalogarAudioRepository,
    ICatalogarAudioTranscricaoRepository catalogarAudioTranscricaoRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<TranscreverCatalogarAudioCommand>
{
    public async Task Handle(TranscreverCatalogarAudioCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var catalogarAudio = await catalogarAudioRepository.GetByIdAsync(request.UIdCatalogarAudio);
            if (catalogarAudio is null)
            {
                logger.LogError("não foi encontrado o catalogo {@uidCatalogarAudio}", request.UIdCatalogarAudio);
                return;
            }

            var transcricoes = new List<CatalogarAudioTranscricaoEntity>();
            var result = await speechToTextService.Send(request.CaminhoAudio);
            await foreach (var item in result.Itens)
            {
                transcricoes.Add(new()
                {
                    CatalogarAudio = catalogarAudio,
                    Final = item.End,
                    Inicio = item.Start,
                    Texto = item.Text,
                    UId = new Guid(),
                    UIdCatalogarAudio = catalogarAudio.UId,
                });
            }

            await catalogarAudioTranscricaoRepository.Insert(transcricoes);

            await unitOfWork.Commit();

            File.Delete(request.CaminhoAudio);

            BackgroundJob.Schedule<EmbeddingsAudioJob>(
                x => x.Executar(request.UIdCatalogarAudio),
                TimeSpan.FromSeconds(1));
        }
        catch (Exception e)
        {
            logger.LogError(message: "erro ao TranscreverAudio", exception: e);
        }
    }
}
