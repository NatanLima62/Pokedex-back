using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seci.Core.Authorization;
using Seci.Core.Extensions;
using Seci.Domain.Contracts.Repositories;
using Seci.Infra.Data.Context;
using Seci.Infra.Data.Repositories;

namespace Seci.Infra.Data;

public static class DependencyInjection
{
    public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        
        services.AddScoped<IAuthenticatedUser>(sp =>
        {
            var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
            
            return httpContextAccessor.UsuarioAutenticado() ? new AuthenticatedUser(httpContextAccessor) : new AuthenticatedUser();
        });
        
        services.AddDbContext<ApplicationSeciDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion);
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();
        });
    }

    public static void RepositoryDependency(this IServiceCollection service)
    {
        service.AddScoped<IAdministradorRepository, AdministradorRepository>();
    }
}