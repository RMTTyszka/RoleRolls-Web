namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class Attribute : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeTemplateId { get; set; }

        public Attribute(AttributeTemplate attribute)
        {
            Id = Guid.NewGuid();
            Name = attribute.Name;
            Value = attribute.Value;
            AttributeTemplateId = attribute.Id;
        }
    }     
   
}