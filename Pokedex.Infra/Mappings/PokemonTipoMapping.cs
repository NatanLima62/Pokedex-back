using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Mappings;

public class PokemonTipoMapping : IEntityTypeConfiguration<PokemonTipo>
{
    public void Configure(EntityTypeBuilder<PokemonTipo> builder)
    {
        builder.Property(p => p.Nome)
            .HasMaxLength(120);

        builder
            .HasMany(pt => pt.Pokemons)
            .WithOne(p => p.PokemonTipo)
            .HasForeignKey(p => p.PokemonTipoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}