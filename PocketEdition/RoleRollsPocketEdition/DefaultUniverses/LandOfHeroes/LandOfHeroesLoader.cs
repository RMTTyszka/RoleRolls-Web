using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplate;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
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
        .Include(t => t.Attributes)
        .ThenInclude(a => a.SkillTemplates)
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
        SynchronizeItemConfiguration(templateFromDb, templateFromCode.ItemConfiguration, templateFromDb.ItemConfiguration);
    }
    await _context.SaveChangesAsync();
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
            fromDb.Add(codeAttr);
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

}
