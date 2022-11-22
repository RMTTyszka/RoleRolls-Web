namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class Life : Entity
    {
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid LifeTemplateId { get; set; }
        public Life(LifeTemplate life)
        {
            Id = Guid.NewGuid();
            LifeTemplateId = life.Id;
            MaxValue = life.MaxValue;
            Value = life.MaxValue;
            Name = life.Name;
        }

        public Life()
        {
        }

    }
   
}