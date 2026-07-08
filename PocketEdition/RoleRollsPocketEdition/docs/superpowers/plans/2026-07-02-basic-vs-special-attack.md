# Basic Vs Special Attack Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Separate `basic attack` and `special attack` into distinct HTTP contracts, services, domain flows, tests, and docs so code structure enforces the business rule difference.

**Architecture:** Keep `basic attack` as weapon/config-driven combat with automatic damage and vitality processing. Add a separate `special attack` flow that resolves `MinorSkill x Defense` with no weapon, no `ItemConfiguration`, and no automatic consequence beyond the roll result. Keep the legacy `/attacks` route as a `basic attack` alias only.

**Tech Stack:** ASP.NET Core `net10.0`, EF Core 10, xUnit, FluentAssertions, NSubstitute

---

User instruction override: do **not** create commits in this workspace. The usual commit checkbox is intentionally omitted from every task.

## File Map

### New files

- `Attacks/Models/BasicAttackInput.cs`
  Public request contract for weapon/config-driven attacks only.
- `Attacks/Models/BasicAttackResponse.cs`
  Public response contract for `basic attack`.
- `Attacks/Models/SpecialAttackInput.cs`
  Public request contract for `MinorSkill x Defense`.
- `Attacks/Models/SpecialAttackResponse.cs`
  Public response contract for `special attack`.
- `Attacks/Services/SpecialAttackService.cs`
  Loads attacker/target, builds `SpecialAttackCommand`, calls `Creature.SpecialAttack`, writes scene action, maps response.
- `Creatures/Entities/CreatureSpecialAttack.cs`
  Domain roll resolution for `MinorSkill x Defense`.
- `Scenes/Services/SceneActionDescriptionBuilder.cs`
  Central place for `basic` vs `special` scene text generation.
- `UnitTests/Attacks/Services/AttackServiceTests/SpecialAttackTests.cs`
  TDD coverage for the new special attack flow.
- `UnitTests/Scenes/Controllers/SceneCreaturesControllerRouteTests.cs`
  Reflection-based contract tests for the three attack routes.
- `UnitTests/Scenes/Services/SceneActionDescriptionBuilderTests.cs`
  Verifies `special attack` history text never mentions weapon or damage.

### Moved files

- `Attacks/Services/AttackService.cs` -> `Attacks/Services/BasicAttackService.cs`
  Rename the generic file to match the now-explicit `basic attack` concept.
- `Creatures/Entities/CreatureAttack.cs` -> `Creatures/Entities/CreatureBasicAttack.cs`
  Rename the generic domain file to match the weapon/config combat flow.

### Modified files

- `Scenes/Controllers/SceneCreaturesController.cs`
  Add `/basic-attacks` and `/special-attacks`; keep `/attacks` as `basic attack` alias.
- `Scenes/Services/IScenesService.cs`
  Split scene processing into `ProcessBasicAttackAction` and `ProcessSpecialAttackAction`.
- `Scenes/Services/ScenesService.cs`
  Replace generic damage text builder with explicit `basic` and `special` action handling.
- `Creatures/Entities/CreatureDefend.cs`
  Swap generic `AttackCommand` usage to `BasicAttackCommand`.
- `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
  Update existing tests to use `BasicAttackCommand` and `Creature.BasicAttack`.
- `docs/rolerolls-regras-de-negocio.md`
  Add separate sections for `Ataque Básico`, `Ataque Especial`, and `Diferença Estrutural`.
- `docs/rolerolls-mapa-tecnico.md`
  Add separate entry points and file mapping for `basic attack` vs `special attack`.

## Task 1: Split HTTP Contracts And Routes

**Files:**
- Create: `Attacks/Models/BasicAttackInput.cs`
- Create: `Attacks/Models/BasicAttackResponse.cs`
- Create: `Attacks/Models/SpecialAttackInput.cs`
- Create: `Attacks/Models/SpecialAttackResponse.cs`
- Modify: `Scenes/Controllers/SceneCreaturesController.cs:10-55`
- Modify: `Attacks/Services/BasicAttackService.cs:17-183`
- Create: `UnitTests/Scenes/Controllers/SceneCreaturesControllerRouteTests.cs`

- [ ] **Step 1: Write the failing route contract test**

```csharp
using System.Reflection;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Attacks.Models;
using RoleRollsPocketEdition.Scenes.Controllers;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Controllers;

