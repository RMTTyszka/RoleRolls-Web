using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere.CampaignTemplates;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.TheFutureIsOutThere;

public class TheFutureIsOutThereLoader : IStartupTask
{
    private readonly RoleRollsDbContext _dbContext;

    public TheFutureIsOutThereLoader(RoleRollsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var templateFromCode = TheFutureIsOutThereTemplate.Template;
        var templateFromDb = await _dbContext.CampaignTemplates
            .FirstOrDefaultAsync(t => t.Id == templateFromCode.Id, cancellationToken);

        if (templateFromDb == null)
        {
            _dbContext.CampaignTemplates.Add(templateFromCode);
        }
        else
        {
            var attributes = await _dbContext.CampaignTemplates
                .Include(t => t.Attributes)
                .Where(t => t.Id == templateFromCode.Id)
                .Select(t => t.Attributes)
                .FirstAsync(cancellationToken);

            var skills = await _dbContext.CampaignTemplates
                .Include(t => t.Skills)
                .ThenInclude(s => s.SpecificSkillTemplates)
                .Where(t => t.Id == templateFromCode.Id)
                .Select(t => t.Skills)
                .FirstAsync(cancellationToken);

            templateFromDb.Name = templateFromCode.Name;
            templateFromDb.Default = templateFromCode.Default;
            templateFromDb.ArchetypeTitle = templateFromCode.ArchetypeTitle;
            templateFromDb.CreatureTypeTitle = templateFromCode.CreatureTypeTitle;
            templateFromDb.Attributes = attributes;
            templateFromDb.Skills = skills;

            await SynchronizeAttributes(templateFromDb, templateFromCode.Attributes, templateFromDb.Attributes,
                _dbContext);
            await SynchronizeSkills(templateFromDb, templateFromCode.Skills, templateFromDb.Skills, _dbContext);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static async Task SynchronizeAttributes(CampaignTemplate templateFromDb,
        List<AttributeTemplate> fromCode,
        List<AttributeTemplate> fromDb,
        RoleRollsDbContext context)
    {
        var dbAttributes = fromDb.ToDictionary(a => a.Id);
        var codeAttributes = fromCode.ToDictionary(a => a.Id);

        foreach (var codeAttr in fromCode)
        {
            if (!dbAttributes.TryGetValue(codeAttr.Id, out var dbAttr))
            {
                await templateFromDb.AddAttributeAsync(new AttributeTemplateModel(codeAttr), context);
                continue;
            }

            dbAttr.Update(new AttributeTemplateModel(codeAttr));
            dbAttr.Name = codeAttr.Name;
            context.AttributeTemplates.Update(dbAttr);
        }

        foreach (var dbAttr in fromDb.Where(a => !codeAttributes.ContainsKey(a.Id)).ToList())
        {
            templateFromDb.RemoveAttribute(dbAttr.Id, context);
        }
    }

    private static async Task SynchronizeSkills(CampaignTemplate templateFromDb,
        List<SkillTemplate> fromCode,
        List<SkillTemplate> fromDb,
        RoleRollsDbContext context)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                templateFromDb.Skills.Add(codeSkill);
                await context.SkillTemplates.AddAsync(codeSkill);
                continue;
            }

            await SynchronizeMinorSkills(dbSkill, codeSkill.SpecificSkillTemplates, dbSkill.SpecificSkillTemplates,
                context);
            dbSkill.Name = codeSkill.Name;
            dbSkill.AttributeTemplateId = codeSkill.AttributeTemplateId;
            context.SkillTemplates.Update(dbSkill);
        }

        foreach (var dbSkill in fromDb.Where(s => !codeSkills.ContainsKey(s.Id)).ToList())
        {
            templateFromDb.RemoveSkill(dbSkill.Id, context);
        }
    }

    private static async Task SynchronizeMinorSkills(SkillTemplate dbSkill,
        List<SpecificSkillTemplate> fromCode,
        List<SpecificSkillTemplate> fromDb,
        RoleRollsDbContext context)
    {
        var dbMinorSkills = fromDb.ToDictionary(ms => ms.Id);
        var codeMinorSkills = fromCode.ToDictionary(ms => ms.Id);

        foreach (var dbMinorSkill in fromDb.Where(ms => !codeMinorSkills.ContainsKey(ms.Id)).ToList())
        {
            fromDb.Remove(dbMinorSkill);
            context.MinorSkillTemplates.Remove(dbMinorSkill);
        }

        foreach (var codeMinorSkill in fromCode)
        {
            if (!dbMinorSkills.TryGetValue(codeMinorSkill.Id, out var dbMinorSkill))
            {
                var newMinorSkill = new SpecificSkillTemplate
                {
                    Id = codeMinorSkill.Id,
                    Name = codeMinorSkill.Name,
                    SkillTemplateId = dbSkill.Id,
                    AttributeTemplateId = codeMinorSkill.AttributeTemplateId
                };
                await dbSkill.AddMinorSkillAsync(newMinorSkill, context);
                continue;
            }

            dbMinorSkill.Name = codeMinorSkill.Name;
            dbMinorSkill.SkillTemplateId = dbSkill.Id;
            dbMinorSkill.AttributeTemplateId = codeMinorSkill.AttributeTemplateId;
            context.MinorSkillTemplates.Update(dbMinorSkill);
        }
    }
}
