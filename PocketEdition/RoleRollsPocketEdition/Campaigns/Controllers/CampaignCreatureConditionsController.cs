using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Campaigns.ApplicationServices;
using RoleRollsPocketEdition.Core.Authentication;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Controllers;

[Authorize]
[ApiController]
[Route("campaigns/{campaignId}/creature-conditions")]
public class CampaignCreatureConditionsController : ControllerBase
{
    private readonly ICampaignsService _campaignsService;

    public CampaignCreatureConditionsController(ICampaignsService campaignsService)
    {
        _campaignsService = campaignsService;
    }

    [HttpPost("")]
    public async Task<IActionResult> AddCreatureCondition([FromRoute] Guid campaignId,
        [FromBody] CreatureConditionModel creatureCondition)
    {
        await _campaignsService.AddCreatureCondition(campaignId, creatureCondition);
        return Ok();
    }

    [HttpDelete("{creatureConditionId}")]
    public async Task<IActionResult> RemoveCreatureCondition([FromRoute] Guid campaignId,
        [FromRoute] Guid creatureConditionId)
    {
        await _campaignsService.RemoveCreatureCondition(campaignId, creatureConditionId);
        return Ok();
    }

    [HttpPut("{creatureConditionId}")]
    public async Task<IActionResult> UpdateCreatureCondition([FromRoute] Guid campaignId,
        [FromRoute] Guid creatureConditionId,
        [FromBody] CreatureConditionModel creatureCondition)
    {
        await _campaignsService.UpdateCreatureCondition(campaignId, creatureConditionId, creatureCondition);
        return Ok();
    }
}
