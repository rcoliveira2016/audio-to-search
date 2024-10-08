﻿namespace AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;

public class CatalogarAudioEntity
{
    public required Guid UId { get; init; }
    public required string Titulo { get; init; }
    public required string Descricao { get; init; }
    public required ICollection<CatalogarAudioTranscricaoEntity> Transcricaoes { get; set; } = new List<CatalogarAudioTranscricaoEntity>();
}
