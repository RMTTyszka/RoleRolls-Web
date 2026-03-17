using RoleRollsPocketEdition.Campaigns;
using Microsoft.EntityFrameworkCore;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Campaigns.Models;
using RoleRollsPocketEdition.Core.Abstractions;
using RoleRollsPocketEdition.Core.Authentication.Users;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Encounters.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Scenes.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes;

public class LandOfHeroesAdminLoader : IStartupTask
{
    private static readonly Guid AdminUserId = Guid.Parse("8B54D776-6A3D-4F0E-A983-7B884E6E4C57");
    private static readonly Guid AdminCampaignId = Guid.Parse("D06C598A-2D4D-4C91-9D10-8872B4B3E40B");
    private static readonly Guid Hero1Id = Guid.Parse("739E5167-06F6-4D50-94F6-4C1D4B8C3D73");
    private static readonly Guid Hero2Id = Guid.Parse("243E680D-8B17-44C4-9C6E-BFB4C36B0A5F");
    private static readonly Guid Enemy1Id = Guid.Parse("9C8FB4B0-B3E5-44A4-B6A6-0FE81C162EB5");
    private static readonly Guid Enemy2Id = Guid.Parse("9A011BB3-7E0C-4F37-B98E-FA8353FD98A2");
    private static readonly Guid EncounterId = Guid.Parse("206BF0F8-4B62-4EEE-8444-4B4E69666E20");
    private static readonly Guid SceneId = Guid.Parse("B56D2A6A-9107-4851-A621-B82A3E68A3FF");
    private const string AdminLogin = "admin";
    private const string AdminPassword = "123qwe";
    private const string AdminCampaignName = "Land Of Heroes";
    private const string Hero1Name = "Hero 1";
    private const string Hero2Name = "Hero 2";
    private const string Enemy1Name = "Enemy 1";
    private const string Enemy2Name = "Enemy 2";
    private const string EncounterName = "Land Of Heroes Encounter";
    private const string SceneName = "Land Of Heroes Scene";

    private readonly RoleRollsDbContext _dbContext;
    private readonly ICampaignsService _campaignsService;
    private readonly ICampaignRepository _campaignRepository;

    public LandOfHeroesAdminLoader(
        RoleRollsDbContext dbContext,
        ICampaignsService campaignsService,
        ICampaignRepository campaignRepository)
    {
        _dbContext = dbContext;
        _campaignsService = campaignsService;
        _campaignRepository = campaignRepository;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken = default)
    {
        await EnsureLandOfHeroesTemplateAsync(cancellationToken);

        var adminUser = await EnsureAdminUserAsync(cancellationToken);
        await EnsureAdminCampaignAsync(adminUser, cancellationToken);
        await EnsureSeedDataAsync(adminUser.Id, cancellationToken);
    }

    private async Task<User> EnsureAdminUserAsync(CancellationToken cancellationToken)
    {
        var adminUser = await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Login == AdminLogin || user.Email == AdminLogin, cancellationToken);

