using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Damages.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Archetypes;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Spells;
using RoleRollsPocketEdition.Spells.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Powers.Entities;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;

public class LandOfHeroesLoader : IStartupTask
{
    private readonly RoleRollsDbContext _dbContext;
    private readonly ICampaignsService _campaignsService;

    public LandOfHeroesLoader(RoleRollsDbContext dbDbContext, ICampaignsService campaignsService)
    {
        _dbContext = dbDbContext;
        _campaignsService = campaignsService;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var templateFromCode = LandOfHeroesTemplate.Template;
        var templateFromDb = await _dbContext.CampaignTemplates
            .FirstOrDefaultAsync(t => t.Id == templateFromCode.Id, cancellationToken: cancellationToken);


        if (templateFromDb == null)
        {
            _dbContext.CampaignTemplates.Add(templateFromCode);
        }
        else
        {
            var attributes = await _dbContext.CampaignTemplates
                .Include(a => a.Attributes)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.Attributes)
                .FirstAsync(cancellationToken);

            var skills = await _dbContext.CampaignTemplates
                .Include(a => a.Skills)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.Skills)
                .FirstAsync(cancellationToken);

            var defenses = await _dbContext.CampaignTemplates
                .Include(t => t.Defenses)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.Defenses)
                .FirstAsync(cancellationToken);

            var vitalities = await _dbContext.CampaignTemplates
                .Include(t => t.Vitalities)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.Vitalities)
                .FirstAsync(cancellationToken);


            var archetypes = await _dbContext.CampaignTemplates
                .Include(t => t.Archetypes)
                .ThenInclude(t => t.PowerDescriptions)
                .Include(t => t.Archetypes)
                .ThenInclude(t => t.Spells)
                .ThenInclude(s => s.Circles)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.Archetypes)
                .FirstAsync(cancellationToken);

            var creatureTypes = await _dbContext.CampaignTemplates
                .Include(t => t.CreatureTypes)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.CreatureTypes)
                .FirstAsync(cancellationToken);

            var damageTypes = await _dbContext.CampaignTemplates
                .Include(t => t.DamageTypes)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.DamageTypes)
                .FirstAsync(cancellationToken);

            var itemConfiguration = await _dbContext.CampaignTemplates
                .Include(c => c.ItemConfiguration)
                .Where(e => e.Id == templateFromCode.Id)
                .Select(e => e.ItemConfiguration)
                .FirstAsync(cancellationToken);

            templateFromDb.Name = templateFromCode.Name;
            templateFromDb.Default = templateFromCode.Default;
            templateFromDb.ItemConfiguration = itemConfiguration;
            templateFromDb.DamageTypes = damageTypes;
            templateFromDb.CreatureTypes = creatureTypes;
            templateFromDb.Archetypes = archetypes;
            templateFromDb.Attributes = attributes;
            templateFromDb.Skills = skills;
            templateFromDb.Defenses = defenses;
            templateFromDb.Vitalities = vitalities;
            await SynchronizeAttributes(templateFromDb, templateFromCode.Attributes, templateFromDb.Attributes,
                _dbContext);
            await SynchronizeSkills(templateFromDb, templateFromCode.Skills,
                templateFromDb.Skills, _dbContext);
            await SynchronizeItemConfiguration(templateFromDb, templateFromCode.ItemConfiguration,
                templateFromDb.ItemConfiguration, _dbContext);
            await SynchronizeLives(templateFromDb, templateFromCode.Vitalities, templateFromDb.Vitalities, _dbContext);
            await SynchronizeDamageTypes(templateFromDb, templateFromCode.DamageTypes, templateFromDb.DamageTypes,
                _dbContext);
            await SynchronizeCreatureTypes(templateFromDb, templateFromCode.CreatureTypes, templateFromDb.CreatureTypes,
                _dbContext);
            await SynchronizeArchetypes(templateFromDb, templateFromCode.Archetypes, templateFromDb.Archetypes,
                _dbContext);
            await SynchronizeAllArchetypeSpells(templateFromDb, _dbContext);
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task SynchronizeArchetypes(CampaignTemplate templateFromDb, List<Archetype> fromCode,
        List<Archetype> fromDb, RoleRollsDbContext dbContext)
    {
        var dbCreatureTypes = fromDb.ToDictionary(c => c.Id);
        var codeCreatureTypes = fromCode.ToDictionary(c => c.Id);

        foreach (var codeCreature in fromCode)
        {
            if (!dbCreatureTypes.TryGetValue(codeCreature.Id, out var dbCreature))
            {
                await templateFromDb.AddArchetypeAsync(new ArchetypeModel(codeCreature), dbContext);
            }
            else
            {
                dbCreature.Name = codeCreature.Name;
                dbCreature.Description = codeCreature.Description;
                await templateFromDb.UpdateArchetypeAsync(new ArchetypeModel(codeCreature), dbContext);
            }
        }

        foreach (var dbCreature in fromDb.Where(c => !codeCreatureTypes.ContainsKey(c.Id)).ToList())
        {
            templateFromDb.Archetypes.Remove(dbCreature);
            dbContext.Archetypes.Remove(dbCreature);
        }
    }

    private async Task SynchronizeSkills(Templates.Entities.CampaignTemplate templateFromDb,
        List<SkillTemplate> fromCode, List<SkillTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbSkills = fromDb.ToDictionary(s => s.Id);
        var codeSkills = fromCode.ToDictionary(s => s.Id);

        foreach (var codeSkill in fromCode)
        {
            if (!dbSkills.TryGetValue(codeSkill.Id, out var dbSkill))
            {
                templateFromDb.Skills.Add(codeSkill);
                await context.SkillTemplates.AddAsync(codeSkill);
            }
            else
            {
                await SynchronizeMinorSkills(codeSkill, codeSkill.SpecificSkillTemplates,
                    dbSkill.SpecificSkillTemplates, context);
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
                dbAttr.Update(new AttributeTemplateModel(codeAttr));
                dbAttr.Name = codeAttr.Name;
                context.AttributeTemplates.Update(dbAttr);
            }
        }

        foreach (var dbAttr in fromDb.Where(a => !codeAttributes.ContainsKey(a.Id)).ToList())
        {
            fromDb.Remove(dbAttr);
        }
    }

    // attribute-level skills synchronization removed; skills are synchronized at template level

    private async Task SynchronizeMinorSkills(SkillTemplate codeSkill, List<SpecificSkillTemplate> fromCode,
        List<SpecificSkillTemplate> fromDb, RoleRollsDbContext context)
    {
        var dbMinorSkills = fromDb.ToDictionary(ms => ms.Id);
        var codeMinorSkills = fromCode.ToDictionary(ms => ms.Id);
        foreach (var dbMinorSkill in fromDb.Where(ms => !codeMinorSkills.ContainsKey(ms.Id)).ToList())
        {
            fromDb.Remove(dbMinorSkill);
            context.MinorSkillTemplates.Remove(dbMinorSkill);
            await context.SaveChangesAsync();
        }

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
                dbMinorSkill.AttributeTemplateId = codeMinorSkill.AttributeTemplateId;
                context.MinorSkillTemplates.Update(dbMinorSkill);
            }
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
            fromDb.Update(ItemConfigurationModel.FromConfiguration(fromCode));
            context.ItemConfigurations.Update(fromDb);
        }
    }

    private async Task SynchronizeLives(
        Templates.Entities.CampaignTemplate creatureFromDb,
        ICollection<VitalityTemplate> fromCode,
        ICollection<VitalityTemplate> fromDb, RoleRollsDbContext dbContext)
    {
        var dbLives = fromDb.ToDictionary(l => l.Id);
        var codeLives = fromCode.ToDictionary(l => l.Id);

        foreach (var codeVitality in fromCode)
        {
            if (!dbLives.TryGetValue(codeVitality.Id, out var dbVitality))
            {
                await creatureFromDb.AddVitalityAsync(new VitalityTemplateModel(codeVitality), dbContext);
            }
            else
            {
                creatureFromDb.UpdateVitality(codeVitality.Id, new VitalityTemplateModel(codeVitality), dbContext);
                dbContext.VitalityTemplates.Update(dbVitality);
            }
        }

        foreach (var dbVitality in fromDb.Where(l => !codeLives.ContainsKey(l.Id)).ToList())
        {
            creatureFromDb.Vitalities.Remove(dbVitality);
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
                await campaignFromDb.UpdateCreatureType(codeCreature.Id, new CreatureTypeModel(codeCreature),
                    dbContext);
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

    private async Task SynchronizeAllArchetypeSpells(Templates.Entities.CampaignTemplate templateFromDb,
        RoleRollsDbContext dbContext)
    {
        foreach (var archetype in templateFromDb.Archetypes)
        {
            var codeSpells = LandOfHeroesArchetypeSpells.GetForArchetype(archetype.Id);
            if (codeSpells.Count == 0) continue;
            await SynchronizeSpells(archetype, codeSpells, dbContext);
            var toUnlink = archetype.Spells.Select(s => s.Id).Where(id => !codeSpells.Select(cs => cs.Id).Contains(id)).ToList();
            if (toUnlink.Count > 0)
            {
                await dbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM \"ArchetypeSpells\" WHERE \"ArchetypesId\" = {archetype.Id} AND \"SpellsId\" = ANY({toUnlink.ToArray()})");
            }
        }
    }

    private async Task SynchronizeSpells(Archetype archetypeFromDb, List<Spell> fromCode, RoleRollsDbContext context)
    {
        archetypeFromDb.Spells ??= new List<Spell>();
        var dbSpells = archetypeFromDb.Spells.ToDictionary(s => s.Id);
        var codeIds = fromCode.Select(s => s.Id).ToHashSet();

        // Add or update
        foreach (var codeSpell in fromCode)
        {
            var existing = await context.Spells.FindAsync(codeSpell.Id);
            if (existing == null)
            {
                existing = codeSpell;
                await context.Spells.AddAsync(existing);
            }
            existing.Name = codeSpell.Name;
            existing.Description = codeSpell.Description;
            await SynchronizeSpellCircles(existing, codeSpell.Circles?.ToList() ?? new List<SpellCircle>(), context);
            if (!archetypeFromDb.Spells.Any(s => s.Id == existing.Id))
            {
                archetypeFromDb.Spells.Add(existing);
            }
        }

        // Remove not in code
        foreach (var dbSpell in archetypeFromDb.Spells.Where(s => !codeIds.Contains(s.Id)).ToList())
        {
            archetypeFromDb.Spells.Remove(dbSpell);
        }
    }

    private async Task SynchronizeSpellCircles(Spell dbSpell, List<SpellCircle> codeCircles, RoleRollsDbContext context)
    {
        dbSpell.Circles ??= new List<SpellCircle>();
        var dbByCircle = dbSpell.Circles.ToDictionary(c => c.Circle);
        var codeByCircle = codeCircles.ToDictionary(c => c.Circle);

        // Add or update
        foreach (var codeCircle in codeCircles)
        {
            if (!dbByCircle.TryGetValue(codeCircle.Circle, out var dbCircle))
            {
                codeCircle.SpellId = dbSpell.Id;
                dbSpell.Circles.Add(codeCircle);
                await context.SpellCircles.AddAsync(codeCircle);
            }
            else
            {
                dbCircle.Title = codeCircle.Title;
                dbCircle.InGameDescription = codeCircle.InGameDescription;
                dbCircle.EffectDescription = codeCircle.EffectDescription;
                dbCircle.CastingTime = codeCircle.CastingTime;
                dbCircle.Duration = codeCircle.Duration;
                dbCircle.AreaOfEffect = codeCircle.AreaOfEffect;
                dbCircle.Requirements = codeCircle.Requirements;
                context.SpellCircles.Update(dbCircle);
            }
        }

        // Remove circles not in code
        foreach (var dbCircle in dbSpell.Circles.Where(c => !codeByCircle.ContainsKey(c.Circle)).ToList())
        {
            dbSpell.Circles.Remove(dbCircle);
            context.SpellCircles.Remove(dbCircle);
        }
    }
}









