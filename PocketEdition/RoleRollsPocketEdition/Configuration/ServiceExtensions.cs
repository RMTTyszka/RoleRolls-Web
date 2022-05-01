using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Configuration
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<ICreatureTemplateService, CreatureTemplateService>();
            return services;
        }
    }
}
