using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities
{
    public class Defense : Entity, IDefenseTemplate
    {
        public string Name { get; set; }
        public string Formula { get; set; }
        public Guid DefenseTemplateId { get; set; }
        public DefenseTemplate DefenseTemplate { get; set; }

        public static Defense FromTemplate(IDefenseTemplate defenseTemplateModel)
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