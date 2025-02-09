using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplate;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;

public class LandOfHeroesLoader : IStartupTask
{
    private readonly RoleRollsDbContext _context;

    public LandOfHeroesLoader(RoleRollsDbContext dbContext)
    {
        _context = dbContext;
    }
    
    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var templateFromCode = LandOfHeroesTemplate.Template;
        var templateFromDb = await _context.CampaignTemplates
            .FirstOrDefaultAsync(t => t.Id == templateFromCode.Id, cancellationToken: cancellationToken);

        if (templateFromDb == null)
        {
            _context.CampaignTemplates.Add(templateFromCode);
        }
        else
        {
            templateFromDb.Name = templateFromCode.Name;
            templateFromDb.Default = templateFromCode.Default;
            await SynchronizeAttributes(templateFromDb, templateFromCode.Attributes, templateFromDb.Attributes, _context);
            await SynchronizeAttributelessSkills(templateFromDb, templateFromCode.AttributelessSkills, templateFromDb.AttributelessSkills, _context);
            await SynchronizeItemConfiguration(templateFromDb, templateFromCode.ItemConfiguration, templateFromDb.ItemConfiguration, _context);
            await SynchronizeLives(templateFromDb, templateFromCode.Lifes, templateFromDb.Lifes, _context);
            await SynchronizeDamageTypes(templateFromDb, templateFromCode.DamageTypes, templateFromDb.DamageTypes, _context);
            await SynchronizeCreatureTypes(templateFromDb, templateFromCode.CreatureTypes, templateFromDb.CreatureTypes, _context);
        }

