using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class SpecificSkillTemplate : Entity
    {
        private Guid? _attributeTemplateId;

        public SpecificSkillTemplate()
        {

        }
        public SpecificSkillTemplate(Guid skillTemplateId, SpecificSkillTemplateModel specificSkill)
        {
            Id = specificSkill.Id;
            SkillTemplateId = skillTemplateId;
            Name = specificSkill.Name;
            AttributeTemplateId = specificSkill.AttributeTemplateId;
        }

        public string Name { get; set; }

        public Guid? AttributeTemplateId
        {
            get => _attributeTemplateId ?? SkillTemplate?.AttributeTemplateId;
            set => _attributeTemplateId = value;
        }

        public AttributeTemplate? AttributeTemplate { get; set; }
        public Guid SkillTemplateId { get; set; }
        public SkillTemplate SkillTemplate { get; set; }

        public ICollection<SpecificSkill> SpecificSkills { get; set; } = new List<SpecificSkill>();

        public Guid GetAttributeId
        {
            get
            {
                if (!AttributeTemplateId.HasValue)
                {
                    if (!SkillTemplate.AttributeTemplateId.HasValue)
                    {
                        throw new Exception();
                    }
                    return SkillTemplate.AttributeTemplateId.Value;

                }
                return AttributeTemplateId.Value;
            }
        }
        public void Update(SpecificSkillTemplateModel specificSkillModel, RoleRollsDbContext dbContext)
        {
            Name = specificSkillModel.Name;
            dbContext.MinorSkillTemplates.Update(this);
        }
    }
}


