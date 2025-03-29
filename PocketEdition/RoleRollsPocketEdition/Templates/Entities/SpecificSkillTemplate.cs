using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class SpecificSkillTemplate : Entity
    {
        private Guid? _attributeId;

        public SpecificSkillTemplate()
        {

        }
        public SpecificSkillTemplate(Guid skillId, MinorSkillTemplateModel minorSkill)
        {
            Id = minorSkill.Id;
            SkillTemplateId = skillId;
            Name = minorSkill.Name;
            AttributeId = minorSkill.AttributeId;
        }

        public string Name { get; set; }

        public Guid? AttributeId
        {
            get
            {
                if (!_attributeId.HasValue)
                {
                    if (!SkillTemplate.AttributeTemplateId.HasValue)
                    {
                        throw new Exception();
                    }
                    return SkillTemplate.AttributeTemplateId.Value;

                }
                return _attributeId.Value;
            }
            set => _attributeId = value;
        }

        public AttributeTemplate? AttributeTemplate { get; set; }
        public Guid SkillTemplateId { get; set; }
        public SkillTemplate SkillTemplate { get; set; }

        public ICollection<SpecificSkill> SpecificSkills { get; set; } = new List<SpecificSkill>();

        public Guid GetAttributeId
        {
            get
            {
                if (!AttributeId.HasValue)
                {
                    if (!SkillTemplate.AttributeTemplateId.HasValue)
                    {
                        throw new Exception();
                    }
                    return SkillTemplate.AttributeTemplateId.Value;

                }
                return AttributeId.Value;
            }
        }
        public void Update(MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            Name = minorSkillModel.Name;
            dbContext.MinorSkillTemplates.Update(this);
        }
    }
}
