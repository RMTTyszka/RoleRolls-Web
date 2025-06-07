using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class SpecificSkillTemplateModel
    {
        public SpecificSkillTemplateModel()
        {

        }
        public SpecificSkillTemplateModel(SpecificSkillTemplate specificSkill)
        {
            Id = specificSkill.Id;
            Name = specificSkill.Name;
            SkillTemplateId = specificSkill.SkillTemplateId;
            AttributeTemplateId = specificSkill.AttributeTemplateId;
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid? SkillTemplateId { get; set; }
        public Guid? AttributeTemplateId { get; set; }
    }
}