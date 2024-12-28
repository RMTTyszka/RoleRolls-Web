using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Services
{
    public class CreatureTemplateService : ICreatureTemplateService
    {
        private readonly RoleRollsDbContext _dbContextl;

        public CreatureTemplateService(RoleRollsDbContext dbContextl)
        {
            _dbContextl = dbContextl;
        }

        public async Task Create(CreatureTemplateModel template)
        {
            var creatureTemplate = new CampaignTemplate(template);
            await _dbContextl.CreatureTemplates.AddAsync(creatureTemplate);
            await _dbContextl.SaveChangesAsync();
        }      
        public async Task<CreatureTemplateModel> Get(Guid id)
        {
            var template = await _dbContextl.CreatureTemplates
                .AsNoTracking()
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .FirstOrDefaultAsync(template => template.Id == id);
            var output = new CreatureTemplateModel(template);
            return output;
        }

        public async Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CreatureTemplateModel updatedTemplate)
        {

            var validation = ValidateInput(updatedTemplate);
            if (validation != CreatureTemplateValidationResult.Ok) 
            {
                return validation;
            }

            var template = await _dbContextl.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .FirstOrDefaultAsync(template => template.Id == id);

            template.Name = updatedTemplate.Name;

            var attributesToCreate = updatedTemplate.Attributes
                .Where(attribute => !template.Attributes.Select(a => a.Id).Contains(attribute.Id))
                .Select(attribute => new AttributeTemplate(attribute))
                .ToList();
            var attributesToUpdate= template.Attributes.Where(attribute => updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id)).ToList();
            var attributesToDelete= template.Attributes.Where(attribute => !updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id)).ToList();

            foreach (var attribute in attributesToUpdate) 
            {
                var updatedAttribute = updatedTemplate.Attributes.First(attr => attr.Id == attribute.Id);
                attribute.Name = updatedAttribute.Name;
            }
            var skillsToCreate = updatedTemplate.Skills
                .Where(skill => !template.Skills.Select(s => s.Id).Contains(skill.Id))
                .Select(skill => new SkillTemplate(skill.AttributeId, skill))
                .ToList();
            var skillsToUpdate= template.Skills.Where(skill => updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id)).ToList();
            var skillsToDelete= template.Skills.Where(skill => !updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id)).ToList();

            var minorSkillsToCreate = new List<MinorSkillTemplate>();
            var minorSkillsToUpdate = new List<MinorSkillTemplate>();
            var minorSkillsToDelete = new List<MinorSkillTemplate>();

            foreach (var skill in skillsToUpdate)
            {
                var updatedSkill= updatedTemplate.Skills.First(sk => sk.Id == skill.Id);
                skill.Name = updatedSkill.Name;

                var minorSkillsToCreate2 = updatedSkill.MinorSkills
                    .Where(minorSkill => !updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id))
                    .Select(minorSkill => new MinorSkillTemplate(skill.Id, minorSkill))
                    .ToList();
                var minorSToUpdate2 = skill.MinorSkills.Where(minorSkill => updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id)).ToList();
                var minorSToDelete2 = skill.MinorSkills.Where(minorSkill => !updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id)).ToList();

                minorSkillsToCreate.AddRange(minorSkillsToCreate2);
                minorSkillsToUpdate.AddRange(minorSToUpdate2);
                minorSkillsToDelete.AddRange(minorSToDelete2);

                foreach (var minorSkill in minorSkillsToCreate)
                {
                    skill.MinorSkills.Add(minorSkill);
                }
                foreach (var minorSkill in minorSkillsToDelete)
                {
                    skill.MinorSkills.Remove(minorSkill);
                }
            }
            foreach (var skill in skillsToCreate)
            {
                template.Skills.Add(skill);
            }
            foreach (var skill in skillsToDelete)
            {
                template.Skills.Remove(skill);
                minorSkillsToDelete.AddRange(skill.MinorSkills);
            }

            var lifesToCreate = updatedTemplate.Lifes
                .Where(life => !template.Lifes.Select(l => l.Id).Contains(life.Id))
                .Select(life => new LifeTemplate(life))
                .ToList();
            var lifesToUpdate= template.Lifes.Where(life => updatedTemplate.Lifes.Select(l => l.Id).Contains(life.Id)).ToList();
            var lifesToDelete= template.Lifes.Where(life => !updatedTemplate.Lifes.Select(l => l.Id).Contains(life.Id)).ToList();

            foreach (var lifes in lifesToUpdate)
            {
                var updatedLife= updatedTemplate.Lifes.First(lf => lf.Id == lifes.Id);
                lifes.Name = updatedLife.Name;
            }
            foreach (var life in lifesToCreate)
            {
                template.Lifes.Add(life);
            }
            foreach (var life in lifesToDelete)
            {
                template.Lifes.Remove(life);
            }

            foreach (var attribute in attributesToCreate)
            {
                template.Attributes.Add(attribute);
            }
            foreach (var attribute in attributesToDelete)
            {
                template.Attributes.Remove(attribute);
                var skillsFromAttribute = template.Skills.Where(sk => sk.AttributeTemplateId == attribute.Id).ToList();
                var minorSkills = skillsFromAttribute.SelectMany(sk => sk.MinorSkills).ToList();
                foreach (var skill in skillsFromAttribute) 
                {
                    template.Skills.Remove(skill);
                    skill.MinorSkills.RemoveAll(_ => true);
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

            await _dbContextl.LifeTemplates.AddRangeAsync(lifesToCreate);
            _dbContextl.LifeTemplates.UpdateRange(lifesToUpdate);
            _dbContextl.LifeTemplates.RemoveRange(lifesToDelete);

            _dbContextl.CreatureTemplates.Update(template);

            await _dbContextl.SaveChangesAsync();

            return CreatureTemplateValidationResult.Ok;
        }

        private CreatureTemplateValidationResult ValidateInput(CreatureTemplateModel template)
        {
            var hasAttribute = template.Skills.All(skill => template.Attributes.Any(attribute => attribute.Id == skill.AttributeId));

            if (!hasAttribute) 
            {
                return CreatureTemplateValidationResult.SkillWithoutAttribute;
            }

            return CreatureTemplateValidationResult.Ok;

        }
    }
}
