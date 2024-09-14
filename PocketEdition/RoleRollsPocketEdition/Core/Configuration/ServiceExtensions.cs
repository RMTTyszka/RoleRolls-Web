using RoleRollsPocketEdition._Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition._Application.Creatures.Services;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Services;
using RoleRollsPocketEdition._Application.Rolls.Application;
using RoleRollsPocketEdition._Application.Scenes.Services;
using RoleRollsPocketEdition._Domain.Campaigns;
using RoleRollsPocketEdition._Domain.Rolls.Services;
using RoleRollsPocketEdition.Authentication.Application.Services;

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
