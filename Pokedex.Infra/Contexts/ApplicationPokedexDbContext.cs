using System.Reflection;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Extensions;

namespace Pokedex.Infra.Contexts;

public class ApplicationPokedexDbContext : DbContext, IUnitOfWork
{
    public ApplicationPokedexDbContext(DbContextOptions<ApplicationPokedexDbContext> options) : base(options)
    {
    }

    public DbSet<Pokemon> Pokemons { get; set; } = null!;
    public DbSet<PokemonTipo> PokemonTipos { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ApplyConfigurations(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTrackingChanges();

        return base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();

        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
    }

    private void ApplyTrackingChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is ITracking && e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
            ((ITracking)entityEntry.Entity).AtualizadoEm = DateTime.Now;

            if (entityEntry.State != EntityState.Added)
                continue;

            ((ITracking)entityEntry.Entity).CriadoEm = DateTime.Now;
        }
    }
}