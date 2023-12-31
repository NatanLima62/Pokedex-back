using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pokedex.Application.Contracts;
using Pokedex.Application.Notifications;
using Pokedex.Application.Services;
using Pokedex.Infra;

namespace Pokedex.Application;

public static class DependencyInjection
{
    public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureDbContext(configuration);
        
        services.AddRepositories();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        AplicarServices(services);
    }

    private static void AplicarServices(this IServiceCollection services)
    {
        services
            .AddScoped<INotificator, Notificator>();

        services
            .AddScoped<IPokemonTipoService, PokemonTipoService>()
            .AddScoped<IPokemonService, PokemonService>();
    }
}