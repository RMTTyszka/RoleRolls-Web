namespace RoleRollsPocketEdition.Creatures.Domain.Models
{
    public class LifeModel
    {

        public Guid Id { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid LifeTemplateId { get; set; }

        public LifeModel(Life life)
        {
            Id = life.Id;
            LifeTemplateId = life.LifeTemplateId;
            MaxValue = life.MaxValue;
            Value = life.MaxValue;
            Name = life.Name;
        }
    }
}