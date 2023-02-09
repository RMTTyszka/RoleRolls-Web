﻿using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Global.Dtos;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Domain.Commands;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public class RollService : IRollService
    {
        private readonly RoleRollsDbContext _roleRollsDbContext;

        public RollService(RoleRollsDbContext roleRollsDbContext)
        {
            this._roleRollsDbContext = roleRollsDbContext;
        }

        public async Task<PagedResult<RollModel>> GetAsync(Guid campaignId, Guid sceneId, PagedRequestInput input)
        {
            var query = from roll in _roleRollsDbContext.Rolls
                .Where(roll => roll.CampaignId == campaignId && roll.SceneId == sceneId)
                        join creature in _roleRollsDbContext.Creatures on roll.ActorId equals creature.Id into joinedCreature
                        from creature in joinedCreature.DefaultIfEmpty()
                        join attribute in _roleRollsDbContext.Attributes on roll.PropertyId equals attribute.Id into joinedAttribute
                        from attribute in joinedAttribute.DefaultIfEmpty()
                        join attributeTemplate in _roleRollsDbContext.AttributeTemplates on attribute.Id equals attributeTemplate.Id into joinedAttributeTemplate
                        from attributeTemplate in joinedAttributeTemplate.DefaultIfEmpty()

                        join skill in _roleRollsDbContext.Skills on roll.PropertyId equals skill.Id into joinedSkill
                        from skill in joinedSkill.DefaultIfEmpty()
                        join skillTemplate in _roleRollsDbContext.SkillTemplates on skill.Id equals skillTemplate.Id into joinedSkillTemplate
                        from skillTemplate in joinedSkillTemplate.DefaultIfEmpty()

                        join minorSkill in _roleRollsDbContext.MinorSkills on roll.PropertyId equals minorSkill.Id into joinedMinorSkill
                        from minorSkill in joinedMinorSkill.DefaultIfEmpty()
                        join minorSkillTemplate in _roleRollsDbContext.MinorSkillTemplates on minorSkill.Id equals minorSkillTemplate.Id into joinedMinorSkillTemplate
                        from minorSkillTemplate in joinedMinorSkillTemplate.DefaultIfEmpty()
                        select new
                        {
                            ActorName = (creature != null ? creature.Name : "MASTER"),
                            ActorId = roll.ActorId,
                            CampaignId = roll.CampaignId,
                            SceneId = roll.SceneId,
                            Complexity = roll.Complexity,
                            Difficulty = roll.Difficulty,
                            Id = roll.Id,
                            NumberOfCriticalFailures = roll.NumberOfCriticalFailures,
                            NumberOfCriticalSuccesses = roll.NumberOfCriticalSuccesses,
                            NumberOfDices = roll.NumberOfDices,
                            NumberOfSuccesses = roll.NumberOfSuccesses,
                            PropertyId = roll.PropertyId,
                            PropertyName = "",
                            PropertyType = roll.PropertyType,
                            RolledDices = roll.RolledDices,
                            Success = roll.Success,
                            AttributeName = (attributeTemplate != null ? attributeTemplate.Name : null),
                            SkillName = (skillTemplate != null ? skillTemplate.Name : null),
                            MinorSkillName = (minorSkillTemplate != null ? minorSkillTemplate.Name : null),
                        };

                    var rolls = await query
                .ToListAsync();
            var output = rolls.Select(roll => new RollModel
            {
                ActorName = roll.ActorName,
                ActorId = roll.ActorId,
                CampaignId = roll.CampaignId,
                SceneId = roll.SceneId,
                Complexity = roll.Complexity,
                Difficulty = roll.Difficulty,
                Id = roll.Id,
                NumberOfCriticalFailures = roll.NumberOfCriticalFailures,
                NumberOfCriticalSuccesses = roll.NumberOfCriticalSuccesses,
                NumberOfDices = roll.NumberOfDices,
                NumberOfSuccesses = roll.NumberOfSuccesses,
                PropertyId = roll.PropertyId,
                PropertyName = roll.AttributeName ?? roll.SkillName ?? roll.MinorSkillName,
                PropertyType = roll.PropertyType,
                RolledDices = roll.RolledDices,
                Success = roll.Success,
            }).ToList();

            var totalCount = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .CountAsync();

            return new PagedResult<RollModel>()
            {
                Content = output,
                TotalElements = totalCount
            };

        }

        public async Task<RollModel?> GetAsync(Guid campaignId, Guid sceneId, Guid id)
        {
            var roll = await _roleRollsDbContext.Rolls.FindAsync(id);
            if (roll is null) 
            {
                return null;
            }
            return new RollModel(roll);
        }

        public async Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, Guid creatureId, RollInput input)
        {
            var creature = await _roleRollsDbContext.Creatures.FindAsync(creatureId);
            var property = GetPropertyValue(creature, input.PropertyType, input.PropertyId);
            var rollCommand = new RollDiceCommand(property.propertyValue, property.rollBonus + input.RollBonus, input.PropertyBonus, input.Difficulty, input.Complexity);
            var roll = new Roll(campaignId, sceneId, creatureId, input.Hidden);
            roll.Process(rollCommand);
            var rollResult = new RollModel(roll);

            await _roleRollsDbContext.AddAsync(roll);
            await _roleRollsDbContext.SaveChangesAsync();
            return rollResult;
        }

        public async Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, RollInput input)
        {
            var rollCommand = new RollDiceCommand(input.PropertyBonus, input.RollBonus, 0, input.Difficulty, input.Complexity);
            var roll = new Roll(campaignId, sceneId, null, input.Hidden);
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