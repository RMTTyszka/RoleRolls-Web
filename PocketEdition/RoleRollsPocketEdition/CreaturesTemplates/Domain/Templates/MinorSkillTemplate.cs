using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class MinorSkillTemplate : Entity
    {
        public MinorSkillTemplate()
        {

        }
        public MinorSkillTemplate(MinorSkillTemplateModel minorSkill)
        {
            Id = minorSkill.Id;
            Name = minorSkill.Name;
        }

        public string Name { get; set; }
    }
}
