using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Campaigns.Repositories;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Creatures.Dtos;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Entities;

namespace RoleRollsPocketEdition.Creatures.Services;

public interface ICreatureActionsService
{
    Task TakeDamage(Guid campaignId, Guid sceneId, Guid creatureId, UpdateLifeInput input);
    Task Heal(Guid campaignId, Guid sceneId, Guid creatureId, UpdateLifeInput input);
}

public class CreatureActionsService : ICreatureActionsService, ITransientDependency
{
    private readonly ICreatureRepository _creatureRepository;
    private readonly ICampaignSceneHistoryBuilderService _campaignSceneHistoryBuilderService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleRollsDbContext _dbContext;
    private readonly ISceneNotificationService _notificationService;

    public CreatureActionsService(ICreatureRepository creatureRepository, ICampaignSceneHistoryBuilderService campaignSceneHistoryBuilderService, IUnitOfWork unitOfWork, RoleRollsDbContext dbContext, ISceneNotificationService notificationService)
    {
        _creatureRepository = creatureRepository;
        _campaignSceneHistoryBuilderService = campaignSceneHistoryBuilderService;
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
        _notificationService = notificationService;
    }

    public async Task TakeDamage(Guid campaignId, Guid sceneId, Guid creatureId, UpdateLifeInput input)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        var takeDamageResult = creature.TakeDamage(input.LifeId, input.Value);
        var result = new SceneAction
        {
            Description = $"{takeDamageResult.Name} took {takeDamageResult.Value} of {takeDamageResult.Life} damage",
            ActorType = creature.Category switch
            {
                CreatureCategory.Hero => ActionActorType.Hero,
                CreatureCategory.Monster => ActionActorType.Monster,
                _ => throw new ArgumentOutOfRangeException()
            },
            Id = Guid.NewGuid(),
            ActorId = creatureId,
            SceneId = sceneId
        };
        var history = await _campaignSceneHistoryBuilderService.BuildHistory(result);
        using (_unitOfWork.Begin())
        {
            _creatureRepository.Update(creature);
            await _dbContext.SceneActions.AddAsync(result);
            _unitOfWork.Commit();
        }
        await _notificationService.NotifyScene(sceneId, history);

    }      
    public async Task Heal(Guid campaignId, Guid sceneId, Guid creatureId, UpdateLifeInput input)
    {
        var creature = await _creatureRepository.GetFullCreature(creatureId);
        var result = creature.Heal(input.LifeId, input.Value);
        result.SceneId = sceneId;
        var history = await _campaignSceneHistoryBuilderService.BuildHistory(result);
        using (_unitOfWork.Begin())
        {
            _creatureRepository.Update(creature);
            await _dbContext.SceneActions.AddAsync(result);
            _unitOfWork.Commit();
        }
        await _notificationService.NotifyScene(sceneId, history);
    }
}