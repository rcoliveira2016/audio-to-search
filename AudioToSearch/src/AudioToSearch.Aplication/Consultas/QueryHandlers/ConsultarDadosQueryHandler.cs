using AudioToSearch.Aplication.Consultas.Dtos.Requests;
using AudioToSearch.Aplication.Consultas.Dtos.Responses;
using AudioToSearch.Domain.CatalogarModels.AudioModels.Repositories;
using AudioToSearch.Infra.ServiceAgents.ProvaderAI.Services;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace AudioToSearch.Aplication.Consultas.QueryHandlers;

public class ConsultarDadosQueryHandler(
        [NotNull] ITextEmbeddingService textEmbeddingService,
        [NotNull] ICatalogarAudioTranscricaoRepository catalogarAudioTranscricaoRepository
    ) : IRequestHandler<ConsultarDadosRequest, ConsultarDadosResponse>
{
    public async Task<ConsultarDadosResponse> Handle(ConsultarDadosRequest request, CancellationToken cancellationToken)
    {
        var embaddins = await textEmbeddingService.GenerateEmbedding(request.Termo);

        var transcricoes = catalogarAudioTranscricaoRepository.GetByEmbedding(embaddins.ToArray());

        var itens = transcricoes
            .GroupBy(x => new { x.CatalogarAudio.Titulo, x.CatalogarAudio.Descricao })
            .Select(x => new ConsultarDadosItemResponse { 
                Descricao = x.Key.Descricao, 
                Titulo = x.Key.Titulo,
                ConteudoFiltrado = string.Join("/n", x.Select(y=> y.Texto))
            });

        return new ConsultarDadosResponse
        {
            Itens = itens,
        };
    }
}
