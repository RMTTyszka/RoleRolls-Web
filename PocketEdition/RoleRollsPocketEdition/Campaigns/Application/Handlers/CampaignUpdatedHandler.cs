using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Events;
using RoleRollsPocketEdition.Infrastructure;
using Attribute = RoleRollsPocketEdition.Creatures.Domain.Entities.Attribute;

namespace RoleRollsPocketEdition.Campaigns.Application.Handlers;

public class CampaignUpdatedHandler : 
    IConsumer<AttributeAdded>,
    IConsumer<AttributeUpdated>,
    IConsumer<AttributeRemoved>
{
    private readonly RoleRollsDbContext _dbContext;

    public CampaignUpdatedHandler(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<AttributeAdded> context)
    {
        var template = await
            _dbContext.CreatureTemplates
                .Include(template => template.Attributes).FirstAsync(template => template.Id == context.Message.CreatureTemplateId);
        var creatures = await _dbContext.Creatures.Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        var attribute = template.Attributes.First(attribute => attribute.Id == context.Message.Attribute.Id);
        foreach (var creature in creatures)
        {
            var newAttribute = new Attribute(attribute);
            creature.Attributes.Add(newAttribute);
            await _dbContext.Attributes.AddAsync(newAttribute);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<AttributeUpdated> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var attributeToUpdate =
                creature.Attributes.First(attribute => attribute.AttributeTemplateId == context.Message.Attribute.Id);
            attributeToUpdate.Name = context.Message.Attribute.Name;
            _dbContext.Attributes.Update(attributeToUpdate);
        }
        await _dbContext.SaveChangesAsync();
        
    }

    public async Task Consume(ConsumeContext<AttributeRemoved> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var attribute =
                creature.Attributes.First(attribute => attribute.AttributeTemplateId == context.Message.AttributeId);
            creature.Attributes.Remove(attribute);
            _dbContext.Attributes.Remove(attribute);

            var skills = creature.Skills.Where(skill => skill.AttributeId == attribute.Id).ToList();
            foreach (var skill in skills)
            {

                creature.Skills.Remove(skill);
                _dbContext.Skills.Remove(skill);
                var minorSkills = skill.MinorSkills.ToList();
                foreach (var minorSkill in minorSkills)
                {
                    skill.MinorSkills.Remove(minorSkill);
                    _dbContext.MinorSkills.Remove(minorSkill);
                }
            }
        }
        await _dbContext.SaveChangesAsync();
    }
}