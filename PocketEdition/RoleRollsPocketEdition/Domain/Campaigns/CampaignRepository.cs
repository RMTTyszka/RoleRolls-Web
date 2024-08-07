﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.CreaturesTemplates.Entities;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Campaigns.Domain
{
    public class CampaignRepository : ICampaignRepository
    {
        private readonly RoleRollsDbContext _dbContext;

        public CampaignRepository(RoleRollsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreatureTemplate> GetCreatureTemplateAggregateAsync(Guid id)
        {
            var creatureTemplate = await _dbContext.CreatureTemplates
            .Include(template => template.Attributes)
            .Include(template => template.Lifes)
            .Include(template => template.Defenses)
            .Include(template => template.Skills).ThenInclude(skill => skill.MinorSkills)
            .FirstAsync(template => template.Id == id);
            return creatureTemplate;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
