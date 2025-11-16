using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
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

            var replacesFormula = creature.Attributes.Aggregate(VitalityTemplate.Formula,
                (formula, attribute) => formula.Replace(attribute.Name, attribute.Points.ToString()));
     
            var dt = new DataTable();
            try
            {
                var result = dt.Compute(replacesFormula,"");

                if (int.TryParse(result.ToString(), out var value))
                {
                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

            return 0;
        }
    }
   
}


