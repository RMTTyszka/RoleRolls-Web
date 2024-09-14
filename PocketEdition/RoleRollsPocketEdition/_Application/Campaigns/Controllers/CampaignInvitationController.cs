using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition._Application.Campaigns.Dtos;
using RoleRollsPocketEdition.Authentication.Application.Services;

namespace RoleRollsPocketEdition._Application.Campaigns.Controllers
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
