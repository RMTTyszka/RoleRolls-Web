using System.Collections.Generic;
using System.Linq;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities;

public class DefenseTemplate : Entity, IDefenseTemplate
{
    public DefenseTemplate()
    {
        // EF CORE
    }
    public DefenseTemplate(DefenseTemplateModel model)
    {
        Id = model.Id;
        Name = model.Name;
        Formula = model.Formula;
        FormulaTokens = model.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
    }

    public string Name { get; set; }
    public string Formula { get; set; }
    public List<FormulaToken> FormulaTokens { get; set; } = new();
    public Guid CampaignTemplateId { get; set; }
    public CampaignTemplate CampaignTemplate { get; set; }
    public ICollection<Defense> Defenses { get; set; }

    public void Update(DefenseTemplateModel defenseModel)
    {
        Name = defenseModel.Name;
        Formula = defenseModel.Formula;
        FormulaTokens = defenseModel.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
    }
}


