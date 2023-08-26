using EFCore.BulkExtensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreaturesTemplates.Domain.Events;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Campaigns.Application.Handlers;

public class DefenseTemplateUpdatedHandler : 
    IConsumer<DefenseTemplateAdded>,
    IConsumer<DefenseTemplateUpdated>,
    IConsumer<DefenseTemplateRemoved>
{
    private readonly RoleRollsDbContext _dbContext;

    public DefenseTemplateUpdatedHandler(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<DefenseTemplateAdded> context)
    {
        var creaturesQuery = from campaign in _dbContext.Campaigns
            join creature in _dbContext.Creatures on campaign.Id equals creature.CampaignId
            select creature;
        var creatures = await creaturesQuery.ToListAsync();
        foreach (var creature in creatures)
        {
            var defense = Defense.FromTemplate(context.Message.DefenseTemplateModel);
            await creature.AddDefenseAsync(defense, _dbContext);
        }
        await _dbContext.SaveChangesAsync();
    }
    public async Task Consume(ConsumeContext<DefenseTemplateUpdated> context)
    {
        var creaturesQuery = from campaign in _dbContext.Campaigns
            join creature in _dbContext.Creatures on campaign.Id equals creature.CampaignId
            join creatureTemplate in _dbContext.CreatureTemplates on campaign.CreatureTemplateId equals creatureTemplate.Id
            where creatureTemplate.CreatureTemplateId == context.Message.DefenseTemplateModel.Id
            select creature;
        var creatures = await creaturesQuery.ToListAsync();
        foreach (var creature in creatures)
        {
            await creature.UpdateDefense(context.Message.DefenseTemplateModel, _dbContext);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<DefenseTemplateRemoved> context)
    {
        await _dbContext.Defenses
            .Where(defense => defense.DefenseTemplateId == context.Message.DefenseTemplateId).BatchDeleteAsync();
        await _dbContext.SaveChangesAsync();

    }
}