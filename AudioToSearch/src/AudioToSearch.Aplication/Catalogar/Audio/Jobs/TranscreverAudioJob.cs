using AudioToSearch.Aplication.Catalogar.Audio.CommandHandlers;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.CrossCutting.Settings.Paths;
using AudioToSearch.Infra.Data.UnitOfWorks;
using AudioToSearch.Infra.ServiceAgents.SpeechToText.SpeechToText;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AudioToSearch.Aplication.Catalogar.Audio.Jobs;

public class TranscreverAudioJob(
    ISpeechToTextService speechToTextService,
    ILogger<CatalogarAudioCommandHandler> logger,
    ICatalogarAudioRepository catalogarAudioRepository,
    IUnitOfWork unitOfWork
    )
{
    public async Task Executar(string caminhoAudio, Guid uidCatalogarAudio)
    {
        try
        {
            var catalogarAudio = await catalogarAudioRepository.GetByIdAsync(uidCatalogarAudio);
            if (catalogarAudio is null)
            {
                logger.LogError("não foi encontrado o catalogo {@uidCatalogarAudio}", uidCatalogarAudio);
                return;
            }
            var result = await speechToTextService.Send(caminhoAudio);
            await foreach (var item in result.Itens)
            {
                catalogarAudio.Transcricaoes.Add(new()
                {
                    CatalogarAudio = catalogarAudio,
                    Final = item.End,
                    Inicio = item.Start,
                    Texto = item.Text,
                    UId = new Guid(),
                    UIdCatalogarAudio = catalogarAudio.UId,
                });
            }
            await unitOfWork.Commit();
        }
        catch (Exception e)
        {
            logger.LogError(message: "erro ao TranscreverAudio", exception: e);
            throw;
        }
    }
}
