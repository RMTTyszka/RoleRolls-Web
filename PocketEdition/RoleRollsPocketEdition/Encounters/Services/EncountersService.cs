using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Core.EntityFramework;
using RoleRollsPocketEdition.Core.Extensions;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Creatures.Services;
using RoleRollsPocketEdition.Encounters.Entities;
using RoleRollsPocketEdition.Encounters.Models;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Encounters.Services;

public class EncounterService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RoleRollsDbContext _context;
    private readonly ICreatureBuilderService _creatureBuilderService;

    public EncounterService(IUnitOfWork unitOfWork, RoleRollsDbContext context, ICreatureBuilderService creatureBuilderService)
    {
        _unitOfWork = unitOfWork;
        _context = context;
        _creatureBuilderService = creatureBuilderService;
    }


    public async Task<EnconterModel?> GetAsync(Guid campaignId, Guid id)
    {
        var entity = await _context.Campaigns
            .AsNoTracking()
            .Include(e => e.Enconters).Where(e => e.Id == campaignId)
            .Select(c => c.Enconters.First(e => e.Id == id))
            .FirstOrDefaultAsync();
        return entity == null ? null : new EnconterModel(entity);
    }

    public async Task<IEnumerable<EnconterModel>> GetAllAsync(Guid campaignId, PagedRequestInput input)
    {
        var output = await _context.Campaigns
            .AsNoTracking()
            .WhereIf(input.Filter.IsNullOrWhiteSpace(), e => e.Name.Contains(input.Filter))
            .Where(e => e.Id == campaignId)
            .Include(e => e.Enconters)
            .SelectMany(c => c.Enconters)
            .PageBy(input)
            .Select(e => new EnconterModel(e))
            .ToListAsync();
        return output;
    }

    public async Task CreateAsync(Guid campaignId, EnconterModel encounter)
    {
        var campaign = await _context.Campaigns
            .FirstAsync(e => e.Id == campaignId);
        var newEncounter = new Enconter(encounter);

        using (_unitOfWork.Begin())
        {
            await campaign.AddEncounter(newEncounter, _context);
            await _unitOfWork.Commit();
        }
    }

    public async Task UpdateAsync(Guid campaignId, EnconterModel encounterModel)
    {
        var campaign = await _context.Campaigns
            .Include(c => c.Enconters)
            .FirstAsync(e => e.Id == campaignId);
        var encounter = campaign.Enconters.First(e => e.Id == encounterModel.Id);
        encounter.Update(encounterModel);

        using (_unitOfWork.Begin())
        {
            _context.Encounters.Update(encounter);
            await _unitOfWork.Commit();
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var encounter = await _context.Encounters.FindAsync(id);
        if (encounter == null)
            return false;
        _context.Encounters.Remove(encounter);
        return true;
    }

    public async Task<bool> AddCreatureAsync(Guid campaignId, Guid encounterId, CreatureModel creatureModel)
    {
        var encounter = await _context.Encounters.FindAsync(encounterId);
        if (encounter == null)
            return false;
        var result = await _creatureBuilderService.BuildCreature(campaignId, creatureModel);

        await _context.Creatures.AddAsync(result.Creature);
        encounter.AddCreature(result.Creature);
        return true;
    }

    public async Task<bool> RemoveCreatureAsync(Guid encounterId, Creature creature)
    {
        if (creature == null)
            throw new ArgumentNullException(nameof(creature), "Creature cannot be null");

        var encounter = await _encounterRepository.GetByIdAsync(encounterId);
        if (encounter == null)
            return false;

        var result = encounter.RemoveCreature(creature);
        if (result)
        {
            await _encounterRepository.UpdateAsync(encounter);
        }
        return result;
    }

    public async Task<bool> RemoveCreatureByIdAsync(Guid encounterId, Guid creatureId)
    {
        var encounter = await _encounterRepository.GetByIdAsync(encounterId);
        if (encounter == null)
            return false;

        var result = encounter.RemoveCreatureById(creatureId);
        if (result)
        {
            await _encounterRepository.UpdateAsync(encounter);
        }
        return result;
    }
}