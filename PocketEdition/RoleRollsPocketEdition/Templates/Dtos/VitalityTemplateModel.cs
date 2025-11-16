using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class VitalityTemplateModel
    {
        public VitalityTemplateModel()
        {

        }
        public VitalityTemplateModel(VitalityTemplate vitality)
        {
            Id = vitality.Id;
            Name = vitality.Name;
            Formula = vitality.Formula;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }

    }
}


