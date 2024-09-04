using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Application.CreaturesTemplates.Services
{
    public interface IDefenseTemplateService
    {
        public Task CreateAsync(Guid creatureTemplateId, DefenseTemplateModel defenseTemplate);
        public Task UpdateAsync(Guid creatureTemplateId, DefenseTemplateModel defenseTemplate);
        public Task RemoveAsync(Guid creatureTemplateId, Guid defenseTemplateId);
    }
}
