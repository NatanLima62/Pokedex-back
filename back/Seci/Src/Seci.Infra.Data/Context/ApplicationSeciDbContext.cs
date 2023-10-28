using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Seci.Domain.Contracts;
using Seci.Domain.Enties;
using Seci.Infra.Data.Extensions;

namespace Seci.Infra.Data.Context;

public class ApplicationSeciDbContext : DbContext, IUnitOfWork
{
    public ApplicationSeciDbContext(DbContextOptions<ApplicationSeciDbContext> options) : base(options) { }

    public DbSet<Administrador> Administradores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ApplyConfigurations(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit() => await SaveChangesAsync() > 0;

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ApplyTrackingChanges();
        return base.SaveChangesAsync(cancellationToken);
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

    private static void ApplyConfigurations(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<ValidationResult>();
        
        modelBuilder.ApplyEntityConfiguration();
        modelBuilder.ApplyTrackingConfiguration();
        modelBuilder.ApplySoftDeleteConfiguration();
    }
}