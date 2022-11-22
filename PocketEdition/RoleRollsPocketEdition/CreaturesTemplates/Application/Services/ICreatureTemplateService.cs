using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CreatureTemplateModel> Get(Guid id);
        public Task Create(CreatureTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CreatureTemplateModel template);
    }
}
