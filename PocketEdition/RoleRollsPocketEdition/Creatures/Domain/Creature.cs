namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class Creature : Entity
    {
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<Skill> Skills { get; set; }

        public ICollection<Life> Lifes { get; set; }


        public Creature FromTemplate(CreatureTemplate template) 
        {
            var creature = new Creature
            {
                Attributes = template.Attributes.Select(attribute => new Attribute(attribute)).ToList(),
                Skills = template.Skills.Select(skill => new Skill(skill)).ToList(),
                Lifes = template.Lifes.Select(life => new Life(life)).ToList(),
            };
            foreach (var life in Lifes)
            {
                life.Value = life.MaxValue;
            }

            return creature;
        }

        public RollsResult RollSkill(RollCheck roll) 
        {
            var attributeId= Skills.FirstOrDefault(skill => skill.Id == roll.SkillId)?.AttributeId;
            if (attributeId is null) 
            {
                return new RollsResult
                {
                    Success = false
                };
            }

            roll.AttributeId = attributeId.Value;
            var result = RollAttribute(roll);
            return result;
        }       
        public RollsResult RollAttribute(RollCheck roll) 
        {
            var attribute = Attributes.FirstOrDefault(attribute => attribute.Id == roll.AttributeId);
            if (attribute is null)
            {
                return new RollsResult
                {
                    Success = false
                };
            }
            return Roll(roll, attribute.Value);
        }

        private RollsResult Roll(RollCheck roll, int level)
        {
            var random = new Random();
            var result = new RollsResult();
            var rolls = Enumerable.Range(0, level).Select((int _) =>
            {
                var value = random.Next(0, 21);
                if (roll.Dificulty.HasValue) 
                {
                    var success = value + roll.Bonus > roll.Dificulty;
                    if (success)
                    {
                        result.Successes += 1;
                        if (value == 20)
                        {
                            result.CriticalSuccesses += 1;
                        }
                    }
                    else 
                    {
                        result.Misses += 1;
                        if (value == 1)
                        {
                            result.CriticalMisses += 1;
                        }
                    }
                }


                return value;
            }).ToList();
            if (roll.Complexity.HasValue)
            {
                result.Success = result.Successes > roll.Complexity;
            }
            result.Rolls = rolls;
            return result;
        }
    }
   
}