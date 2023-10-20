using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pokedex.Domain.Contracts;

namespace Pokedex.Infra.Extensions;

public static class ModelBuilderExtension
{
    public static void ApplyEntityConfiguration(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.GetEntities<IEntity>();
        var props = entities.SelectMany(c => c.GetProperties()).ToList();

        foreach (var property in props.Where(c => c.ClrType == typeof(int) && c.Name == "Id"))
        {
            property.IsKey();
        }
    }
    
    public static void ApplyTrackingConfiguration(this ModelBuilder modelBuilder)
    {
        var propDatas = new[] { "CriadoEm", "AtualizadoEm" };
        var propIds = new[] { "CriadoPor", "AtualizadoPor" };
        var propBools = new[] { "CriadoPorAdmin", "AtualizadoPorAdmin" };
        
        var entidades = modelBuilder.GetEntities<ITracking>();

        var dataProps = entidades
            .SelectMany(c 
                => c.GetProperties().Where(p => p.ClrType == typeof(DateTime) && propDatas.Contains(p.Name)));

        foreach (var prop in dataProps)
        {
            prop.SetColumnType("timestamp");
            prop.SetDefaultValueSql("CURRENT_TIMESTAMP");
        }
        
        var idProps = entidades
            .SelectMany(c 
                => c.GetProperties().Where(p => p.ClrType == typeof(int) && propIds.Contains(p.Name)));
        
        foreach (var prop in idProps)
        {
            prop.IsNullable = true;
        }
        
        var boolProps = entidades
            .SelectMany(c 
                => c.GetProperties().Where(p => p.ClrType == typeof(bool) && propBools.Contains(p.Name)));
        
        foreach (var prop in boolProps)
        {
            prop.SetDefaultValue(false);
            prop.IsNullable = false;
        }
    }
    
    private static List<IMutableEntityType> GetEntities<T>(this ModelBuilder modelBuilder)
    {
        var entities = modelBuilder.Model.GetEntityTypes()
            .Where(c => c.ClrType.GetInterface(typeof(T).Name) != null).ToList();

        return entities;
    }
}