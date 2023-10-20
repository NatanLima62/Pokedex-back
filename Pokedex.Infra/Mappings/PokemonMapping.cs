using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pokedex.Domain.Entities;

namespace Pokedex.Infra.Mappings;

public class PokemonMapping : IEntityTypeConfiguration<Pokemon>
{
    public void Configure(EntityTypeBuilder<Pokemon> builder)
    {
        builder.Property(p => p.Nome)
            .HasMaxLength(120);
        
        builder.Property(p => p.Descricao)
            .HasMaxLength(255);
        
        builder.Property(p => p.PokemonTipoId)
            .IsRequired();
    }
}