using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class VitalityFormulaOperation : Entity
    {
        public Guid VitalityTemplateId { get; set; }
        public Guid AttributeId { get; set; }
        public string Operator { get; set; }
    }
}
