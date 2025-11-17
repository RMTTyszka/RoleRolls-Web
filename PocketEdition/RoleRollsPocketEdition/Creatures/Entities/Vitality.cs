using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Vitality : Entity
    {
        [NotMapped] public int MaxValue => CalculateMaxValue(Creature);
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
        public Guid VitalityTemplateId { get; set; }
        public VitalityTemplate VitalityTemplate { get; set; }
        public Vitality(VitalityTemplate vitality)
        {
            Id = Guid.NewGuid();
            VitalityTemplateId = vitality.Id;
            Name = vitality.Name;
            VitalityTemplate = vitality;
        }


        public Vitality()
        {
        }

        public int CalculateMaxValue(Creature? creature)
        {
            if (creature == null)
            {
                return 0;
            }

            var result = creature.GetFormulaDetails(VitalityTemplate.Formula, VitalityTemplate.FormulaTokens);
            return result.Value;
        }
    }
   
}


