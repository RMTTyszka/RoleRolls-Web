using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication.Application.Services;
using RoleRollsPocketEdition.Campaigns.Application.Dtos;
using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Entities;
using RoleRollsPocketEdition.Campaigns.Domain.Models;

namespace RoleRollsPocketEdition.Campaigns.Application.Controllers
{
    [Route("campaigns/{campaignId}/players")]
    public class CampaignPlayersController : ControllerBase
    {
        private readonly ICampaignsService _campaignsService;
        private readonly ICurrentUser _currentUser;

        public CampaignPlayersController(ICampaignsService campaignsService, ICurrentUser currentUser)
        {
            _campaignsService = campaignsService;
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<List<CampaignPlayerModel>> GetList([FromRoute] Guid campaignId)
        {
            var players = await _campaignsService.GetPlayersAsync(campaignId);
            return players;
        }      
        [HttpPost]
        public async Task<Guid> InviteAsync([FromRoute] Guid campaignId)
        {
            var invitationCode = await _campaignsService.Invite(campaignId);
            return invitationCode;
        }       
    }
}
