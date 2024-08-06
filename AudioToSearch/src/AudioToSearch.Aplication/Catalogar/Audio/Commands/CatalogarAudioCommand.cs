using MediatR;

namespace AudioToSearch.Aplication.Catalogar.Audio.Commands;

public class CatalogarAudioCommand: IRequest
{
    public required string CaminhoArquivo { get; init; }
    public required string Descricao { get; init; }
    public required string Nome { get; init; }
}
