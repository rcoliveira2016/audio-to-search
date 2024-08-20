using MediatR;

namespace AudioToSearch.Aplication.Catalogar.Audio.Commands;

public class EmbeddingsAudioCommand : IRequest
{
    public required Guid UIdCatalogarAudio { get; init; }
}
