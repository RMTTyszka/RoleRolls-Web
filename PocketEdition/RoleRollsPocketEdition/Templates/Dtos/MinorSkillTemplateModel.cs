using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class MinorSkillTemplateModel
    {
        public MinorSkillTemplateModel()
        {

        }
        public MinorSkillTemplateModel(MinorSkillTemplate minorSkill)
        {
            Id = minorSkill.Id;
            Name = minorSkill.Name;
            SkillTemplateId = minorSkill.SkillTemplateId;
            AttributeId = minorSkill.AttributeId;
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid SkillTemplateId { get; set; }
        public Guid? AttributeId { get; set; }
    }
}