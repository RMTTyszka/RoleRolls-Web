using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Domain.Campaigns.Events.Attributes;
using RoleRollsPocketEdition.Domain.Campaigns.Events.Lifes;
using RoleRollsPocketEdition.Domain.Campaigns.Events.MinorSkills;
using RoleRollsPocketEdition.Domain.Campaigns.Events.Skills;
using RoleRollsPocketEdition.Domain.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using Attribute = RoleRollsPocketEdition.Domain.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.Application.Campaigns.Handlers;

public class CampaignUpdatedHandler : 
    IConsumer<AttributeAdded>,
    IConsumer<AttributeUpdated>,
    IConsumer<AttributeRemoved>,
    IConsumer<SkillAdded>,
    IConsumer<SkillUpdated>,
    IConsumer<SkillRemoved>,  
IConsumer<MinorSkillAdded>,
    IConsumer<MinorSkillUpdated>,
    IConsumer<MinorSkillRemoved>,
    IConsumer<LifeAdded>,
    IConsumer<LifeUpdated>,
    IConsumer<LifeRemoved>
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

    public async Task Consume(ConsumeContext<SkillAdded> context)
    {
        var template = await
            _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .FirstAsync(template => template.Id == context.Message.CreatureTemplateId);
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        var skill = template.Skills.First(skill => skill.Id == context.Message.Skill.Id);
        foreach (var creature in creatures)
        {
            var attribute = creature.Attributes.First(attribute => attribute.AttributeTemplateId == skill.AttributeId);
            var newSkill = new Skill(skill, attribute);
            creature.Skills.Add(newSkill);
            await _dbContext.Skills.AddAsync(newSkill);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<SkillUpdated> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var skillToUpdate =
                creature.Skills.First(skill => skill.SkillTemplateId == context.Message.Skill.Id);
            skillToUpdate.Name = context.Message.Skill.Name;
            _dbContext.Skills.Update(skillToUpdate);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<SkillRemoved> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var skill =
                creature.Skills.First(skill => skill.SkillTemplateId == context.Message.SkillId);
            creature.Skills.Remove(skill);
            _dbContext.Skills.Remove(skill);

            var minorSkills = skill.MinorSkills.ToList();
            foreach (var minorSkill in minorSkills)
            {
                skill.MinorSkills.Remove(minorSkill);
                _dbContext.MinorSkills.Remove(minorSkill);
            }
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<MinorSkillAdded> context)
    {
        var template = await
            _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .FirstAsync(template => template.Id == context.Message.CreatureTemplateId);
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        var minorSkill = template.Skills.First(skill => skill.Id == context.Message.MinorSkill.SkillTemplateId).MinorSkills
            .First(minorSkill => minorSkill.Id == context.Message.MinorSkill.Id);
        foreach (var creature in creatures)
        {
            var newMinorSkill = new MinorSkill(minorSkill);
            creature.Skills.First(skill => skill.SkillTemplateId == context.Message.MinorSkill.SkillTemplateId).MinorSkills.Add(newMinorSkill);
            await _dbContext.MinorSkills.AddAsync(newMinorSkill);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<MinorSkillUpdated> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var minorSkillToUpdate =
                creature.Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.MinorSkillTemplateId == context.Message.MinorSkill.Id);
            minorSkillToUpdate.Name = context.Message.MinorSkill.Name;
            _dbContext.MinorSkills.Update(minorSkillToUpdate);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<MinorSkillRemoved> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var minorSkill =
                creature.Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.MinorSkillTemplateId == context.Message.MinorSkillId);
            _dbContext.MinorSkills.Remove(minorSkill);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<LifeAdded> context)
    {
        var template = await
            _dbContext.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Lifes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .FirstAsync(template => template.Id == context.Message.CreatureTemplateId);
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Attributes)
            .Include(creature => creature.Skills)
            .ThenInclude(skill => skill.MinorSkills)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        var life = template.Lifes.First(life => life.Id == context.Message.Life.Id);
        foreach (var creature in creatures)
        {
            var newLife = new Life(life);
            creature.Lifes.Add(newLife);
            await _dbContext.Lifes.AddAsync(newLife);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<LifeUpdated> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Lifes)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var lifeToUpdated =
                creature.Lifes.First(attribute => attribute.LifeTemplateId == context.Message.Life.Id);
            lifeToUpdated.Name = context.Message.Life.Name;
            lifeToUpdated.Formula = context.Message.Life.Formula;
            _dbContext.Lifes.Update(lifeToUpdated);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task Consume(ConsumeContext<LifeRemoved> context)
    {
        var creatures = await _dbContext.Creatures
            .Include(creature => creature.Lifes)
            .Where(creature => creature.CampaignId == context.Message.CampaignId)
            .ToListAsync();
        foreach (var creature in creatures)
        {
            var life =
                creature.Lifes.First(life => life.LifeTemplateId == context.Message.LifeId);
            creature.Lifes.Remove(life);
            _dbContext.Lifes.Remove(life);
        }
        await _dbContext.SaveChangesAsync();
    }
}