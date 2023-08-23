using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CreatureTemplateModel> Get(Guid id);
        public Task Create(CreatureTemplateModel template);
        public Task<CreatureTemplateValidationResult> UpdateAsync(Guid id, CreatureTemplateModel template);
    }
}
