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
    public void AttackRoutesShouldExposeExpectedTemplatesAndContracts()
    {
        AssertRoute("BasicAttack", "{creatureId}/basic-attacks", typeof(BasicAttackInput), typeof(BasicAttackResponse));
        AssertRoute("SpecialAttack", "{creatureId}/special-attacks", typeof(SpecialAttackInput), typeof(SpecialAttackResponse));
        AssertRoute("Attack", "{creatureId}/attacks", typeof(BasicAttackInput), typeof(BasicAttackResponse));
    }

    [Fact]
    public void EvadeRouteShouldBeInitiatedByTheDefender()
    {
        AssertRoute("Evade", "{defenderId}/evades", typeof(EvadeInput), typeof(EvadeResponse));
    }

    private static void AssertRoute(string methodName, string expectedTemplate, Type expectedInputType, Type expectedResponseType)
    {
        var method = typeof(SceneCreaturesController).GetMethod(methodName);

        method.Should().NotBeNull($"{methodName} should exist on {nameof(SceneCreaturesController)}");
        method!.GetCustomAttribute<HttpPostAttribute>()!.Template.Should().Be(expectedTemplate);
        method.GetParameters().Single(parameter => parameter.GetCustomAttribute<FromBodyAttribute>() != null).ParameterType
            .Should()
            .Be(expectedInputType);
        method.ReturnType.GenericTypeArguments.Single().Should().Be(expectedResponseType);
    }
}
