namespace Pokedex.Api.Configuration;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            options.OrderActionsBy(a => a.GroupName);
        });
    }
}