public class SceneCreaturesControllerRouteTests
{
    [Fact]
    public void AttackRoutes_ShouldExposeBasicSpecialAndLegacyEndpoints()
    {
        var methods = typeof(SceneCreaturesController)
            .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        var basic = methods.Single(m => m.Name == "BasicAttack");
        var special = methods.Single(m => m.Name == "SpecialAttack");
        var legacy = methods.Single(m => m.Name == "Attack");

        basic.GetCustomAttribute<HttpPostAttribute>()!.Template.Should().Be("{creatureId}/basic-attacks");
        special.GetCustomAttribute<HttpPostAttribute>()!.Template.Should().Be("{creatureId}/special-attacks");
        legacy.GetCustomAttribute<HttpPostAttribute>()!.Template.Should().Be("{creatureId}/attacks");

        basic.GetParameters().Single(p => p.Name == "input").ParameterType.Should().Be(typeof(BasicAttackInput));
        special.GetParameters().Single(p => p.Name == "input").ParameterType.Should().Be(typeof(SpecialAttackInput));
        legacy.GetParameters().Single(p => p.Name == "input").ParameterType.Should().Be(typeof(BasicAttackInput));
    }
}
```

- [ ] **Step 2: Run the route test to verify it fails**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Scenes.Controllers.SceneCreaturesControllerRouteTests
```

Expected:

- build/test fails because `BasicAttackInput`, `SpecialAttackInput`, `BasicAttack`, and `SpecialAttack` do not exist yet

- [ ] **Step 3: Write the minimal contracts and controller actions**

```csharp
// Attacks/Models/BasicAttackInput.cs
namespace RoleRollsPocketEdition.Attacks.Models;

public class BasicAttackInput
{
    public Guid TargetId { get; set; }
    public Guid? DefenseId { get; set; }
    public Property? VitalityId { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
    public List<Guid> CombatManeuverIds { get; set; } = [];
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

// Attacks/Models/SpecialAttackInput.cs
namespace RoleRollsPocketEdition.Attacks.Models;

public class SpecialAttackInput
{
    public Guid TargetId { get; set; }
    public Guid SpecialSkillId { get; set; }
    public Guid DefenseId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

// Attacks/Services/BasicAttackService.cs (interface only for this task)
public interface IBasicAttackService
{
    Task<BasicAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, BasicAttackInput input);
}

// Attacks/Services/SpecialAttackService.cs (interface only for this task)
public interface ISpecialAttackService
{
    Task<SpecialAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, SpecialAttackInput input);
}

// Scenes/Controllers/SceneCreaturesController.cs
[HttpPost("{creatureId}/basic-attacks")]
public Task<BasicAttackResponse> BasicAttack(
    [FromRoute] Guid campaignId,
    [FromRoute] Guid sceneId,
    [FromRoute] Guid creatureId,
    [FromBody] BasicAttackInput input)
{
    return _basicAttackService.Attack(campaignId, sceneId, creatureId, input);
}

[HttpPost("{creatureId}/special-attacks")]
public Task<SpecialAttackResponse> SpecialAttack(
    [FromRoute] Guid campaignId,
    [FromRoute] Guid sceneId,
    [FromRoute] Guid creatureId,
    [FromBody] SpecialAttackInput input)
{
    return _specialAttackService.Attack(campaignId, sceneId, creatureId, input);
}

[HttpPost("{creatureId}/attacks")]
public Task<BasicAttackResponse> Attack(
    [FromRoute] Guid campaignId,
    [FromRoute] Guid sceneId,
    [FromRoute] Guid creatureId,
    [FromBody] BasicAttackInput input)
{
    return _basicAttackService.Attack(campaignId, sceneId, creatureId, input);
}
```

