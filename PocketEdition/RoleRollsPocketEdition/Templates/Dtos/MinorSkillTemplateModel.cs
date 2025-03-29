using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class MinorSkillTemplateModel
    {
        public MinorSkillTemplateModel()
        {

        }
        public MinorSkillTemplateModel(SpecificSkillTemplate specificSkill)
        {
            Id = specificSkill.Id;
            Name = specificSkill.Name;
            SkillTemplateId = specificSkill.SkillTemplateId;
            AttributeId = specificSkill.AttributeId;
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid SkillTemplateId { get; set; }
        public Guid? AttributeId { get; set; }
    }
}