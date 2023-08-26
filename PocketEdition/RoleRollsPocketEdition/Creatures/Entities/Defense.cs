using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Defense : Entity
    {
        public string Name { get; set; }
        public string Formula { get; set; }
        public Guid DefenseTemplateId { get; set; }

        public static Defense FromTemplate(DefenseTemplateModel defenseTemplateModel)
        {
            var defense = new Defense
            {
                Id = Guid.NewGuid(),
                DefenseTemplateId = defenseTemplateModel.Id,
                Name = defenseTemplateModel.Name,
                Formula = defenseTemplateModel.Formula,
            };
            return defense;
        }

        public void Update(DefenseTemplateModel defenseTemplateModel)
        {
            Name = defenseTemplateModel.Name;
            Formula = defenseTemplateModel.Formula;
        }
    }
}