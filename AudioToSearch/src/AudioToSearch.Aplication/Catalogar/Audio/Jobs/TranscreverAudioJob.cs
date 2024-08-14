using AudioToSearch.Aplication.Catalogar.Audio.Commands;
using MediatR;

namespace AudioToSearch.Aplication.Catalogar.Audio.Jobs;

public class TranscreverAudioJob(IMediator bus)
{
    public async Task Executar(string caminhoAudio, Guid uidCatalogarAudio)
    {
        await bus.Send(new TranscreverCatalogarAudioCommand
        {
            CaminhoAudio = caminhoAudio,
            UIdCatalogarAudio = uidCatalogarAudio
        });
    }
}
