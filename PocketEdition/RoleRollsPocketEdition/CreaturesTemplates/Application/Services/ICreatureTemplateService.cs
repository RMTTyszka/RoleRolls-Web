using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.CreaturesTemplates.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CreatureTemplateModel> Get(Guid id);
        public Task Create(CreatureTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CreatureTemplateModel template);
    }
}
