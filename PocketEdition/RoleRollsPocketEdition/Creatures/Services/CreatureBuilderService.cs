using RoleRollsPocketEdition.Campaigns;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Services;

public interface ICreatureBuilderService
{
    Task<CreatureUpdateValidationResult> BuildCreature(Guid campaignId, CreatureModel creatureModel);
}

public class CreatureBuilderService : ICreatureBuilderService, ITransientDependency
{
    private readonly ICurrentUser _currentUser;
    private readonly RoleRollsDbContext _dbContext;
    private readonly ICampaignRepository _campaignRepository;

    public CreatureBuilderService(ICurrentUser currentUser, RoleRollsDbContext dbContext, ICampaignRepository campaignRepository)
    {
        _currentUser = currentUser;
        _dbContext = dbContext;
        _campaignRepository = campaignRepository;
    }

    public async Task<CreatureUpdateValidationResult> BuildCreature(Guid campaignId, CreatureModel creatureModel)
    {
        var ownerId = _currentUser.User.Id;
        var campaign = await _dbContext.Campaigns.FindAsync(campaignId);
        var creatureTemplate = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);
        var creature = creatureTemplate.InstantiateCreature(creatureModel.Name, creatureModel.Id, campaignId, creatureModel.Category,
            ownerId, creatureModel.IsTemplate);
        var result = creature.Update(creatureModel);
        if (result.Validation == CreatureUpdateValidation.Ok)
        {
            creature.FullRestore();
            return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null)
            {
                Creature = creature,
            };
        }

        return result;

    }
}