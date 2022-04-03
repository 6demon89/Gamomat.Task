using SlotMachine.API.Interfaces;

namespace SlotMachine.API.Extentions
{
    /// <summary>
    /// Proved easy way to extract and auto-map Endpoint
    /// </summary>
    public static class EndpointDefinitionExtensions
    {
        /// <summary>
        /// Checks assembly for all implementation of IEndpointDefinition interface and adds it to the service in a singleton scope
        /// </summary>
        public static void AddEndpointDefinitions(
            this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();

            foreach (var marker in scanMarkers)
            {
                endpointDefinitions.AddRange(
                    marker.Assembly.ExportedTypes
                        .Where(x => typeof(IEndpointDefinition).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(Activator.CreateInstance).Cast<IEndpointDefinition>()
                );
            }

            foreach (var endpointDefinition in endpointDefinitions)
            {
                endpointDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        }


        /// <summary>
        /// Injects Endpoints to the WebApplication Map
        /// </summary>
        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();

            foreach (var endpointDefinition in definitions)
            {
                endpointDefinition.DefineEndpoints(app);
            }
        }
    }
}
