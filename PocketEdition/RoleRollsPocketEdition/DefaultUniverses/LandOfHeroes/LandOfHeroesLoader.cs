using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplate;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Configurations;
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
            .Include(t => t.Lifes)
            .Include(t => t.Attributes)
            .ThenInclude(a => a.SkillTemplates)
            .ThenInclude(s => s.MinorSkills)     
            .Include(a => a.AttributelessSkills)
            .ThenInclude(s => s.MinorSkills)
            .Include(t => t.ItemConfiguration)
            .FirstOrDefaultAsync(t => t.Id == templateFromCode.Id, cancellationToken: cancellationToken);

        if (templateFromDb == null)
        {
            _context.CampaignTemplates.Add(templateFromCode);
        }
        else
        {
            templateFromDb.Name = templateFromCode.Name;
            templateFromDb.Default = templateFromCode.Default;
            SynchronizeAttributes(templateFromDb, templateFromCode.Attributes, templateFromDb.Attributes);
            SynchronizeAttributelessSkills(templateFromDb, templateFromCode.AttributelessSkills, templateFromDb.AttributelessSkills);
            SynchronizeItemConfiguration(templateFromDb, templateFromCode.ItemConfiguration, templateFromDb.ItemConfiguration);
            SynchronizeLives(templateFromDb, templateFromCode.Lifes, templateFromDb.Lifes);
            SynchronizeDamageTypes(templateFromDb, templateFromCode.DamageTypes, templateFromDb.DamageTypes);
        }
        await _context.SaveChangesAsync();
    }

    private void SynchronizeAttributelessSkills(Templates.Entities.CampaignTemplate templateFromDb, List<SkillTemplate> fromCode, List<SkillTemplate> fromDb)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                templateFromDb.AttributelessSkills.Add(codeSkill);
                fromDb.Add(codeSkill);
            }
            else
            {
                SynchronizeMinorSkills(codeSkill.MinorSkills, dbSkill.MinorSkills);
                dbSkill.Name = codeSkill.Name;
            }
        }

        foreach (var dbSkill in fromDb.Where(s => !codeSkills.ContainsKey(s.Id)).ToList())
        {
            fromDb.Remove(dbSkill);
        }
    }

    private void SynchronizeAttributes(Templates.Entities.CampaignTemplate templateFromDb, List<AttributeTemplate> fromCode,
        ICollection<AttributeTemplate> fromDb)
    {
        var dbAttributes = fromDb.ToDictionary(a => a.Id);
        var codeAttributes = fromCode.ToDictionary(a => a.Id);

        foreach (var codeAttr in fromCode)
        {
            if (!dbAttributes.TryGetValue(codeAttr.Id, out var dbAttr))
            {
                templateFromDb.Attributes.Add(codeAttr);
            }
            else
            {
                SynchronizeSkills(codeAttr.SkillTemplates, dbAttr.SkillTemplates);
                dbAttr.Name = codeAttr.Name;
            }
        }

        foreach (var dbAttr in fromDb.Where(a => !codeAttributes.ContainsKey(a.Id)).ToList())
        {
            fromDb.Remove(dbAttr);
        }
    }

    private void SynchronizeSkills(
        ICollection<SkillTemplate> fromCode,
        ICollection<SkillTemplate> fromDb)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                fromDb.Add(codeSkill);
            }
            else
            {
                SynchronizeMinorSkills(codeSkill.MinorSkills, dbSkill.MinorSkills);
                dbSkill.Name = codeSkill.Name;
                dbSkill.AttributeTemplateId = codeSkill.AttributeTemplateId;
            }
        }

        foreach (var dbSkill in fromDb.Where(s => !codeSkills.ContainsKey(s.Id)).ToList())
        {
            fromDb.Remove(dbSkill);
        }
    }

    private void SynchronizeMinorSkills(
        ICollection<MinorSkillTemplate> fromCode,
        ICollection<MinorSkillTemplate> fromDb)
    {
        var dbMinorSkills = fromDb.ToDictionary(ms => ms.Id);
        var codeMinorSkills = fromCode.ToDictionary(ms => ms.Id);

        foreach (var codeMinorSkill in fromCode)
        {
            if (!dbMinorSkills.TryGetValue(codeMinorSkill.Id, out var dbMinorSkill))
            {
                fromDb.Add(codeMinorSkill);
            }
            else
            {
                dbMinorSkill.Name = codeMinorSkill.Name;
                dbMinorSkill.SkillTemplateId = codeMinorSkill.SkillTemplateId;
                dbMinorSkill.AttributeId = codeMinorSkill.AttributeId;
            }
        }

        foreach (var dbMinorSkill in fromDb.Where(ms => !codeMinorSkills.ContainsKey(ms.Id)).ToList())
        {
            fromDb.Remove(dbMinorSkill);
        }
    }

    private void SynchronizeItemConfiguration(
        Templates.Entities.CampaignTemplate templateFromDb,
        ItemConfiguration fromCode,
        ItemConfiguration fromDb)
    {
        if (fromDb == null)
        {
            templateFromDb.ItemConfiguration = fromCode;
        }
        else
        {
            fromDb.CampaignTemplateId = fromCode.CampaignTemplateId;
        }
    }
    private void SynchronizeLives(
        Templates.Entities.CampaignTemplate creatureFromDb,
        ICollection<LifeTemplate> fromCode,
        ICollection<LifeTemplate> fromDb)
    {
        var dbLives = fromDb.ToDictionary(l => l.Id);
        var codeLives = fromCode.ToDictionary(l => l.Id);

        foreach (var codeLife in fromCode)
        {
            if (!dbLives.TryGetValue(codeLife.Id, out var dbLife))
            {
                codeLife.CampaignTemplate = creatureFromDb;
                creatureFromDb.Lifes.Add(codeLife);
            }
            else
            {
                dbLife.Name = codeLife.Name;
                dbLife.Formula = codeLife.Formula;
            }
        }

        foreach (var dbLife in fromDb.Where(l => !codeLives.ContainsKey(l.Id)).ToList())
        {
            creatureFromDb.Lifes.Remove(dbLife);
        }
    }
    private void SynchronizeDamageTypes(
        Templates.Entities.CampaignTemplate creatureFromDb,
        ICollection<DamageType> fromCode,
        ICollection<DamageType> fromDb)
    {
        var dbLives = fromDb.ToDictionary(l => l.Id);
        var codeLives = fromCode.ToDictionary(l => l.Id);

        foreach (var codeLife in fromCode)
        {
            if (!dbLives.TryGetValue(codeLife.Id, out var dbLife))
            {
                creatureFromDb.DamageTypes.Add(codeLife);
            }
            else
            {
                dbLife.Name = codeLife.Name;
            }
        }

        foreach (var dbLife in fromDb.Where(l => !codeLives.ContainsKey(l.Id)).ToList())
        {
            creatureFromDb.DamageTypes.Remove(dbLife);
        }
    }

}