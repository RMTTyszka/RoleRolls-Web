using RoleRollsPocketEdition.Campaigns;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Creatures.Services;
using RoleRollsPocketEdition.Rolls;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Services;
using RoleRollsPocketEdition.Templates.Services;

namespace RoleRollsPocketEdition.Core.Configuration
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
            services.AddScoped<IRollSimulationService, RollSimulationService>();
            services.AddScoped<IDefenseTemplateService, DefenseTemplateService>();
            services.AddScoped<ICampaignSceneHistoryBuilderService, CampaignSceneHistoryBuilderService>();
            return services;
        }
    }
}
