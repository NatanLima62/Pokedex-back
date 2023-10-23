using System;
using System.Globalization;
using System.Threading;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pokedex.Application.Configurations;

namespace Pokedex.Application.Test.Fixtures;

public sealed class ServicesFixture : IDisposable
{
    public readonly IMapper Mapper;
    
    public ServicesFixture()
    {
        SetupCulture();
        
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
        
        Mapper = CreateMapper();
    }

    private static void SetupCulture()
    {
        var otherCulture = CultureInfo.CreateSpecificCulture("pt-BR");
        // Change the current thread's culture.
        Thread.CurrentThread.CurrentCulture = otherCulture;
        Thread.CurrentThread.CurrentUICulture = otherCulture;

        // Change the default culture of any new threads created by the application domain.
        // These properties are only available as of .NET 4.5.
        CultureInfo.DefaultThreadCurrentCulture = otherCulture;
        CultureInfo.DefaultThreadCurrentUICulture = otherCulture;
    }
    
    private static IMapper CreateMapper()
    {
        var provider = CreateServiceProvider();
        
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.ConstructServicesUsing(provider.GetService);
            mc.AddProfile(new AutoMapperProfile());
        });
        
        return mappingConfig.CreateMapper();
    }
    
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();

        return services.BuildServiceProvider();
    }

    public void Dispose()
    {
        //
    }
}