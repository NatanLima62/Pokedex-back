using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Repositories;

namespace Pokedex.Infra;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationPokedexDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddScoped<IPokemonRepository, PokemonRepository>();
    }
}