using RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Application.Creatures.Services;
using RoleRollsPocketEdition.Application.CreaturesTemplates.Services;
using RoleRollsPocketEdition.Application.Rolls.Application;
using RoleRollsPocketEdition.Application.Scenes.Services;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Domain.Campaigns;
using RoleRollsPocketEdition.Domain.Rolls.Services;

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
            services.AddScoped<IRollSimulationService, RollSimulationService>();
            services.AddScoped<IDefenseTemplateService, DefenseTemplateService>();
            services.AddScoped<ICampaignSceneHistoryBuilderService, CampaignSceneHistoryBuilderService>();
            return services;
        }
    }
}
