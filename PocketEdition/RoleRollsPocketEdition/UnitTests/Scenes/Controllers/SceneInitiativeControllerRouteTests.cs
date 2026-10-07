using System.Reflection;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RoleRollsPocketEdition.Scenes.Controllers;
using RoleRollsPocketEdition.Scenes.Models;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Scenes.Controllers;

public class SceneInitiativeControllerRouteTests
{
    [Fact]
    public void RoutesExposeCombatInitiativeOperations()
    {
        GetRoute(typeof(HttpGetAttribute), "GetAsync", null);
        GetRoute(typeof(HttpPostAttribute), "RollAllAsync", "roll-all");
        GetRoute(typeof(HttpPostAttribute), "RollCreatureAsync", "{creatureId}/roll");
        GetRoute(typeof(HttpDeleteAttribute), "RemoveCreatureAsync", "{creatureId}");
    }

    [Fact]
    public void RollEndpointsReturnInitiativeData()
    {
        typeof(SceneInitiativeController).GetMethod("RollAllAsync")!.ReturnType.GenericTypeArguments
            .Should().Contain(typeof(ActionResult<List<SceneInitiativeEntryModel>>));
        typeof(SceneInitiativeController).GetMethod("RollCreatureAsync")!.ReturnType.GenericTypeArguments
            .Should().Contain(typeof(ActionResult<SceneInitiativeEntryModel>));
    }

    private static void GetRoute(Type attributeType, string methodName, string? expectedTemplate)
    {
        var method = typeof(SceneInitiativeController).GetMethod(methodName);
        method.Should().NotBeNull();
        var attribute = method!.GetCustomAttributes(attributeType).Single();
        (attribute as IRouteTemplateProvider)!.Template.Should().Be(expectedTemplate);
    }
}
