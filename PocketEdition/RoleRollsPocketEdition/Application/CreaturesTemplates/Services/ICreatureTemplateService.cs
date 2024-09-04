using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Application.CreaturesTemplates.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CreatureTemplateModel> Get(Guid id);
        public Task Create(CreatureTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CreatureTemplateModel template);
    }
}
