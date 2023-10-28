using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seci.Domain.Enties;

namespace Seci.Infra.Data.Mappings;

public class AdministradorMapping : IEntityTypeConfiguration<Administrador>
{
    public void Configure(EntityTypeBuilder<Administrador> builder)
    {
        builder
            .Property(a => a.Nome)
            .HasMaxLength(120)
            .IsRequired();
        
        builder
            .Property(a => a.Email)
            .HasMaxLength(120)
            .IsRequired();
        
        builder
            .Property(a => a.Senha)
            .HasMaxLength(250)
            .IsRequired();
    }
}