using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates
{
    public class LifeFormulaOperation : Entity
    {
        public Guid LifeTemplateId { get; set; }
        public Guid AttributeId { get; set; }
        public string Operator { get; set; }
    }
}