- [ ] **Step 4: Run the route test again**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Scenes.Controllers.SceneCreaturesControllerRouteTests
```

Expected:

- `Passed!`
- `Failed: 0`

## Task 2: Add The Special Attack Domain Flow

**Files:**
- Create: `Creatures/Entities/CreatureSpecialAttack.cs`
- Create: `Attacks/Services/SpecialAttackService.cs`
- Modify: `Scenes/Services/IScenesService.cs:8-20`
- Modify: `Scenes/Services/ScenesService.cs:135-182`
- Create: `UnitTests/Attacks/Services/AttackServiceTests/SpecialAttackTests.cs`

- [ ] **Step 1: Write the failing special attack tests**

```csharp
using FluentAssertions;
using NSubstitute;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates.Skills;
using RoleRollsPocketEdition.Rolls.Services;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests;

public class SpecialAttackTests
{
    [Fact]
    public void SpecialAttack_ShouldResolveMinorSkillAgainstExplicitDefense()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Caster").Creature;
        var defender = new BaseCreature(template, "Target").Creature;

        var command = new SpecialAttackCommand
        {
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Bluff],
            DefenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion],
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(20);

        var result = attacker.SpecialAttack(defender, command, dice);

        result.Success.Should().BeTrue();
        result.SpecialSkillId.Should().Be(command.SpecialSkillId);
        result.DefenseId.Should().Be(command.DefenseId);
        result.RolledDices.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void SpecialAttack_ShouldNotChangeTargetVitalitiesOrRequireWeapon()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Caster").Creature;
        var defender = new BaseCreature(template, "Target").Creature;
        var before = defender.Vitalities.Select(v => (v.VitalityTemplateId, v.Value)).ToList();

        var command = new SpecialAttackCommand
        {
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Arcane],
            DefenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion],
            Luck = 0,
            Advantage = 0
        };

        var dice = Substitute.For<IDiceRoller>();
        dice.Roll(20).Returns(1);

        var result = attacker.SpecialAttack(defender, command, dice);

        result.Success.Should().BeFalse();
        defender.Vitalities.Select(v => (v.VitalityTemplateId, v.Value)).Should().BeEquivalentTo(before);
    }
}
```

- [ ] **Step 2: Run the special attack tests to verify they fail**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests.SpecialAttackTests
```

Expected:

- build/test fails because `SpecialAttackCommand`, `SpecialAttackResult`, and `Creature.SpecialAttack` do not exist yet

- [ ] **Step 3: Write the minimal special attack implementation**

```csharp
// Attacks/Services/SpecialAttackService.cs
public class SpecialAttackCommand
{
    public Guid SpecialSkillId { get; set; }
    public Guid DefenseId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

public class SpecialAttackResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Target { get; set; } = null!;
    public Guid SpecialSkillId { get; set; }
    public Guid DefenseId { get; set; }
    public bool Success { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public int Difficulty { get; set; }
    public int Complexity { get; set; }
    public int NumberOfSuccesses { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Bonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
}

public class SpecialAttackService : ISpecialAttackService, ITransientDependency
{
    public async Task<SpecialAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, SpecialAttackInput input)
    {
        var attacker = await LoadCreature(attackerId);
        var target = await LoadCreature(input.TargetId);
        var result = attacker.SpecialAttack(target, new SpecialAttackCommand
        {
            SpecialSkillId = input.SpecialSkillId,
            DefenseId = input.DefenseId,
            Luck = input.Luck,
            Advantage = input.Advantage
        }, _diceRoller);

        await _scenesService.ProcessSpecialAttackAction(sceneId, result);
        return SpecialAttackResponse.From(result);
    }
}

// Creatures/Entities/CreatureSpecialAttack.cs
public partial class Creature
{
    public SpecialAttackResult SpecialAttack(Creature target, SpecialAttackCommand input, IDiceRoller diceRoller)
    {
        var property = new Property(input.SpecialSkillId, PropertyType.MinorSkill);
        var defenseValue = target.DefenseValue(input.DefenseId);
        var roll = ExecuteRoll(property, null, input.Advantage, 0, 1, defenseValue, input.Luck, 20, diceRoller);

        return new SpecialAttackResult
        {
            Attacker = this,
            Target = target,
            SpecialSkillId = input.SpecialSkillId,
            DefenseId = input.DefenseId,
            Success = roll.Success,
            RolledDices = roll.RolledDices,
            Difficulty = roll.Difficulty,
            Complexity = roll.Complexity,
            NumberOfSuccesses = roll.NumberOfSuccesses,
            NumberOfRollSuccesses = roll.NumberOfRollSuccesses,
            Bonus = roll.Bonus,
            Luck = roll.Luck,
            Advantage = roll.Advantage
        };
    }
}
```

