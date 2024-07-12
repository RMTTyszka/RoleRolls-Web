using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Campaigns.Application.Dtos;
using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Campaigns.Domain.Models;

namespace RoleRollsPocketEdition.Campaigns.Application.Controllers
{
    [Route("campaigns/invitations")]
    public class CampaignInvitationController : ControllerBase
    {
        private readonly ICampaignsService _campaignsService;
        private readonly ICurrentUser _currentUser;

        public CampaignInvitationController(ICampaignsService campaignsService, ICurrentUser currentUser)
        {
            _campaignsService = campaignsService;
            _currentUser = currentUser;
        }

        [HttpPut]
        public async Task<ActionResult> AcceptInvitationAsync([FromBody] AcceptInvitationInput input)
        {
            var result = await _campaignsService.AcceptInvite(_currentUser.User.Id, input.InvitationCode);
            if (result.Result == Dtos.InvitationResult.Ok) {
                return Ok();
            }
            return new UnprocessableEntityObjectResult(result.Result);
        }
    }
}
