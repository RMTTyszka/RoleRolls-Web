using System.Data;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Life : Entity
    {
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public Guid LifeTemplateId { get; set; }
        public Life(LifeTemplate life)
        {
            Id = Guid.NewGuid();
            LifeTemplateId = life.Id;
            // TODO formula
            Name = life.Name;
            LifeTemplate = life;
            Formula = life.Formula;
        }

        public LifeTemplate LifeTemplate { get; set; }

        public Life()
        {
        }

        public void CalculateMaxValue(Creature creature)
        {
            var replacesFormula = creature.Attributes.Aggregate(Formula,
                (formula, attribute) => formula.Replace(attribute.Name, attribute.Value.ToString()));
     
            DataTable dt = new DataTable();
            try
            {
                var result = dt.Compute(replacesFormula,"");
                if (result is null)
                {
                    return;
                }

                if (int.TryParse(result.ToString(), out var value))
                {
                    MaxValue = value;
                }
            }
            catch (Exception)
            {
                MaxValue = 0;
            }
            
        }
    }
   
}