- [ ] **Step 4: Run the special attack tests again**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests.SpecialAttackTests
```

Expected:

- `Passed!`
- `Failed: 0`

## Task 3: Refactor Basic Attack And Scene History Into Explicit Flows

**Files:**
- Move: `Attacks/Services/AttackService.cs` -> `Attacks/Services/BasicAttackService.cs`
- Move: `Creatures/Entities/CreatureAttack.cs` -> `Creatures/Entities/CreatureBasicAttack.cs`
- Modify: `Creatures/Entities/CreatureDefend.cs`
- Modify: `UnitTests/Attacks/Services/AttackServiceTests/AttackTests.cs`
- Create: `Scenes/Services/SceneActionDescriptionBuilder.cs`
- Modify: `Scenes/Services/IScenesService.cs:8-20`
- Modify: `Scenes/Services/ScenesService.cs:135-182`
- Create: `UnitTests/Scenes/Services/SceneActionDescriptionBuilderTests.cs`

- [ ] **Step 1: Write the failing scene description and basic attack separation tests**

```csharp
using FluentAssertions;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.UnitTests.Core;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Services;

public class SceneActionDescriptionBuilderTests
{
    [Fact]
    public void SpecialAttackDescription_ShouldNotMentionWeaponOrDamage()
    {
        var template = LandOfHeroesTemplate.Template;
        var attacker = new BaseCreature(template, "Mage").Creature;
        var target = new BaseCreature(template, "Guard").Creature;

        var result = new SpecialAttackResult
        {
            Attacker = attacker,
            Target = target,
            SpecialSkillId = LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Bluff],
            DefenseId = LandOfHeroesTemplate.DefenseIds[LandOfHeroesDefense.Evasion],
            Success = true
        };

        var description = SceneActionDescriptionBuilder.BuildSpecialAttackDescription(result);

        description.Should().Contain("Mage");
        description.Should().Contain("Guard");
        description.Should().NotContain("damage");
        description.Should().NotContain("with ");
    }
}
```

Update the first basic attack test to compile against explicit names:

```csharp
var input = new BasicAttackCommand
{
    WeaponSlot = EquipableSlot.MainHand,
    ItemConfiguration = campaignTemplate.ItemConfiguration,
    DefenseId = defensePropertyId,
    VitalityId = new Property(LandOfHeroesTemplate.VitalityIds[LandOfHeroesVitality.Moral], PropertyType.Vitality),
    Luck = 0,
    Advantage = 0
};

var result = attacker.BasicAttack(defender, input, dice);
```

- [ ] **Step 2: Run the updated tests to verify they fail**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter "FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Scenes.Services.SceneActionDescriptionBuilderTests|FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests.AttackTests"
```

Expected:

- build/test fails because `BasicAttackCommand`, `BasicAttackResult`, `Creature.BasicAttack`, and `SceneActionDescriptionBuilder` do not exist yet

- [ ] **Step 3: Write the minimal explicit basic attack and scene-history implementation**

