using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition._Domain.DefaultUniverses.LandOfHeroes.CreatureTemplate;
using RoleRollsPocketEdition._Domain.Global;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.DefaultUniverses.LandOfHeroes;

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
            .FirstOrDefaultAsync(t => t.Id == templateFromCode.Id, cancellationToken: cancellationToken);

        if (templateFromDb == null)
        {
            _context.CampaignTemplates.Add(templateFromCode);
        }
        else
        {
            SynchronizeAttributes(templateFromCode.Attributes, templateFromDb.Attributes);
        }

        await _context.SaveChangesAsync();
    }

    private void SynchronizeAttributes(
        List<AttributeTemplate> fromCode,
        ICollection<AttributeTemplate> fromDb)
    {
        var dbAttributes = fromDb.ToDictionary(a => a.Id);
        var codeAttributes = fromCode.ToDictionary(a => a.Id);

        foreach (var codeAttr in fromCode)
        {
            if (!dbAttributes.TryGetValue(codeAttr.Id, out var dbAttr))
            {
                fromDb.Add(codeAttr);
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
}
