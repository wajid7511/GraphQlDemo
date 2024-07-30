namespace GraphQlDemo;

public static class GraphQlDemoServiceExtension
{
    public static IServiceCollection AddMapper(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(GraphQlDemoProfile));
        return service;
    }
}
