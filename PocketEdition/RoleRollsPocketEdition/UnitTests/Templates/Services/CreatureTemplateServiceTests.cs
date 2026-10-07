using FluentAssertions;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Services;
using Xunit;

namespace RoleRollsPocketEdition.UnitTests.Templates.Services;

public class CreatureTemplateServiceTests
{
    [Fact]
    public void ValidateInputRejectsInitiativePropertyOutsideTemplate()
    {
        var template = new CampaignTemplateModel
        {
            IniciativePropertyId = Guid.NewGuid(),
            Attributes = [],
            Skills = [],
            Defenses = [],
            Vitalities = []
        };

        var result = CreatureTemplateService.ValidateInput(template);

        result.Should().Be(CreatureTemplateValidationResult.InvalidIniciativeProperty);
    }

    [Fact]
    public void ValidateInputAcceptsInitiativeAttributeFromTemplate()
    {
        var propertyId = Guid.NewGuid();
        var template = new CampaignTemplateModel
        {
            IniciativePropertyId = propertyId,
            Attributes = [new AttributeTemplateModel { Id = propertyId }],
            Skills = [],
            Defenses = [],
            Vitalities = []
        };

        var result = CreatureTemplateService.ValidateInput(template);

        result.Should().Be(CreatureTemplateValidationResult.Ok);
    }
}
