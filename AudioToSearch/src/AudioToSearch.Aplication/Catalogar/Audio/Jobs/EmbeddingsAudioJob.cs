using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using MediatR;

namespace AudioToSearch.Aplication.Catalogar.Audio.Jobs;

public class EmbeddingsAudioJob(IMediator bus)
{
    public async Task Executar(Guid uidCatalogarAudio)
    {
        await bus.Send(new EmbeddingsAudioCommand
        {
            UIdCatalogarAudio = uidCatalogarAudio
        });
    }
}