```csharp
// BasicAttackService.cs
public interface IBasicAttackService
{
    Task<BasicAttackResponse> Attack(Guid campaignId, Guid sceneId, Guid attackerId, BasicAttackInput input);
}

public class BasicAttackCommand
{
    public EquipableSlot WeaponSlot { get; set; }
    public ItemConfiguration ItemConfiguration { get; set; } = null!;
    public List<BasicAttackVitalityRule> BasicAttackVitalityRules { get; set; } = [];
    public Guid? DefenseId { get; set; }
    public Property? VitalityId { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public List<Guid> CombatManeuverIds { get; set; } = [];
    public Property? BlockProperty { get; set; }
}

public class BasicAttackResult
{
    public Creature Attacker { get; set; } = null!;
    public Creature Target { get; set; } = null!;
    public ItemInstance Weapon { get; set; } = null!;
    public bool Success { get; set; }
    public int TotalDamage { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Difficulty { get; set; }
}

// CreatureBasicAttack.cs
public partial class Creature
{
    public BasicAttackResult BasicAttack(Creature target, BasicAttackCommand input, IDiceRoller diceRoller, ITestOutputHelper? testOutputHelper = null)
    {
        var weapon = GetWeaponOrDefault(input.WeaponSlot);
        var weaponTemplate = (WeaponTemplate?)weapon.Template;
        var weaponCategory = weaponTemplate?.Category ?? WeaponCategory.Light;
        var gripStats = GripTypeDefinition.Stats[Equipment.GripType];
        var weaponLevelBonus = weapon.LevelBonus;
        var hitValue = GetHitValue(input, weaponCategory);
        var defenseValue = GetDefenseValue(target, input.GetDefenseId1);
        var diceCount = Math.Max(0, hitValue.Total);
        var levelDifferenceBonus = GetLevelDifferenceBonusAgainst(target);
        var hitBonus = hitValue.Total + gripStats.Hit +
                       GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null) + levelDifferenceBonus +
                       weaponLevelBonus;

        var armorCategory = target.Equipment.ArmorCategory;
        var luck = input.Luck - target.GetEvasionLuck() + ResolveWeaponVsArmorLuck(weapon, armorCategory);
        var advantage = Math.Max(0, Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Hit, BonusType.Advantage, null)));

        var roll = new Roll();
        roll.Process(new RollDiceCommand(diceCount, advantage, hitBonus, gripStats.AttackDifficult, defenseValue, [], luck), diceRoller, 20);

        var rolledValues = JsonSerializer.Deserialize<List<int>>(roll.RolledDices) ?? [];
        var successes = rolledValues
            .Select(total => total - defenseValue)
            .Where(over => over >= 0)
            .OrderByDescending(over => over)
            .ToList();

        var difficulty = gripStats.AttackDifficult;
        var tier = 1 + Math.Max(Level - 1, 0) / 2;
        var damageBonusPerHit = gripStats.BaseBonusDamage * tier + gripStats.Damage;

        var result = ResolveSuccessfulAttack(target, weapon, difficulty, damageBonusPerHit, successes, armorCategory, input);
        result.Difficulty = difficulty;
        result.NumberOfRollSuccesses = roll.NumberOfRollSuccesses;
        return result;
    }
}

// CreatureDefend.cs
public BasicAttackResult Evade(Creature attacker, BasicAttackCommand input, IDiceRoller diceRoller)
{
    var weapon = attacker.Equipment.GetItem(input.WeaponSlot) ?? new ItemInstance
    {
        Template = new WeaponTemplate
        {
            Category = WeaponCategory.Medium,
            DamageType = WeaponDamageType.Bludgeoning
        }
    };

    var weaponCategory = ((WeaponTemplate)weapon.Template).Category;
    var gripStats = GripTypeDefinition.Stats[attacker.Equipment.GripType];
    var hitProperty = input.ItemConfiguration.GetWeaponHitProperty(weaponCategory);
    var hitValue = attacker.GetPropertyValue(new PropertyInput(hitProperty));
    var totalHitBonus = hitValue.Total +
                        attacker.GetTotalBonus(BonusApplication.Hit, BonusType.Buff, null) +
                        attacker.GetLevelDifferenceBonusAgainst(this);
    var attackSuccesses = hitValue.Total;
    var evadeComplexity = 10 + totalHitBonus;

    var defenseProperty = new Property(input.GetDefenseId1);
    var defenseValue = GetPropertyValue(new PropertyInput(defenseProperty));
    var chestArmor = Equipment.Chest;
    var armorCategory = chestArmor?.ArmorTemplate?.Category ?? ArmorCategory.None;
    var armorDefenseBonus = chestArmor?.GetDefenseBonus1() ?? ArmorDefinition.DefenseBonus1(armorCategory);
    var armorBonus = chestArmor?.GetBonus ?? 0;
    var evadeRoll = new Roll();
    evadeRoll.Process(new RollDiceCommand(
        defenseValue.Total,
        Math.Max(input.Advantage, GetTotalBonus(BonusApplication.Evasion, BonusType.Advantage, null)),
        defenseValue.Total + armorBonus + armorDefenseBonus,
        evadeComplexity,
        evadeComplexity,
        [],
        input.Luck), diceRoller, 20);

    var remainingSuccesses = Math.Max(0, attackSuccesses - evadeRoll.NumberOfSuccesses);
    var numberOfHits = remainingSuccesses / gripStats.AttackDifficult;
    var damageProperty = attacker.GetPropertyValue(new PropertyInput(
        input.ItemConfiguration.GetWeaponDamageProperty(weaponCategory)));
    var damages = new List<DamageRollResult>();

    for (var i = 0; i < numberOfHits; i++)
    {
        var blockProperty = input.ItemConfiguration.BlockProperty;
        var blockValue = attacker.GetPropertyValue(new PropertyInput(blockProperty, input.BlockProperty));
        var damage = attacker.RollDamage(weapon, damageProperty, gripStats, diceRoller);
        damage.ReducedDamage -= GetBasicBlock(blockValue);
        damage.ReducedDamage = Math.Max(1, damage.ReducedDamage);
        damages.Add(damage);
        ApplyBasicAttackDamage(this, damage.TotalDamage, input.VitalityId);
    }

    return new BasicAttackResult
    {
        Attacker = attacker,
        Target = this,
        Weapon = weapon,
        TotalDamage = damages.Sum(d => d.ReducedDamage),
        Success = numberOfHits <= 0
    };
}

// SceneActionDescriptionBuilder.cs
public static class SceneActionDescriptionBuilder
{
    public static string BuildBasicAttackDescription(BasicAttackResult attackResult, string statusDescription)
    {
        var baseDescription =
            $"{attackResult.Attacker.Name} attacked {attackResult.Target.Name} with {attackResult.Weapon.Name} and caused {attackResult.TotalDamage} damage";

        return string.IsNullOrWhiteSpace(statusDescription)
            ? baseDescription
            : $"{baseDescription} | {statusDescription}";
    }

    public static string BuildSpecialAttackDescription(SpecialAttackResult attackResult)
    {
        var defenseName = attackResult.Target.Defenses
            .First(defense => defense.DefenseTemplateId == attackResult.DefenseId)
            .Name;

        var outcome = attackResult.Success ? "succeeded" : "failed";
        return $"{attackResult.Attacker.Name} used a special attack against {attackResult.Target.Name} ({defenseName}) and {outcome}";
    }
}

// ScenesService.cs
public async Task ProcessBasicAttackAction(Guid sceneId, BasicAttackResult attackResult)
{
    var statusDescription = BuildStatusDescription(attackResult);
    var description = SceneActionDescriptionBuilder.BuildBasicAttackDescription(attackResult, statusDescription);
    await PersistSceneAction(sceneId, attackResult.Attacker.Id, description);
}

public async Task ProcessSpecialAttackAction(Guid sceneId, SpecialAttackResult attackResult)
{
    var description = SceneActionDescriptionBuilder.BuildSpecialAttackDescription(attackResult);
    await PersistSceneAction(sceneId, attackResult.Attacker.Id, description);
}

private async Task PersistSceneAction(Guid sceneId, Guid actorId, string description)
{
    var result = new SceneAction
    {
        Description = description,
        Id = Guid.NewGuid(),
        ActorId = actorId,
        SceneId = sceneId
    };

    var history = await _campaignSceneHistoryBuilderService.BuildHistory(result);
    _roleRollsDbContext.SceneActions.Add(result);
    await _roleRollsDbContext.SaveChangesAsync();
    await _sceneNotificationService.NotifyScene(sceneId, history);
}
```

