using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication.Application.Services;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Rolls.Commands;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Rolls.Models;

namespace RoleRollsPocketEdition.Rolls
{
    public class RollService : IRollService
    {

        private readonly RoleRollsDbContext _roleRollsDbContext;
        private readonly ICurrentUser _currentUser;
        private readonly ISceneNotificationService _sceneNotificationService;
        private readonly ICampaignSceneHistoryBuilderService _historyService;

        public RollService(RoleRollsDbContext roleRollsDbContext, ICurrentUser currentUser, ISceneNotificationService sceneNotificationService, ICampaignSceneHistoryBuilderService historyService)
        {
            _roleRollsDbContext = roleRollsDbContext;
            _currentUser = currentUser;
            _sceneNotificationService = sceneNotificationService;
            _historyService = historyService;
        }
        



        public async Task<PagedResult<RollModel>> GetAsync(Guid campaignId, Guid sceneId, PagedRequestInput input)
        {
            var query = from roll in _roleRollsDbContext.Rolls
                .AsNoTracking()
                .Where(roll => roll.SceneId == sceneId)
                        join creature in _roleRollsDbContext.Creatures on roll.ActorId equals creature.Id into joinedCreature
                        from creature in joinedCreature.DefaultIfEmpty()
                        join attribute in _roleRollsDbContext.Attributes on roll.PropertyId equals attribute.Id into joinedAttribute
                        from attribute in joinedAttribute.DefaultIfEmpty()
                        join attributeTemplate in _roleRollsDbContext.AttributeTemplates on attribute.AttributeTemplateId equals attributeTemplate.Id into joinedAttributeTemplate
                        from attributeTemplate in joinedAttributeTemplate.DefaultIfEmpty()

                        join skill in _roleRollsDbContext.Skills on roll.PropertyId equals skill.Id into joinedSkill
                        from skill in joinedSkill.DefaultIfEmpty()
                        join skillTemplate in _roleRollsDbContext.SkillTemplates on skill.SkillTemplateId equals skillTemplate.Id into joinedSkillTemplate
                        from skillTemplate in joinedSkillTemplate.DefaultIfEmpty()

                        join minorSkill in _roleRollsDbContext.MinorSkills on roll.PropertyId equals minorSkill.Id into joinedMinorSkill
                        from minorSkill in joinedMinorSkill.DefaultIfEmpty()
                        join minorSkillTemplate in _roleRollsDbContext.MinorSkillTemplates on minorSkill.SpecificSkillTemplateId equals minorSkillTemplate.Id into joinedMinorSkillTemplate
                        from minorSkillTemplate in joinedMinorSkillTemplate.DefaultIfEmpty()
                        select new
                        {
                            ActorName = (creature != null ? creature.Name : "MASTER"),
                            roll.ActorId,
                            roll.SceneId,
                            roll.Complexity,
                            roll.Difficulty,
                            roll.Id,
                            roll.NumberOfCriticalFailures,
                            roll.NumberOfCriticalSuccesses,
                            roll.NumberOfDices,
                            roll.NumberOfSuccesses,
                            roll.PropertyId,
                            PropertyName = "",
                            roll.PropertyType,
                            roll.RolledDices,
                            roll.Success,
                            AttributeName = (attributeTemplate != null ? attributeTemplate.Name : null),
                            SkillName = (skillTemplate != null ? skillTemplate.Name : null),
                            MinorSkillName = (minorSkillTemplate != null ? minorSkillTemplate.Name : null),
                            roll.DateTime,
                            roll.Description,
                            roll.RollBonus
                        };

   

            var totalCount = await query
                .CountAsync();

            var rolls = await query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .OrderByDescending(roll => roll.DateTime)
                .ToListAsync();
            var output = rolls.Select(roll => new RollModel
            {
                ActorName = roll.ActorName,
                ActorId = roll.ActorId,
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
                DateTime = roll.DateTime,
                Description = roll.Description,
                RollBonus = roll.RollBonus
            }).ToList();

            return new PagedResult<RollModel>
            {
                Items = output,
                TotalCount = totalCount
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
            var creature = await _roleRollsDbContext.Creatures
                .Include(creature => creature.Attributes)
                .Include(creature => creature.Vitalities)
                .Include(creature => creature.Skills)
                .ThenInclude(skill => skill.SpecificSkills)
                .FirstAsync(creature => creature.Id == creatureId);
            var property = creature.GetPropertyValue(input.PropertyType, input.PropertyId);
            var rollCommand = new RollDiceCommand(property.Value, input.PropertyBonus, input.RollBonus + property.Bonus, input.Difficulty, input.Complexity, input.Rolls);
            var roll = new Roll(campaignId, sceneId, creatureId, input.PropertyId, input.PropertyType, input.Hidden, input.Description);
            roll.Process(rollCommand);
            var rollResult = new RollModel(roll);

            await _roleRollsDbContext.AddAsync(roll);
            await _roleRollsDbContext.SaveChangesAsync();
            var history = await _historyService.BuildHistory(roll);
            await _sceneNotificationService.NotifyScene(sceneId, history);
            return rollResult;
        }

        public async Task<RollModel> RollAsync(Guid campaignId, Guid sceneId, RollInput input)
        {
            var rollCommand = new RollDiceCommand(input.PropertyBonus, input.PropertyBonus, input.RollBonus, input.Difficulty, input.Complexity, input.Rolls);
            var roll = new Roll(campaignId, sceneId, _currentUser.User.Id, input.PropertyId, input.PropertyType, input.Hidden, input.Description);
            roll.Process(rollCommand);
            var rollResult = new RollModel(roll);

            await _roleRollsDbContext.AddAsync(roll);
            await _roleRollsDbContext.SaveChangesAsync();
            var history = await _historyService.BuildHistory(roll);
            await _sceneNotificationService.NotifyScene(sceneId, history);
            return rollResult;
        }
    }
}