using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public class CreatureTemplateService : ICreatureTemplateService
    {
        private readonly RoleRollsDbContext _dbContextl;

        public CreatureTemplateService(RoleRollsDbContext dbContextl)
        {
            _dbContextl = dbContextl;
        }

        public async Task Create(CreatureTemplate template)
        {
            await _dbContextl.CreatureTemplates.AddAsync(template);
            await _dbContextl.SaveChangesAsync();
        }      
        public async Task<CreatureTemplate> Get(Guid id)
        {
            var template = await _dbContextl.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .FirstOrDefaultAsync(template => template.Id == id);
            return template;
        }

        public async Task Update(Guid id, CreatureTemplate updatedTemplate)
        {
            var template = await _dbContextl.CreatureTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .FirstOrDefaultAsync(template => template.Id == id);

            template.Name = updatedTemplate.Name;

            var attributesToCreate = updatedTemplate.Attributes.Where(attribute => !template.Attributes.Select(a => a.Id).Contains(attribute.Id));
            var attributesToUpdate= template.Attributes.Where(attribute => updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id));
            var attributesToDelete= template.Attributes.Where(attribute => !updatedTemplate.Attributes.Select(a => a.Id).Contains(attribute.Id));

            foreach (var attribute in attributesToUpdate) 
            {
                var updatedAttribute = updatedTemplate.Attributes.First(attr => attr.Id == attribute.Id);
                attribute.Name = updatedAttribute.Name;
            }

            var skillsToCreate = updatedTemplate.Skills.Where(skill => !template.Skills.Select(s => s.Id).Contains(skill.Id));
            var skillsToUpdate= template.Skills.Where(skill => updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id));
            var skillsToDelete= template.Skills.Where(skill => !updatedTemplate.Skills.Select(s => s.Id).Contains(skill.Id));

            var minorSkillsToCreate = new List<MinorSkillTemplate>();
            var minorSkillsToUpdate = new List<MinorSkillTemplate>();
            var minorSkillsToDelete = new List<MinorSkillTemplate>();

            foreach (var skill in skillsToUpdate)
            {
                var updatedSkill= updatedTemplate.Skills.First(sk => sk.Id == skill.Id);
                skill.Name = updatedSkill.Name;

                var minorSkillsToCreate2 = updatedSkill.MinorSkills.Where(minorSkill => !updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id));
                var minorSToUpdate2 = skill.MinorSkills.Where(minorSkill => updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id));
                var minorSToDelete2 = skill.MinorSkills.Where(minorSkill => !updatedSkill.MinorSkills.Select(s => s.Id).Contains(minorSkill.Id));

                minorSkillsToCreate.AddRange(minorSkillsToCreate2);
                minorSkillsToUpdate.AddRange(minorSToUpdate2);
                minorSkillsToDelete.AddRange(minorSToDelete2);
            }

            var lifesToCreate = updatedTemplate.Lifes.Where(life => !template.Lifes.Select(l => l.Id).Contains(life.Id));
            var lifesToUpdate= template.Lifes.Where(life => updatedTemplate.Lifes.Select(l => l.Id).Contains(life.Id));
            var lifesToDelete= template.Lifes.Where(life => !updatedTemplate.Lifes.Select(l => l.Id).Contains(life.Id));

            foreach (var lifes in lifesToUpdate)
            {
                var updatedLife= updatedTemplate.Lifes.First(lf => lf.Id == lifes.Id);
                lifes.Name = updatedLife.Name;
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
        }
    }
}