- [ ] **Step 4: Run the scene and basic attack tests again**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter "FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Scenes.Services.SceneActionDescriptionBuilderTests|FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests.AttackTests|FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests.EvadeTests"
```

Expected:

- `Passed!`
- `Failed: 0`

## Task 4: Update The Two RoleRolls Docs And Run Final Verification

**Files:**
- Modify: `docs/rolerolls-regras-de-negocio.md:213-372`
- Modify: `docs/rolerolls-mapa-tecnico.md:260-431`
- Read-only verification: `docs/superpowers/specs/2026-07-02-basic-vs-special-attack-design.md`

- [ ] **Step 1: Update the business-rules doc with explicit basic vs special sections**

Insert this structure into `docs/rolerolls-regras-de-negocio.md` around the combat section:

```md
### Ataque Básico

- usa arma
- usa configuração de categoria de arma da campanha
- pode aplicar dano automaticamente
- pode aplicar vitalidade automaticamente

### Ataque Especial

- não usa arma
- não usa configuração de categoria de arma
- sempre resolve `MinorSkill x Defense`
- nunca aplica dano automaticamente
- nunca aplica vitalidade automaticamente
- nunca aplica efeito automaticamente

### Diferença Estrutural

O ataque básico é um fluxo de combate armado.

