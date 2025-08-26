public static class ApiRegisterExtention
{
    public static void RegisterEndpointExtension(this WebApplication app)
    {
        var endpointDefinationRef = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
            .Select(Activator.CreateInstance).Cast<IEndpointDefinition>();
        
        foreach (var endpoint in endpointDefinationRef)
        {
            endpoint.RegisterEndpoints(app);
        }
    }
}