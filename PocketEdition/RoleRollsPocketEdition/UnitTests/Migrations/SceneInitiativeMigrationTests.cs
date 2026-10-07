using System.Reflection;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Migrations;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Migrations;

public class SceneInitiativeMigrationTests
{
    [Fact]
    public void MigrationHasEfDiscoveryMetadata()
    {
        var migrationType = typeof(AddSceneInitiativeAndTemplateProperty);

        migrationType.GetCustomAttribute<DbContextAttribute>()!.ContextType.Should().Be(typeof(RoleRollsDbContext));
        migrationType.GetCustomAttribute<MigrationAttribute>()!.Id
            .Should().Be("20261007120000_AddSceneInitiativeAndTemplateProperty");
    }
}
