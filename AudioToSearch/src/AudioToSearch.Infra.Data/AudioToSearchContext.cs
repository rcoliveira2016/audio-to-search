using AudioToSearch.Domain.CatalogarModels.AudioModels.Entitis;
using AudioToSearch.Infra.Data.MapEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AudioToSearch.Infra.Data;

public class AudioToSearchContext : DbContext
{
    public AudioToSearchContext(DbContextOptions<AudioToSearchContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogarAudioEntityTypeConfiguration).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlite("Data Source=Application.db;");
    }
}
