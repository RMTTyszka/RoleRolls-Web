using System.Collections.Generic;
using System.Linq;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos;

public class DefenseTemplateModel : IDefenseTemplate
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Formula { get; set; }
    public List<FormulaToken> FormulaTokens { get; set; } = new();

    public DefenseTemplateModel()
    {
        
    }

    public DefenseTemplateModel(DefenseTemplate defense)
    {
        Id = defense.Id;
        Name = defense.Name;
        Formula = defense.Formula;
        FormulaTokens = defense.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
    }
}


