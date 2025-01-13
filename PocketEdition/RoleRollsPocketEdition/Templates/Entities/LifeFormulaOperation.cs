using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class LifeFormulaOperation : Entity
    {
        public Guid LifeTemplateId { get; set; }
        public Guid AttributeId { get; set; }
        public string Operator { get; set; }
    }
}
