namespace AudioToSearch.Aplication.Consultas.Dtos.Responses;

public sealed class ConsultarDadosResponse
{
    public IEnumerable<ConsultarDadosItemResponse> Itens { get; init; } = Enumerable.Empty<ConsultarDadosItemResponse>();
}

public class ConsultarDadosItemResponse
{
    public required string Titulo { get; init; }
    public required string Descricao { get; init; }
    public required string ConteudoFiltrado { get; init; }
}