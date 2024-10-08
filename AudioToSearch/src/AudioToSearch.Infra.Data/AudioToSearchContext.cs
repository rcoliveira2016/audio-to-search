﻿using AudioToSearch.Infra.Data.MapEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AudioToSearch.Infra.Data;

public class AudioToSearchContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AudioToSearchContext(DbContextOptions<AudioToSearchContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogarAudioEntityTypeConfiguration).Assembly);
        modelBuilder.HasPostgresExtension("vector");
        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("AudioToSearchDatabase"), o => o.UseVector());
    }
}
