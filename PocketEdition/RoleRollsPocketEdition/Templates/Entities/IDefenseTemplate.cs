using System.Collections.Generic;

namespace RoleRollsPocketEdition.Templates.Entities;

public interface IDefenseTemplate
{
    string Name { get; set; }
    string Formula { get; set; }
    Guid Id { get; set; }
    List<FormulaToken> FormulaTokens { get; set; }
}


