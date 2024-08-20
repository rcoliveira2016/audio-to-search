using Pgvector;
using System.ComponentModel.DataAnnotations.Schema;

namespace AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;

public class CatalogarAudioTranscricaoEntity
{
    public required Guid UId { get; init; }
    public required string Texto { get; init; }
    public required TimeSpan Inicio { get; init; }
    public required TimeSpan Final { get; init; }
    public required Guid UIdCatalogarAudio { get; init; }
    public Vector? Embedding { get; set; }
    public required virtual CatalogarAudioEntity CatalogarAudio { get; set; }
}
