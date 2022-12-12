using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Campaigns.Domain.Services;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Configuration
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<ICreatureTemplateService, CreatureTemplateService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICampaignsService, CampaignsService>();
            services.AddScoped<ICampaignRepository, CampaignRepository>();
            return services;
        }
    }
}
