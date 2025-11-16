using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Services
{
    public class CreatureTemplateService : ICreatureTemplateService
    {
        private readonly RoleRollsDbContext _dbContextl;

        public CreatureTemplateService(RoleRollsDbContext dbContextl)
        {
            _dbContextl = dbContextl;
        }

        public async Task Create(CampaignTemplateModel template)
        {
            var creatureTemplate = new CampaignTemplate(template);
            await _dbContextl.CampaignTemplates.AddAsync(creatureTemplate);
            await _dbContextl.SaveChangesAsync();
        }

        public async Task<CampaignTemplateModel> Get(Guid id)
        {
            var template = await _dbContextl.CampaignTemplates
                .AsNoTracking()
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.SpecificSkillTemplates)
                .Include(template => template.Vitalities)
                .FirstOrDefaultAsync(template => template.Id == id)
                ?? throw new InvalidOperationException($"Campaign template {id} was not found.");
            var output = new CampaignTemplateModel(template);
            return output;
        }

        public async Task<List<CampaignTemplateModel>> GetDefaults(PagedRequestInput input)
        {
            var templates = await _dbContextl.CampaignTemplates
                .AsNoTracking()
                .Where(template => template.Default)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Select(e => new CampaignTemplateModel(e))
                .ToListAsync();
            return templates;
        }

        public async Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CampaignTemplateModel updatedTemplate)
        {
            var validation = ValidateInput(updatedTemplate);
            if (validation != CreatureTemplateValidationResult.Ok)
            {
                return validation;
            }

            var template = await _dbContextl.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.SpecificSkillTemplates)
                .Include(template => template.Vitalities)
                .FirstAsync(template => template.Id == id);

            template.Name = updatedTemplate.Name;

            var attributesToCreate = updatedTemplate.Attributes
                .Where(attribute => !template.Attributes.Select(a => a.Id).Contains(attribute.Id))
                .Select(attribute => new AttributeTemplate(attribute))
                .ToList();
            var attributesToUpdate = template.Attributes
                .Where(attribute => updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id)).ToList();
            var attributesToDelete = template.Attributes
                .Where(attribute => !updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id)).ToList();

            foreach (var attribute in attributesToUpdate)
            {
                var updatedAttribute = updatedTemplate.Attributes.First(attr => attr.Id == attribute.Id);
                attribute.Name = updatedAttribute.Name;
            }

            var skillsToCreate = updatedTemplate.Skills
                .Where(skill => !template.Skills.Select(s => s.Id).Contains(skill.Id))
                .Select(skill => new SkillTemplate(null, skill))
                .ToList();
            var skillsToUpdate = template.Skills
                .Where(skill => updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id)).ToList();
            var skillsToDelete = template.Skills
                .Where(skill => !updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id)).ToList();

            var minorSkillsToCreate = new List<SpecificSkillTemplate>();
            var minorSkillsToUpdate = new List<SpecificSkillTemplate>();
            var minorSkillsToDelete = new List<SpecificSkillTemplate>();

            foreach (var skill in skillsToUpdate)
            {
                var updatedSkill = updatedTemplate.Skills.First(sk => sk.Id == skill.Id);
                skill.Name = updatedSkill.Name;

                var minorSkillsToCreate2 = updatedSkill.SpecificSkillTemplates
                    .Where(minorSkill => !updatedSkill.SpecificSkillTemplates.Select(s => s.Id).Contains(minorSkill.Id))
                    .Select(minorSkill => new SpecificSkillTemplate(skill.Id, minorSkill))
                    .ToList();
                var minorSToUpdate2 = skill.SpecificSkillTemplates.Where(minorSkill =>
                    updatedSkill.SpecificSkillTemplates.Select(s => s.Id).Contains(minorSkill.Id)).ToList();
                var minorSToDelete2 = skill.SpecificSkillTemplates.Where(minorSkill =>
                    !updatedSkill.SpecificSkillTemplates.Select(s => s.Id).Contains(minorSkill.Id)).ToList();

                minorSkillsToCreate.AddRange(minorSkillsToCreate2);
                minorSkillsToUpdate.AddRange(minorSToUpdate2);
                minorSkillsToDelete.AddRange(minorSToDelete2);

                foreach (var minorSkill in minorSkillsToCreate)
                {
                    skill.SpecificSkillTemplates.Add(minorSkill);
                }

                foreach (var minorSkill in minorSkillsToDelete)
                {
                    skill.SpecificSkillTemplates.Remove(minorSkill);
                }
            }

            foreach (var skill in skillsToCreate)
            {
                template.Skills.Add(skill);
            }

            foreach (var skill in skillsToDelete)
            {
                template.Skills.Remove(skill);
                minorSkillsToDelete.AddRange(skill.SpecificSkillTemplates);
            }

            var vitalitiesToCreate = updatedTemplate.Vitalities
                .Where(vitality => !template.Vitalities.Select(l => l.Id).Contains(vitality.Id))
                .Select(vitality => new VitalityTemplate(vitality))
                .ToList();
            var vitalitiesToUpdate = template.Vitalities
                .Where(vitality => updatedTemplate.Vitalities.Select(l => l.Id).Contains(vitality.Id)).ToList();
            var vitalitiesToDelete = template.Vitalities
                .Where(vitality => !updatedTemplate.Vitalities.Select(l => l.Id).Contains(vitality.Id)).ToList();

            foreach (var vitalities in vitalitiesToUpdate)
            {
                var updatedVitality = updatedTemplate.Vitalities.First(lf => lf.Id == vitalities.Id);
                vitalities.Name = updatedVitality.Name;
            }

            foreach (var vitality in vitalitiesToCreate)
            {
                template.Vitalities.Add(vitality);
            }

            foreach (var vitality in vitalitiesToDelete)
            {
                template.Vitalities.Remove(vitality);
            }

            foreach (var attribute in attributesToCreate)
            {
                template.Attributes.Add(attribute);
            }

            foreach (var attribute in attributesToDelete)
            {
                template.Attributes.Remove(attribute);
                var skillsFromAttribute = template.Skills.Where(sk => sk.AttributeTemplateId == attribute.Id).ToList();
                var minorSkills = skillsFromAttribute.SelectMany(sk => sk.SpecificSkillTemplates).ToList();
                foreach (var skill in skillsFromAttribute)
                {
                    template.Skills.Remove(skill);
                    skill.SpecificSkillTemplates.RemoveAll(_ => true);
                }

                skillsToDelete.AddRange(skillsFromAttribute);
                minorSkillsToDelete.AddRange(minorSkills);
            }

            await _dbContextl.AttributeTemplates.AddRangeAsync(attributesToCreate);
            _dbContextl.AttributeTemplates.UpdateRange(attributesToUpdate);
            _dbContextl.AttributeTemplates.RemoveRange(attributesToDelete);

            await _dbContextl.MinorSkillTemplates.AddRangeAsync(minorSkillsToCreate);
            _dbContextl.MinorSkillTemplates.UpdateRange(minorSkillsToUpdate);
            _dbContextl.MinorSkillTemplates.RemoveRange(minorSkillsToDelete);

            await _dbContextl.SkillTemplates.AddRangeAsync(skillsToCreate);
            _dbContextl.SkillTemplates.UpdateRange(skillsToUpdate);
            _dbContextl.SkillTemplates.RemoveRange(skillsToDelete);

            await _dbContextl.VitalityTemplates.AddRangeAsync(vitalitiesToCreate);
            _dbContextl.VitalityTemplates.UpdateRange(vitalitiesToUpdate);
            _dbContextl.VitalityTemplates.RemoveRange(vitalitiesToDelete);

            _dbContextl.CampaignTemplates.Update(template);

            await _dbContextl.SaveChangesAsync();

            return CreatureTemplateValidationResult.Ok;
        }

        private CreatureTemplateValidationResult ValidateInput(CampaignTemplateModel template)
        {
            return CreatureTemplateValidationResult.Ok;
        }
    }
}
