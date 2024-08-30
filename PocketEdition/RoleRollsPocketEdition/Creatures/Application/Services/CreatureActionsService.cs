using RoleRollsPocketEdition.Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Domain.Campaigns.Repositories;
using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Application.Services;

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
        var result = creature.TakeDamage(input.LifeId, input.Value);
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