using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Domain.Commands;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public class RollService
    {
        private readonly RoleRollsDbContext _roleRollsDbContext;

        public RollService(RoleRollsDbContext roleRollsDbContext)
        {
            this._roleRollsDbContext = roleRollsDbContext;
        }


        public async Task<RollModel> RollAsync(Guid campaignId, Guid creatureId, RollInput input)
        {
            var creature = await _roleRollsDbContext.Creatures.FindAsync(creatureId);
            var property= GetPropertyValue(creature, input.PropertyType, input.PropertyId);
            var rollCommand = new RollDiceCommand(property.propertyValue, property.rollBonus + input.RollBonus, input.PropertyBonus, input.Difficulty, input.Complexity);
            var roll = new Roll(campaignId, creatureId, input.Hidden);
            roll.Process(rollCommand);
            var rollResult = new RollModel(roll);

            await _roleRollsDbContext.AddAsync(roll);
            await _roleRollsDbContext.SaveChangesAsync();
            return rollResult;
        }      

        private (int propertyValue, int rollBonus) GetPropertyValue(Creature creature, RollPropertyType propertyType, Guid propertyId)
        {
            switch (propertyType)
            {
                case RollPropertyType.Attribute:
                    return (creature.Attributes.First(attribute => attribute.Id == propertyId).Value, 0);
                case RollPropertyType.Skill:
                    return (creature.Skills.First(skill => skill.Id == propertyId).Value, 0);
                case RollPropertyType.MinorSkill:
                    var minorSkill = creature.Skills.SelectMany(skill => skill.MinorSkills).First(minorSkill => minorSkill.Id == propertyId);
                    var skill = creature.Skills.First(skill => skill.Id == minorSkill.SkillId);
                    var rollBonus = minorSkill.GetProficiency();
                    return (skill.Value, rollBonus);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}