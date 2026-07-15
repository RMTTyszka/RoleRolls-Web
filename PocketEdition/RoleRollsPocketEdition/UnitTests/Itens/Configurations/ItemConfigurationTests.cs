using FluentAssertions;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.DefaultUniverses.LandOfHeroes.CampaignTemplates;
using RoleRollsPocketEdition.Itens.Configurations;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Itens.Configurations;

public class ItemConfigurationTests
{
    [Fact(DisplayName = "Land of Heroes configura a propriedade de Evasão")]
    public void LandOfHeroesConfiguresEvadeProperty()
    {
        var evadeProperty = LandOfHeroesTemplate.Template.ItemConfiguration.EvadeProperty;

        evadeProperty.Should().NotBeNull();
        evadeProperty!.Id.Should().Be(LandOfHeroesTemplate.MinorSkillIds[LandOfHeroesMinorSkill.Evasion]);
        evadeProperty.Type.Should().Be(PropertyType.MinorSkill);
    }

    [Fact]
    public void ItemConfigurationModelCopiesAndUpdatesEvadeProperty()
    {
        var originalProperty = new Property(Guid.NewGuid(), PropertyType.MinorSkill);
        var updatedProperty = new Property(Guid.NewGuid(), PropertyType.MinorSkill);
        var configuration = new ItemConfiguration(LandOfHeroesTemplate.Template, new ItemConfigurationModel
        {
            EvadeProperty = originalProperty
        });

        var copiedModel = ItemConfigurationModel.FromConfiguration(configuration);
        configuration.Update(new ItemConfigurationModel { EvadeProperty = updatedProperty });

        copiedModel.EvadeProperty.Should().BeSameAs(originalProperty);
        configuration.EvadeProperty.Should().BeSameAs(updatedProperty);
    }
}
