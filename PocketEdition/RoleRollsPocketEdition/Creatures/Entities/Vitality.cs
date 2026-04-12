using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Vitality : Entity
    {
        [NotMapped] public int MaxValue => CalculateMaxValue(Creature);
        [NotMapped] public IReadOnlyList<VitalityConditionModel> CurrentConditions => ResolveCurrentConditions();
        [NotMapped] public string? CurrentStatus => CurrentConditions.FirstOrDefault()?.Name;
        [NotMapped] public string? CurrentStatusDescription => CurrentConditions.FirstOrDefault()?.Description;
        [NotMapped] public Guid? CurrentStatusConditionId => CurrentConditions.FirstOrDefault()?.ConditionId;
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
        public Guid VitalityTemplateId { get; set; }
        public VitalityTemplate VitalityTemplate { get; set; }
        public Vitality(VitalityTemplate vitality, Creature creature)
        {
            Id = Guid.NewGuid();
            VitalityTemplateId = vitality.Id;
            Name = vitality.Name;
            VitalityTemplate = vitality;
            Creature = creature;
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

        private IReadOnlyList<VitalityConditionModel> ResolveCurrentConditions()
        {
            if (VitalityTemplate == null)
            {
                return [];
            }

            var maxValue = MaxValue;
            if (maxValue <= 0)
            {
                return [];
            }

            var conditions = Creature?.Campaign?.CampaignTemplate?.CreatureConditions ??
                             VitalityTemplate.CampaignTemplate?.CreatureConditions;

            if (conditions == null || conditions.Count == 0)
            {
                return [];
            }

            var currentConditions = new List<VitalityConditionModel>();

            if (Value <= 0)
            {
                var atZeroId = VitalityTemplate.ConditionAtZero?.Id;
                if (atZeroId.HasValue)
                {
                    var atZero = conditions.FirstOrDefault(condition => condition.Id == atZeroId.Value);
                    if (atZero != null)
                    {
                        currentConditions.Add(new VitalityConditionModel
                        {
                            ConditionId = atZero.Id,
                            Name = atZero.Name,
                            Description = atZero.Description,
                            ThresholdPercent = 0
                        });
                    }
                }
            }

            var currentPercent = (decimal)Value * 100 / maxValue;
            if (currentPercent <= 30m)
            {
                var atThirtyId = VitalityTemplate.ConditionAtThirtyPercent?.Id;
                if (atThirtyId.HasValue)
                {
                    var atThirty = conditions.FirstOrDefault(condition => condition.Id == atThirtyId.Value);
                    if (atThirty != null)
                    {
                        currentConditions.Add(new VitalityConditionModel
                        {
                            ConditionId = atThirty.Id,
                            Name = atThirty.Name,
                            Description = atThirty.Description,
                            ThresholdPercent = 30
                        });
                    }
                }
            }

            var normalized = currentConditions
                .GroupBy(condition => condition.ConditionId ?? Guid.Empty)
                .Select(group => group.OrderBy(condition => condition.ThresholdPercent).First())
                .OrderBy(condition => condition.ThresholdPercent)
                .ThenBy(condition => condition.Name)
                .ToList();

            return normalized;
        }
    }
   
}


