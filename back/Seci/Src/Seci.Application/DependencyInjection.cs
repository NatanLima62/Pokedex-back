using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seci.Infra.Data;

namespace Seci.Application;

public static class DependencyInjection
{
    //Configure profiles
    
    //configure services
    public static void ConfigureService(this IServiceCollection service, IConfiguration configuration)
    {
        service.ConfigureDbContext(configuration);
        service.RepositoryDependency();
    }
    
    //services injection
}