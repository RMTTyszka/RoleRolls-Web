using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos
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
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}