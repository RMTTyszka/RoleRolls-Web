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
using RoleRollsPocketEdition.Encounters.Validations;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.Encounters.Services;

public interface IEncounterService
{
    Task<EnconterModel?> GetAsync(Guid campaignId, Guid id);
    Task<PagedResult<EnconterModel>> GetAllAsync(Guid campaignId, PagedRequestInput input);
    Task CreateAsync(Guid campaignId, EnconterModel encounter);
    Task UpdateAsync(Guid campaignId, EnconterModel encounterModel);
    Task<EncounterValidationResult> DeleteAsync(Guid id);
    Task<PagedResult<CreatureModel>> GetAllCreaturesAsync(Guid campaignId, Guid encounterId, PagedRequestInput input);
    Task<EncounterValidationResult> AddCreatureAsync(Guid campaignId, Guid encounterId, CreatureModel creatureModel);
    Task<EncounterValidationResult> RemoveCreatureAsync(Guid encounterId, Guid creatureId, bool delete);
}

public class EncounterService : IEncounterService, ITransientDependency
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
            .Include(e => e.Encounters)
            .ThenInclude(e => e.Creatures)
            .Where(e => e.Id == campaignId)
            .Select(c => c.Encounters.First(e => e.Id == id))
            .FirstOrDefaultAsync();
        return entity == null ? null : new EnconterModel(entity);
    }

    public async Task<PagedResult<EnconterModel>> GetAllAsync(Guid campaignId, PagedRequestInput input)
    {
        var query =  _context.Campaigns
            .AsNoTracking()
            .WhereIf(input.Filter.IsNullOrWhiteSpace(), e => e.Name.Contains(input.Filter))
            .Where(e => e.Id == campaignId)
            .Include(e => e.Encounters)
            .SelectMany(c => c.Encounters);
            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input)
            .Select(e => new EnconterModel(e))
            .ToListAsync();
        return new PagedResult<EnconterModel>(totalCount, items);
    }

    public async Task CreateAsync(Guid campaignId, EnconterModel encounter)
    {
        var campaign = await _context.Campaigns
            .FirstAsync(e => e.Id == campaignId);
        var newEncounter = new Encounter(encounter);
        var creatures = await _creatureBuilderService.BuildCreatures(campaignId, encounter.Creatures);
        foreach (var creature in creatures)
        {
            creature.Creature.Id = Guid.NewGuid();
            newEncounter.AddCreature(creature.Creature);
        }

        using (_unitOfWork.Begin())
        {
            await campaign.AddEncounter(newEncounter, _context);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task UpdateAsync(Guid campaignId, EnconterModel encounterModel)
    {
        var campaign = await _context.Campaigns
            .Include(c => c.Encounters)
            .FirstAsync(e => e.Id == campaignId);
        var encounter = campaign.Encounters.First(e => e.Id == encounterModel.Id);
        encounter.Update(encounterModel);

        using (_unitOfWork.Begin())
        {
            _context.Encounters.Update(encounter);
            await _unitOfWork.CommitAsync();
        }
    }

    public async Task<EncounterValidationResult> DeleteAsync(Guid id)
    {
        var encounter = await _context.Encounters.FindAsync(id);
        if (encounter == null)
            return EncounterValidationResult.NotFound();

        _context.Encounters.Remove(encounter);
        await _context.SaveChangesAsync();
        return EncounterValidationResult.Ok();

    }
    public async Task<PagedResult<CreatureModel>> GetAllCreaturesAsync(Guid campaignId, Guid encounterId, PagedRequestInput input)
    {
        var query =  _context.Encounters
            .AsNoTracking()
            .Include(e => e.Creatures)
            .Where(e => e.Id == encounterId)
            .SelectMany(e => e.Creatures)
            .WhereIf(input.Filter.IsNullOrWhiteSpace(), e => e.Name.Contains(input.Filter))
 ;
        var totalCount = await query.CountAsync();
        var itens = await query
            .PageBy(input)
            .Select(e => new CreatureModel(e))
            .ToListAsync();
        var output = new PagedResult<CreatureModel>(totalCount, itens);
        return output;
    }
    public async Task<EncounterValidationResult> AddCreatureAsync(Guid campaignId, Guid encounterId, CreatureModel creatureModel)
    {
        var encounter = await _context.Encounters.FindAsync(encounterId);
        if (encounter == null)
            return EncounterValidationResult.NotFound();
        creatureModel.IsTemplate = false;
        creatureModel.Id = Guid.NewGuid();
        var result = await _creatureBuilderService.BuildCreature(campaignId, creatureModel);

        using (_unitOfWork.Begin())
        {
            await _context.Creatures.AddAsync(result.Creature);
            encounter.AddCreature(result.Creature);
            await _unitOfWork.CommitAsync();
        }

        return EncounterValidationResult.Ok();
    }

    public async Task<EncounterValidationResult> RemoveCreatureAsync(Guid encounterId, Guid creatureId, bool delete)
    {

        var creature = await _context.Creatures.FindAsync(creatureId);

        var encounter = await _context.Encounters.FindAsync(encounterId);
        if (encounter == null)
            return EncounterValidationResult.NotFound();

        encounter.RemoveCreature(creature);
        using (_unitOfWork.Begin())
        {
            if (delete)
            {
                _context.Creatures.Remove(creature);
            }
            await _unitOfWork.CommitAsync();
        }
        return EncounterValidationResult.Ok();
    }
}