O ataque especial é um teste ofensivo sem arma, resolvido apenas para informar o mestre.
```

- [ ] **Step 2: Update the technical-map doc with explicit file ownership**

Insert this structure into `docs/rolerolls-mapa-tecnico.md` around the combat and mapping sections:

```md
### Ataque Básico

- `Attacks/Services/BasicAttackService.cs`
- `Creatures/Entities/CreatureBasicAttack.cs`
- `Creatures/Entities/CreatureDefend.cs`
- `Itens/Configurations/ItemConfiguration.cs`

### Ataque Especial

- `Attacks/Services/SpecialAttackService.cs`
- `Creatures/Entities/CreatureSpecialAttack.cs`
- `Scenes/Services/SceneActionDescriptionBuilder.cs`

### Diferença estrutural

`basic attack` usa arma e configuração de campanha.

`special attack` não usa arma e resolve apenas `MinorSkill x Defense`.
```

- [ ] **Step 3: Run the full focused verification suite**

Run:

```powershell
dotnet test RoleRollsPocketEdition.csproj --filter "FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Attacks.Services.AttackServiceTests|FullyQualifiedName~RoleRollsPocketEdition.UnitTests.Scenes"
```

Expected:

- `Passed!`
- `Failed: 0`
- no `SpecialAttack` test mentions weapon damage

- [ ] **Step 4: Verify the docs contain the required separation language**

Run:

```powershell
Select-String -Path 'docs/rolerolls-regras-de-negocio.md','docs/rolerolls-mapa-tecnico.md' -Pattern 'Ataque Básico|Ataque Especial|Diferença Estrutural'
```

Expected:

- both docs contain all three markers

## Spec Coverage Check

- `basic attack` weapon/config dependence: covered by Task 3 `BasicAttackCommand`, `BasicAttackService`, `Creature.BasicAttack`, and existing `AttackTests` migration.
- `special attack` as `MinorSkill x Defense`: covered by Task 2 `SpecialAttackCommand`, `Creature.SpecialAttack`, and `SpecialAttackTests`.
- `special attack` with no weapon, no damage, no vitality: covered by Task 2 vitality test plus Task 3 description test.
- explicit endpoints and legacy alias behavior: covered by Task 1 route reflection tests.
- explicit scene/history separation: covered by Task 3 `SceneActionDescriptionBuilder` and `ScenesService` split methods.
- docs separation: covered by Task 4.

## Placeholder Scan

- No `TODO`
- No `TBD`
- No “similar to previous task”
- No commit steps, per explicit user instruction

## Type Consistency Check

- Public request types: `BasicAttackInput`, `SpecialAttackInput`
- Public response types: `BasicAttackResponse`, `SpecialAttackResponse`
- Domain commands: `BasicAttackCommand`, `SpecialAttackCommand`
- Domain results: `BasicAttackResult`, `SpecialAttackResult`
- Domain methods: `Creature.BasicAttack(...)`, `Creature.SpecialAttack(...)`
- Scene hooks: `ProcessBasicAttackAction(...)`, `ProcessSpecialAttackAction(...)`

Plan complete and saved to `docs/superpowers/plans/2026-07-02-basic-vs-special-attack.md`. Two execution options:

**1. Subagent-Driven (recommended)** - I dispatch a fresh subagent per task, review between tasks, fast iteration

**2. Inline Execution** - Execute tasks in this session using executing-plans, batch execution with checkpoints

**Which approach?**
