using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using RoleRollsPocketEdition.Archetypes;
using RoleRollsPocketEdition.Archetypes.Entities;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Bonuses;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.CreatureTypes.Entities;
using RoleRollsPocketEdition.Encounters.Entities;
using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Itens.Templates;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public partial class Creature : Entity, IHaveBonuses
    {
        public ICollection<Attribute> Attributes { get; set; }

        public ICollection<Skill> Skills { get; set; }

        public ICollection<Vitality> Vitalities { get; set; }
        public ICollection<Defense> Defenses { get; set; }
        public ICollection<CreaturePower> Powers { get; set; }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public Guid OwnerId { get; set; }

        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsTemplate { get; set; }
        
        [NotMapped]
        public int TotalSkillsPointsLimit => Skills?.Sum(s => s.PointsLimit) ?? 0;
        public int MaxPointsPerSpecificSkill => 3 + Level - 1;
        public int MinPointsPerSpecificSkill => 0;

        public CreatureCategory Category { get; set; }
        public Equipment Equipment { get; set; }
        public Inventory Inventory { get; set; }
        public CreatureType? CreatureType { get; set; }
        public Archetype? Archetype { get; set; }

        public Guid? EncounterId { get; set; }
        public Encounter? Encounter { get; set; }

        [NotMapped]
        public List<SpecificSkill> SpecificSkills => Skills.SelectMany(s => s.SpecificSkills).ToList();

        public Creature()
        {
            Attributes = new List<Attribute>();
            Skills = new List<Skill>();
            Vitalities = new List<Vitality>();
            Defenses = new List<Defense>();
            Equipment = new Equipment();
            Inventory = new Inventory();
            Level = 1;
        }

        public int DefenseValue(Guid id)
        {
            var defense = Defenses.Where(x => x.Id == id || x.DefenseTemplateId == id).First();
            var value = ApplyFormula(defense.Formula, defense.FormulaTokens);
            return value;
        }

        public static Creature FromTemplate(CampaignTemplate template, Guid campaignId,
            CreatureCategory creatureCategory, bool isTemplate)
        {
            var attributes = template.Attributes.Select(attribute => new Attribute(attribute)).ToList();
            var creatureSkills = new List<Skill>();
            foreach (var st in template.Skills)
            {
                creatureSkills.Add(new Skill(st, attributes));
            }

            var creature = new Creature
            {
                Attributes = attributes,
                Vitalities = template.Vitalities.Select(vitality => new Vitality(vitality)).ToList(),
                Defenses = template.Defenses.Select(Defense.FromTemplate).ToList(),
                CampaignId = campaignId,
                CreatureTemplateId = template.Id,
                Category = creatureCategory,
                IsTemplate = isTemplate,
                Skills = creatureSkills,
                Id = Guid.NewGuid()
            };
            foreach (var vitality in creature.Vitalities)
            {
                vitality.CalculateMaxValue(creature);
                vitality.Value = vitality.MaxValue;
            }

            return creature;
        }

        // Removed AttributelessSkills; use Skills where Skill.AttributeId may be null

        public CreatureUpdateValidationResult Update(CreatureModel creatureModel)
        {
            if (Valid(creatureModel))
            {
                Name = creatureModel.Name;
                foreach (var attribute in Attributes)
                {
                    var updatedAttribute = creatureModel.Attributes.First(attr =>
                        attr.AttributeTemplateId == attribute.AttributeTemplateId);
                    attribute.Update(updatedAttribute);
                }

                // Validate global minor skills points
                var allUpdatedSpecifics = creatureModel.Skills.SelectMany(s => s.SpecificSkills).ToList();
                var totalRequested = allUpdatedSpecifics.Sum(ms => ms.Points);
                if (totalRequested > TotalSkillsPointsLimit)
                {
                    return new CreatureUpdateValidationResult(CreatureUpdateValidation.SkillPointsGreaterThanAllowed,
                        "Skills");
                }
                foreach (var s in allUpdatedSpecifics)
                {
                    if (s.Points > MaxPointsPerSpecificSkill || s.Points < MinPointsPerSpecificSkill)
                    {
                        return new CreatureUpdateValidationResult(CreatureUpdateValidation.SkillPointsGreaterThanAllowed,
                            "SpecificSkills");
                    }
                }

                foreach (var skill in Skills)
                {
                    var updatedSkill = creatureModel.Skills.First(sk => sk.SkillTemplateId == skill.SkillTemplateId);
                    var result = skill.Update(updatedSkill);
                    if (result.Validation != CreatureUpdateValidation.Ok)
                    {
                        return result;
                    }
                }

                return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null);
            }

            return new CreatureUpdateValidationResult(CreatureUpdateValidation.InvalidModel, null);
        }

        private bool Valid(CreatureModel creatureModel)
        {
            var hasDifferentArchetype = Archetype is not null && creatureModel.Archetype is not null &&
                                        Archetype.Id != creatureModel.Archetype.Id;
            var hasDifferentCreatureType = CreatureType is not null && creatureModel.CreatureType is not null &&
                                           CreatureType.Id != creatureModel.CreatureType.Id;

            if (hasDifferentArchetype || hasDifferentCreatureType)
            {
                return false;
            }

            return true;
        }

        public CreatureTakeDamageResult TakeDamage(Guid vitalityId, int value)
        {
            var vitality = Vitalities.First(vitality => vitality.VitalityTemplateId == vitalityId);

            var excessDamage = 0;
            var actualDamage = value;

            if (vitality.Value - value <= 0)
            {
                actualDamage = vitality.Value;
                excessDamage = value - vitality.Value;
                vitality.Value = 0;
            }
            else
            {
                vitality.Value -= value;
            }

            return new CreatureTakeDamageResult
            {
                Name = Name,
                Value = actualDamage,
                ExcessDamage = excessDamage,
                Vitality = vitality.Name,
                ActorId = Id
            };
        }




        public SceneAction Heal(Guid vitalityId, int value)
        {
            var vitality = Vitalities.First(vitality => vitality.Id == vitalityId);
            vitality.Value += value;
            vitality.Value = Math.Min(vitality.Value, vitality.MaxValue);
            return new SceneAction
            {
                Description = $"{Name} healed {value} of {vitality.Name}",
                ActorType = Category switch
                {
                    CreatureCategory.Hero => ActionActorType.Hero,
                    CreatureCategory.Monster => ActionActorType.Monster,
                    _ => throw new ArgumentOutOfRangeException()
                },
                Id = Guid.NewGuid(),
                ActorId = Id
            };
        }

        public void FullRestore()
        {
            foreach (var vitality in Vitalities)
            {
                vitality.Value = vitality.MaxValue;
            }
        }

        public int ApplyFormula(string formula, IReadOnlyCollection<FormulaToken>? formulaTokens = null)
        {
            return GetFormulaDetails(formula, formulaTokens).Value;
        }

        public FormulaEvaluationResult GetFormulaDetails(string formula,
            IReadOnlyCollection<FormulaToken>? formulaTokens = null)
        {
            if (formulaTokens is { Count: > 0 })
            {
                var (description, resolvedExpression) = BuildExpressionsFromTokens(formulaTokens);
                var totalValue = EvaluateExpression(resolvedExpression);
                return new FormulaEvaluationResult(totalValue, description, resolvedExpression);
            }

            if (string.IsNullOrWhiteSpace(formula))
            {
                return new FormulaEvaluationResult(0, string.Empty, string.Empty);
            }

            var resolved = BuildLegacyExpression(formula);
            var value = EvaluateExpression(resolved);
            return new FormulaEvaluationResult(value, formula, resolved);
        }

        private (string Description, string ResolvedExpression) BuildExpressionsFromTokens(
            IEnumerable<FormulaToken> formulaTokens)
        {
            var orderedTokens = formulaTokens.OrderBy(token => token.Order).ToList();
            var descriptionBuilder = new StringBuilder();
            var resolvedBuilder = new StringBuilder();

            foreach (var token in orderedTokens)
            {
                descriptionBuilder.Append(ResolveTokenDescription(token));
                resolvedBuilder.Append(ResolveTokenValue(token));
            }

            return (descriptionBuilder.ToString().Trim(), resolvedBuilder.ToString());
        }

        private string ResolveTokenValue(FormulaToken token) =>
            token.Type switch
            {
                FormulaTokenType.Property => AppendManualValue(ResolvePropertyValue(token.Property), token.ManualValue),
                FormulaTokenType.Creature =>
                    AppendManualValue(ResolveCreatureValue(token.CreatureValue), token.ManualValue),
                FormulaTokenType.Equipment =>
                    AppendManualValue(ResolveEquipmentValue(token), token.ManualValue),
                FormulaTokenType.Manual => token.ManualValue ?? string.Empty,
                _ => ResolveLegacyTokenValue(token)
            };

        private string ResolveTokenDescription(FormulaToken token) =>
            token.Type switch
            {
                FormulaTokenType.Property =>
                    AppendManualDescription(
                        $"{GetPropertyDisplayName(token.Property)}({ResolvePropertyValue(token.Property)})",
                        token.ManualValue),
                FormulaTokenType.Creature => AppendManualDescription(
                    $"{GetCreatureValueLabel(token.CreatureValue)}({ResolveCreatureValue(token.CreatureValue)})",
                    token.ManualValue),
                FormulaTokenType.Equipment => AppendManualDescription(
                    $"{GetEquipmentValueLabel(token)}({ResolveEquipmentValue(token)})",
                    token.ManualValue),
                FormulaTokenType.Manual => token.ManualValue ?? string.Empty,
                _ => ResolveLegacyTokenDescription(token)
            };

        private static string AppendManualValue(string baseValue, string? manualValue)
        {
            if (string.IsNullOrEmpty(manualValue))
            {
                return baseValue;
            }
            return $"{baseValue}{manualValue}";
        }

        private static string AppendManualDescription(string baseValue, string? manualValue)
        {
            if (string.IsNullOrWhiteSpace(manualValue))
            {
                return baseValue;
            }
            return $"{baseValue} {manualValue}";
        }

        private string ResolveLegacyTokenValue(FormulaToken token)
        {
            if (token.Value.HasValue)
            {
                return token.Value.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(token.Operator))
            {
                return token.Operator!;
            }

            return token.ManualValue ?? string.Empty;
        }

        private string ResolveLegacyTokenDescription(FormulaToken token)
        {
            if (token.Value.HasValue)
            {
                return token.Value.Value.ToString(CultureInfo.InvariantCulture);
            }

            if (!string.IsNullOrWhiteSpace(token.Operator))
            {
                return $" {token.Operator} ";
            }

            return token.ManualValue ?? string.Empty;
        }

        private string BuildLegacyExpression(string formula)
        {
            var replacedFormula = Attributes.Aggregate(formula,
                (current, attribute) => current.Replace(attribute.Name, attribute.TotalValue.ToString()));

            replacedFormula = Skills.Aggregate(replacedFormula,
                (current, skill) => current.Replace(skill.Name,
                    ResolvePropertyValue(new Property(skill.SkillTemplateId, PropertyType.Skill))));

            replacedFormula = SpecificSkills.Aggregate(replacedFormula,
                (current, minorSkill) => current.Replace(minorSkill.Name,
                    ResolvePropertyValue(new Property(minorSkill.SpecificSkillTemplateId, PropertyType.MinorSkill))));

            return replacedFormula;
        }

        private string ResolvePropertyValue(Property? property)
        {
            if (property is null)
            {
                return "0";
            }

            var propertyValue = GetPropertyValue(new PropertyInput(property));
            var total = propertyValue.Value + propertyValue.Bonus;
            return total.ToString(CultureInfo.InvariantCulture);
        }

        private string GetPropertyDisplayName(Property? property)
        {
            if (property is null)
            {
                return "Propriedade";
            }

            string? AttributeName() =>
                Attributes.FirstOrDefault(attr =>
                        attr.AttributeTemplateId == property.Id || attr.Id == property.Id)
                    ?.Name;

            return property.Type switch
            {
                PropertyType.Attribute => AttributeName() ?? "Atributo",
                PropertyType.Skill => Skills.FirstOrDefault(sk =>
                        sk.SkillTemplateId == property.Id || sk.Id == property.Id)
                    ?.Name ?? "Pericia",
                PropertyType.MinorSkill => SpecificSkills.FirstOrDefault(ms =>
                        ms.SpecificSkillTemplateId == property.Id || ms.Id == property.Id)
                    ?.Name ?? "Especializacao",
                PropertyType.Vitality => Vitalities.FirstOrDefault(v =>
                        v.VitalityTemplateId == property.Id || v.Id == property.Id)
                    ?.Name ?? "Vitalidade",
                PropertyType.Defense => Defenses.FirstOrDefault(d =>
                        d.DefenseTemplateId == property.Id || d.Id == property.Id)
                    ?.Name ?? "Defesa",
                _ => AttributeName() ?? "Propriedade"
            };
        }

        private string ResolveCreatureValue(FormulaCreatureValue? creatureValue)
        {
            return creatureValue switch
            {
                FormulaCreatureValue.Level => Level.ToString(CultureInfo.InvariantCulture),
                FormulaCreatureValue.ArmorDefenseBonus =>
                    ResolveEquipmentValue(EquipableSlot.Chest, FormulaEquipmentValue.DefenseBonus1),
                FormulaCreatureValue.DefenseBonus1 =>
                    ResolveEquipmentValue(EquipableSlot.Chest, FormulaEquipmentValue.DefenseBonus1),
                FormulaCreatureValue.DefenseBonus2 =>
                    ResolveEquipmentValue(EquipableSlot.Chest, FormulaEquipmentValue.DefenseBonus2),
                FormulaCreatureValue.ArmorBonus =>
                    ResolveEquipmentValue(EquipableSlot.Chest, FormulaEquipmentValue.LevelBonus),
                _ => "0"
            };
        }

        private string GetCreatureValueLabel(FormulaCreatureValue? creatureValue)
        {
            return creatureValue switch
            {
                FormulaCreatureValue.Level => "Nivel",
                FormulaCreatureValue.DefenseBonus1 or FormulaCreatureValue.ArmorDefenseBonus => "Bonus defesa 1",
                FormulaCreatureValue.DefenseBonus2 => "Bonus defesa 2",
                FormulaCreatureValue.ArmorBonus => "Bonus armadura",
                _ => "Criatura"
            };
        }

        private string ResolveEquipmentValue(FormulaToken token)
        {
            if (token.EquipmentSlot is null || token.EquipmentValue is null)
            {
                return "0";
            }
            return ResolveEquipmentValue(token.EquipmentSlot.Value, token.EquipmentValue.Value);
        }

        private string ResolveEquipmentValue(EquipableSlot slot, FormulaEquipmentValue equipmentValue)
        {
            var item = Equipment?.GetItem(slot);
            var armorCategory = slot == EquipableSlot.Chest
                ? Equipment?.ArmorCategory ?? ArmorCategory.None
                : ArmorCategory.None;

            var value = equipmentValue switch
            {
                FormulaEquipmentValue.LevelBonus => item?.LevelBonus ?? 0,
                FormulaEquipmentValue.DefenseBonus1 => slot == EquipableSlot.Chest
                    ? item?.GetDefenseBonus1() ?? ArmorDefinition.DefenseBonus1(armorCategory)
                    : 0,
                FormulaEquipmentValue.DefenseBonus2 => slot == EquipableSlot.Chest
                    ? item?.GetDefenseBonus2() ?? ArmorDefinition.DefenseBonus2(armorCategory)
                    : 0,
                _ => 0
            };

            return value.ToString(CultureInfo.InvariantCulture);
        }

        private string GetEquipmentValueLabel(FormulaToken token)
        {
            var slotName = token.EquipmentSlot?.ToString() ?? "Equipamento";
            return token.EquipmentValue switch
            {
                FormulaEquipmentValue.LevelBonus => $"Bonus {slotName}",
                FormulaEquipmentValue.DefenseBonus1 => "Bonus defesa 1",
                FormulaEquipmentValue.DefenseBonus2 => "Bonus defesa 2",
                _ => slotName
            };
        }

        private static int EvaluateExpression(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return 0;
            }

            var dt = new DataTable();
            try
            {
                var result = dt.Compute(expression, string.Empty);
                if (decimal.TryParse(result?.ToString() ?? "0", NumberStyles.Any, CultureInfo.InvariantCulture,
                        out var decimalValue))
                {
                    return (int)Math.Round(decimalValue, MidpointRounding.AwayFromZero);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return 0;
        }


        public void AddItemToInventory(ItemInstance? item)
        {
            Inventory.AddItem(item);
        }

        public void RemoveItem(ItemInstance? item)
        {
            Inventory.Remove(item);
        }

        public void Destroy(ItemInstance? item)
        {
            Inventory.Remove(item);
        }

        public void Equip(ItemInstance item, EquipableSlot slot)
        {
            var removedItem = Equipment.Equip(item, slot);
            RemoveItem(item);
            if (removedItem is not null)
            {
                AddItemToInventory(removedItem); 
            }
        }

        public void Unequip(EquipableSlot slot)
        {
            Equipment.Unequip(slot);
        }


        private DamageRollResult RollDamage(ItemInstance weapon, PropertyValue damageProperty,
            GripTypeStats gripTypeDetails, IDiceRoller diceRoller)
        {
            var result = new DamageRollResult();
            var maxValue = gripTypeDetails.Damage;
            var flatBonus = gripTypeDetails.BaseBonusDamage;
            var attributeModifier = gripTypeDetails.AttributeModifier;
            var magicModifier = gripTypeDetails.MagicBonusModifier;
            var damage = diceRoller.Roll(maxValue);
            result.DiceValue = damage;
            result.BonusModifier = attributeModifier;
            result.FlatBonus = flatBonus;
            result.MagicBonus = (weapon.GetBonus) * magicModifier;
            result.AttributeBonus = damageProperty.Value * attributeModifier;
            damage += flatBonus;
            damage += (weapon.GetBonus) * magicModifier;
            damage += damageProperty.Value * attributeModifier;
            Console.WriteLine($"LEVEL: {Level}. DAMAGE: {damage - result.DiceValue}");
            result.TotalDamage = damage;
            result.ReducedDamage = damage;
            // TODO any extra damage * levelModifier;
            return result;
        }

        public int GetTotalBonus(BonusApplication bonusApplication, BonusType type, Property? property)
        {
            var bonuses = this.GetBonus(bonusApplication, type, property);
            return bonuses.SumBonus(bonusApplication, property);
        }


        [NotMapped]
        public List<Bonus> AllBonuses =>
            Bonuses.Concat(CreatureType.GetBonusesOrEmpty())
                .Concat(Archetype.GetBonusesOrEmpty())
                .Concat(Equipment.Bonuses)
                .ToList();

        public List<Bonus> Bonuses { get; set; } = [];

        public void LevelUp()
        {
            Level += 1;
        }

        public void AddPointToAttribute(Guid attributeId)
        {
            var attribute = Attributes.First(a => a.Id == attributeId);
            if (attribute.Points < MaxAttributePoints)
            {
                attribute.Points += 1;
            }
        }     
        public void AddPointToSpecificSkill(Guid specificSkillId)
        {
            var specificSkill = SpecificSkills.First(s => s.Id == specificSkillId);
            var skill = specificSkill.Skill;
            if (specificSkill.Points < skill.PointsLimit)
            {
                specificSkill.Points += 1;
            }
        }      
        public void AddPointToSkill(Guid skillId)
        {
            var skill = Skills.First(s => s.Id == skillId);
            if (skill.Points < skill.PointsLimit)
            {
                skill.Points += 1;
            }
        }

        private int GetRollDices()
        {
            return 4 + Level / 6;
        }
    }

    public record FormulaEvaluationResult(int Value, string Description, string ResolvedFormula);
}


