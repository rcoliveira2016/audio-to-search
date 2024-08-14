using MediatR;

namespace AudioToSearch.Aplication.Catalogar.Audio.Commands;

public class TranscreverCatalogarAudioCommand : IRequest
{
    public required string CaminhoAudio { get; init; } 
    public required Guid UIdCatalogarAudio { get; init; }
}
