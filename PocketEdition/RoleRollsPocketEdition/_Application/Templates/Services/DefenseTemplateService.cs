﻿using MassTransit;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.Campaigns.Events.Defenses;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Services
{
    public class DefenseTemplateService : IDefenseTemplateService
    {
        private readonly RoleRollsDbContext _dbContext;
        private readonly IBus _bus;

        public DefenseTemplateService(RoleRollsDbContext dbContext, IBus bus)
        {
            _dbContext = dbContext;
            _bus = bus;
        }

        public async Task CreateAsync(Guid creatureTemplateId, DefenseTemplateModel model)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);

            var defenseAdded = await template.AddDefenseAsync(model, _dbContext);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(defenseAdded);
        }      

        public async Task UpdateAsync(Guid creatureTemplateId, DefenseTemplateModel model)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);
            
            var defenseUpdated = template.UpdateDefense(model, _dbContext);
            await _dbContext.SaveChangesAsync();
            await _bus.Publish(defenseUpdated);
        }
        public async Task RemoveAsync(Guid creatureTemplateId, Guid defenseTemplateId)
        {
            var template = await _dbContext.CampaignTemplates
                .Include(template => template.Attributes)
                .Include(template => template.Skills)
                .ThenInclude(skill => skill.MinorSkills)
                .Include(template => template.Lifes)
                .Include(template => template.Defenses)
                .FirstAsync(template => template.Id == creatureTemplateId);
            template.RemoveDefense(defenseTemplateId, _dbContext);
            await _bus.Publish(new DefenseTemplateRemoved()
            {
                DefenseTemplateId = defenseTemplateId,
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
