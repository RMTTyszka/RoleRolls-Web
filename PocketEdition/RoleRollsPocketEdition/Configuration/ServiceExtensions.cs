using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain;
using RoleRollsPocketEdition.Campaigns.Domain.Services;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Scenes.Application.Services;

namespace RoleRollsPocketEdition.Configuration
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddTransient<ICreatureTemplateService, CreatureTemplateService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICampaignsService, CampaignsService>();
            services.AddTransient<ICampaignRepository, CampaignRepository>();
            services.AddTransient<IRollService, RollService>();
            services.AddTransient<ICreatureService, CreatureService>();
            services.AddTransient<IScenesService, ScenesService>();
            services.AddScoped<ICurrentUser, CurrentUser>();
            return services;
        }
    }
}
