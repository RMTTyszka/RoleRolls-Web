using System.Collections.Generic;
using System.Linq;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Defense : Entity, IDefenseTemplate
    {
        public string Name { get; set; }
        public string Formula { get; set; }
        public List<FormulaToken> FormulaTokens { get; set; } = new();
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
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
                FormulaTokens = defenseTemplateModel.FormulaTokens?.Select(token => token.Clone()).ToList() ??
                                new List<FormulaToken>(),
            };
            return defense;
        }

        public void Update(DefenseTemplateModel defenseTemplateModel)
        {
            Name = defenseTemplateModel.Name;
            Formula = defenseTemplateModel.Formula;
            FormulaTokens = defenseTemplateModel.FormulaTokens?.Select(token => token.Clone()).ToList() ??
                            new List<FormulaToken>();
        }
    }
}


