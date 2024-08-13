using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace AudioToSearch.Infra.Data.MapEntity;

public class CatalogarAudioEntityTypeConfiguration : IEntityTypeConfiguration<CatalogarAudioEntity>
{
    public void Configure(EntityTypeBuilder<CatalogarAudioEntity> builder)
    {
        builder.HasKey(b => b.UId);
        builder.HasMany(e => e.Transcricaoes)
            .WithOne(e => e.CatalogarAudio)
            .HasForeignKey(e => e.UIdCatalogarAudio)
            .IsRequired();
    }
}
