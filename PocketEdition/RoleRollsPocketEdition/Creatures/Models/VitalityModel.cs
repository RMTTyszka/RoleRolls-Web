using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class VitalityModel
    {

        public VitalityModel()
        {

        }
        public Guid Id { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid VitalityTemplateId { get; set; }

        public VitalityModel(Vitality vitality)
        {
            Id = vitality.Id;
            VitalityTemplateId = vitality.VitalityTemplateId;
            MaxValue = vitality.MaxValue;
            Value = vitality.Value;
            Name = vitality.Name;
        }
    }
}


