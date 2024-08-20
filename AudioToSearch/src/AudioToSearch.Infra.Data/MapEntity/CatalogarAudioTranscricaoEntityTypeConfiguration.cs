using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AudioToSearch.Infra.Data.MapEntity;

public class CatalogarAudioTranscricaoEntityTypeConfiguration : IEntityTypeConfiguration<CatalogarAudioTranscricaoEntity>
{
    public void Configure(EntityTypeBuilder<CatalogarAudioTranscricaoEntity> builder)
    {
        builder.HasKey(x => x.UId);

        builder
            .Property(i => i.Embedding)
            .HasColumnType("vector(3)");
    }
}