        if (adminUser is null)
        {
            adminUser = new User
            {
                Id = AdminUserId,
                Login = AdminLogin,
                Email = AdminLogin
            };
            adminUser.HashPassword(AdminPassword);

            await _dbContext.Users.AddAsync(adminUser, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        return adminUser;
    }

    private async Task EnsureAdminCampaignAsync(User adminUser, CancellationToken cancellationToken)
    {
        var campaignExists = await _dbContext.Campaigns
            .AnyAsync(campaign =>
                    campaign.Id == AdminCampaignId,
                cancellationToken);

        if (campaignExists)
        {
            return;
        }

        await _campaignsService.CreateAsync(new CampaignCreateInput
        {
            Id = AdminCampaignId,
            Name = AdminCampaignName,
            MasterId = adminUser.Id,
            CampaignTemplateId = LandOfHeroesTemplate.Template.Id
        });
    }

    private async Task EnsureSeedDataAsync(Guid ownerId, CancellationToken cancellationToken)
    {
        var campaign = await _dbContext.Campaigns
            .Include(campaign => campaign.Encounters)
            .ThenInclude(encounter => encounter.Creatures)
            .FirstAsync(campaign => campaign.Id == AdminCampaignId, cancellationToken);

        var template = await _campaignRepository.GetCreatureTemplateAggregateAsync(campaign.CampaignTemplateId);

        var creatures = await _dbContext.Creatures
            .Where(creature => creature.CampaignId == campaign.Id &&
                               (creature.Id == Hero1Id ||
                                creature.Id == Hero2Id ||
                                creature.Id == Enemy1Id ||
                                creature.Id == Enemy2Id))
            .ToDictionaryAsync(creature => creature.Id, cancellationToken);

        await EnsureCreatureAsync(
            creatures,
            template,
            campaign.Id,
            ownerId,
            Hero1Id,
            Hero1Name,
            CreatureCategory.Hero,
            attributePoints: 3,
            specificSkillPoints: 2,
            cancellationToken);
        await EnsureCreatureAsync(
            creatures,
            template,
            campaign.Id,
            ownerId,
            Hero2Id,
            Hero2Name,
            CreatureCategory.Hero,
            attributePoints: 3,
            specificSkillPoints: 2,
            cancellationToken);
        var enemy1 = await EnsureCreatureAsync(
            creatures,
            template,
            campaign.Id,
            ownerId,
            Enemy1Id,
            Enemy1Name,
            CreatureCategory.Monster,
            attributePoints: 2,
            specificSkillPoints: 1,
            cancellationToken);
        var enemy2 = await EnsureCreatureAsync(
            creatures,
            template,
            campaign.Id,
            ownerId,
            Enemy2Id,
            Enemy2Name,
            CreatureCategory.Monster,
            attributePoints: 2,
            specificSkillPoints: 1,
            cancellationToken);

        var encounter = campaign.Encounters.FirstOrDefault(existingEncounter => existingEncounter.Id == EncounterId);
        if (encounter is null)
        {
            encounter = new Encounter
            {
                Id = EncounterId,
                Name = EncounterName
            };

            await campaign.AddEncounter(encounter, _dbContext);
        }

        EnsureEncounterContainsCreature(encounter, enemy1);
        EnsureEncounterContainsCreature(encounter, enemy2);

        await EnsureSceneAsync(campaign.Id, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Creature> EnsureCreatureAsync(
        Dictionary<Guid, Creature> existingCreatures,
        CampaignTemplate template,
        Guid campaignId,
        Guid ownerId,
        Guid creatureId,
        string creatureName,
        CreatureCategory category,
        int attributePoints,
        int specificSkillPoints,
        CancellationToken cancellationToken)
    {
        if (existingCreatures.TryGetValue(creatureId, out var existingCreature))
        {
            return existingCreature;
        }

        var creature = CreateSeedCreature(
            template,
            campaignId,
            ownerId,
            creatureId,
            creatureName,
            category,
            attributePoints,
            specificSkillPoints);

        existingCreatures[creatureId] = creature;
        await _dbContext.Creatures.AddAsync(creature, cancellationToken);
        return creature;
    }

    private static Creature CreateSeedCreature(
        CampaignTemplate template,
        Guid campaignId,
        Guid ownerId,
        Guid creatureId,
        string creatureName,
        CreatureCategory category,
        int attributePoints,
        int specificSkillPoints)
    {
        var creature = template.InstantiateCreature(creatureName, creatureId, campaignId, category, ownerId, false);

        creature.Inventory.Creature = creature;
        creature.Equipment.Creature = creature;

        foreach (var attribute in creature.Attributes)
        {
            attribute.Creature = creature;
            attribute.Points = attributePoints;
        }

        foreach (var skill in creature.Skills)
        {
            skill.Points = Math.Min(skill.PointsLimit, specificSkillPoints);

            foreach (var specificSkill in skill.SpecificSkills)
            {
                specificSkill.Points = Math.Min(skill.PointsLimit, specificSkillPoints);
            }
        }

        foreach (var vitality in creature.Vitalities)
        {
            vitality.Creature = creature;
            vitality.Value = vitality.CalculateMaxValue(creature);
        }

        foreach (var defense in creature.Defenses)
        {
            defense.Creature = creature;
        }

        return creature;
    }

    private static void EnsureEncounterContainsCreature(Encounter encounter, Creature creature)
    {
        if (encounter.Creatures.Any(existingCreature => existingCreature.Id == creature.Id))
        {
            return;
        }

        encounter.AddCreature(creature);
    }

    private async Task EnsureSceneAsync(Guid campaignId, CancellationToken cancellationToken)
    {
        var scene = await _dbContext.CampaignScenes
            .FirstOrDefaultAsync(existingScene => existingScene.Id == SceneId, cancellationToken);

        if (scene is null)
        {
            scene = new Scene
            {
                Id = SceneId,
                CampaignId = campaignId,
                Name = SceneName,
                Status = SceneStatus.Active
            };

            await _dbContext.CampaignScenes.AddAsync(scene, cancellationToken);
        }
        else
        {
            scene.Name = SceneName;
            scene.Status = SceneStatus.Active;
        }

        var existingCreatureIds = await _dbContext.SceneCreatures
            .Where(sceneCreature => sceneCreature.SceneId == SceneId)
            .Select(sceneCreature => sceneCreature.CreatureId)
            .ToListAsync(cancellationToken);

        var sceneCreatures = new[]
        {
            CreateSceneCreatureIfMissing(existingCreatureIds, Hero1Id, CreatureCategory.Hero),
            CreateSceneCreatureIfMissing(existingCreatureIds, Hero2Id, CreatureCategory.Hero),
            CreateSceneCreatureIfMissing(existingCreatureIds, Enemy1Id, CreatureCategory.Monster),
            CreateSceneCreatureIfMissing(existingCreatureIds, Enemy2Id, CreatureCategory.Monster)
        }.Where(sceneCreature => sceneCreature is not null).Cast<SceneCreature>().ToList();

        if (sceneCreatures.Count > 0)
        {
            await _dbContext.SceneCreatures.AddRangeAsync(sceneCreatures, cancellationToken);
        }
    }

    private static SceneCreature? CreateSceneCreatureIfMissing(
        ICollection<Guid> existingCreatureIds,
        Guid creatureId,
        CreatureCategory category)
    {
        if (existingCreatureIds.Contains(creatureId))
        {
            return null;
        }

        return new SceneCreature
        {
            Id = Guid.NewGuid(),
            SceneId = SceneId,
            CreatureId = creatureId,
            CreatureCategory = category,
            Hidden = false
        };
    }

    private async Task EnsureLandOfHeroesTemplateAsync(CancellationToken cancellationToken)
    {
        var templateExists = await _dbContext.CampaignTemplates
            .AnyAsync(template => template.Id == LandOfHeroesTemplate.Template.Id, cancellationToken);

        if (templateExists)
        {
            return;
        }

        await _dbContext.CampaignTemplates.AddAsync(LandOfHeroesTemplate.Template, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
