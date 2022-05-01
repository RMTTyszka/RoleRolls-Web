using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Creatures.Application.Services
{
    public interface ICreatureTemplateService
    {
        public Task<CreatureTemplate> Get(Guid id);
        public Task Create(CreatureTemplate template);
        public Task Update(Guid id, CreatureTemplate template);
    }
}
