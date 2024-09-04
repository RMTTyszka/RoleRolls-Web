using RoleRollsPocketEdition.Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos
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
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid SkillTemplateId { get; set; }

    }
}