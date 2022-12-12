namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class LifeFormulaOperation : Entity
    {
        public Guid LifeTemplateId { get; set; }
        public Guid AttributeId { get; set; }
        public string Operator { get; set; }
    }
}