        var bonus = _context.ChangeTracker.Entries().Select(e => e.Entity).OfType<Bonus>().ToList();
        await _context.SaveChangesAsync(cancellationToken);
    }

    private async Task SynchronizeAttributelessSkills(Templates.Entities.CampaignTemplate templateFromDb,
        List<SkillTemplate> fromCode, List<SkillTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                templateFromDb.AttributelessSkills.Add(codeSkill);
                await context.SkillTemplates.AddAsync(codeSkill);
            }
            else
            {
                await SynchronizeMinorSkills(codeSkill, codeSkill.MinorSkills, dbSkill.MinorSkills, context);
                dbSkill.Name = codeSkill.Name;
                context.SkillTemplates.Update(dbSkill);
            }
        }

        foreach (var dbSkill in fromDb.Where(s => !codeSkills.ContainsKey(s.Id)).ToList())
        {
            fromDb.Remove(dbSkill);
        }
    }

    private async Task SynchronizeAttributes(Templates.Entities.CampaignTemplate templateFromDb,
        List<AttributeTemplate> fromCode,
        List<AttributeTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbAttributes = fromDb.ToDictionary(a => a.Id);
        var codeAttributes = fromCode.ToDictionary(a => a.Id);

        foreach (var codeAttr in fromCode)
        {
            if (!dbAttributes.TryGetValue(codeAttr.Id, out var dbAttr))
            {
                await templateFromDb.AddAttributeAsync(new AttributeTemplateModel(codeAttr), context);
            }
            else
            {
                await SynchronizeSkills(codeAttr, codeAttr.SkillTemplates.ToList(), dbAttr.SkillTemplates.ToList(), context);
                dbAttr.Name = codeAttr.Name;
                context.AttributeTemplates.Update(dbAttr);
            }
        }

        foreach (var dbAttr in fromDb.Where(a => !codeAttributes.ContainsKey(a.Id)).ToList())
        {
            fromDb.Remove(dbAttr);
        }
    }

    private async Task SynchronizeSkills(AttributeTemplate attributeTemplate, List<SkillTemplate> fromCode,
        List<SkillTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                await attributeTemplate.AddSkill(new SkillTemplateModel(codeSkill), context);
            }
            else
            {
                await SynchronizeMinorSkills(codeSkill, codeSkill.MinorSkills, dbSkill.MinorSkills, context);
                dbSkill.Name = codeSkill.Name;
                dbSkill.AttributeTemplateId = codeSkill.AttributeTemplateId;
                context.SkillTemplates.Update(dbSkill);
            }
        }

        foreach (var dbSkill in fromDb.Where(s => !codeSkills.ContainsKey(s.Id)).ToList())
        {
            fromDb.Remove(dbSkill);
        }
    }

    private async Task SynchronizeMinorSkills(SkillTemplate codeSkill, List<MinorSkillTemplate> fromCode,
        List<MinorSkillTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbMinorSkills = fromDb.ToDictionary(ms => ms.Id);
        var codeMinorSkills = fromCode.ToDictionary(ms => ms.Id);

        foreach (var codeMinorSkill in fromCode)
        {
            if (!dbMinorSkills.TryGetValue(codeMinorSkill.Id, out var dbMinorSkill))
            {
                await codeSkill.AddMinorSkillAsync(codeMinorSkill, context);
            }
            else
            {
                dbMinorSkill.Name = codeMinorSkill.Name;
                dbMinorSkill.SkillTemplateId = codeMinorSkill.SkillTemplateId;
                dbMinorSkill.AttributeId = codeMinorSkill.AttributeId;
                context.MinorSkillTemplates.Update(dbMinorSkill);
            }
        }

        foreach (var dbMinorSkill in fromDb.Where(ms => !codeMinorSkills.ContainsKey(ms.Id)).ToList())
        {
            fromDb.Remove(dbMinorSkill);
        }
    }

    private async Task SynchronizeItemConfiguration(Templates.Entities.CampaignTemplate templateFromDb,
        ItemConfiguration fromCode,
        ItemConfiguration fromDb, RoleRollsDbContext context)
    {
        if (fromDb == null)
        {
            templateFromDb.ItemConfiguration = fromCode;
            await context.ItemConfigurations.AddAsync(fromCode);
        }
        else
        {
            fromDb.CampaignTemplateId = fromCode.CampaignTemplateId;
            context.ItemConfigurations.Update(fromDb);
        }
    }
    private async Task SynchronizeLives(
        Templates.Entities.CampaignTemplate creatureFromDb,
        ICollection<LifeTemplate> fromCode,
        ICollection<LifeTemplate> fromDb, RoleRollsDbContext dbContext)
    {
        var dbLives = fromDb.ToDictionary(l => l.Id);
        var codeLives = fromCode.ToDictionary(l => l.Id);

        foreach (var codeLife in fromCode)
        {
            if (!dbLives.TryGetValue(codeLife.Id, out var dbLife))
            {
                await creatureFromDb.AddLifeAsync(new LifeTemplateModel(codeLife), dbContext);
            }
            else
            {
                dbLife.Name = codeLife.Name;
                dbLife.Formula = codeLife.Formula;
                creatureFromDb.UpdateLife(codeLife.Id, new LifeTemplateModel(codeLife), dbContext);
                dbContext.LifeTemplates.Update(dbLife);
            }
        }

        foreach (var dbLife in fromDb.Where(l => !codeLives.ContainsKey(l.Id)).ToList())
        {
            creatureFromDb.Lifes.Remove(dbLife);
        }
    }
    private async Task SynchronizeCreatureTypes(
        Templates.Entities.CampaignTemplate campaignFromDb,
        ICollection<CreatureType> fromCode,
        ICollection<CreatureType> fromDb,
        RoleRollsDbContext dbContext)
    {
        var dbCreatureTypes = fromDb.ToDictionary(c => c.Id);
        var codeCreatureTypes = fromCode.ToDictionary(c => c.Id);

        foreach (var codeCreature in fromCode)
        {
            if (!dbCreatureTypes.TryGetValue(codeCreature.Id, out var dbCreature))
            {
                await campaignFromDb.AddCreatureTypeAsync(new CreatureTypeModel(codeCreature), dbContext);
            }
            else
            {
                dbCreature.Name = codeCreature.Name;
                dbCreature.Description = codeCreature.Description;
                await campaignFromDb.UpdateCreatureType(new CreatureTypeModel(codeCreature), dbContext);
            }
        }

        foreach (var dbCreature in fromDb.Where(c => !codeCreatureTypes.ContainsKey(c.Id)).ToList())
        {
            campaignFromDb.CreatureTypes.Remove(dbCreature);
            dbContext.CreatureTypes.Remove(dbCreature);
        }
    }
    private async Task SynchronizeDamageTypes(Templates.Entities.CampaignTemplate creatureFromDb,
        ICollection<DamageType> fromCode,
        ICollection<DamageType> fromDb, RoleRollsDbContext context)
    {
        var dbDamageTypes = fromDb.ToDictionary(l => l.Id);
        var codeDamageTypes = fromCode.ToDictionary(l => l.Id);

        foreach (var codeDamageType in fromCode)
        {
            if (!dbDamageTypes.TryGetValue(codeDamageType.Id, out var dbDamageType))
            {
                await creatureFromDb.AddDamageTypeAsync(codeDamageType, context);
            }
            else
            {
                dbDamageType.Name = codeDamageType.Name;
                context.DamageTypes.Update(dbDamageType);
            }
        }

        foreach (var damageType in fromDb.Where(l => !codeDamageTypes.ContainsKey(l.Id)).ToList())
        {
            creatureFromDb.DamageTypes.Remove(damageType);
            context.DamageTypes.Remove(damageType);
        }
    }

}