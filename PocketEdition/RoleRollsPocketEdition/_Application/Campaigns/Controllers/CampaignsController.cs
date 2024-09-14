using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Campaigns.ApplicationServices;
using RoleRollsPocketEdition._Application.Creatures.Services;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Services;
using RoleRollsPocketEdition._Application.Rolls.Application;
using RoleRollsPocketEdition._Domain.Campaigns.Models;
using RoleRollsPocketEdition.Authentication;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.Campaigns.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns")]
    public class CampaignsController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;
        private readonly ICreatureTemplateService _creatureTemplateService;
        private readonly ICreatureService _creatureService;
        private readonly IRollService _rollService;

        public CampaignsController(ICampaignsService campaignsService, ICreatureTemplateService creatureTemplateService, IRollService rollService, ICreatureService creatureService)
        {
            _campaignsService = campaignsService;
            _creatureTemplateService = creatureTemplateService;
            _rollService = rollService;
            _creatureService = creatureService;
        }

        [HttpGet("{campaignId}")]
        public async Task<CampaignModel> GetAsync(Guid campaignId)
        {
            return await _campaignsService.GetAsync(campaignId);
        }    
        [HttpGet()]
        public async Task<PagedResult<CampaignModel>> GetListAsync([FromQuery] PagedRequestInput input)
        {
            return await _campaignsService.GetListAsync(input);
        }
        [HttpPost("")]
        public Task Create([FromBody] CampaignModel template)
        {
            return _campaignsService.CreateAsync(template);
        }
        [HttpPut("{campaignId}")]
        public async Task<IActionResult> Update([FromRoute] Guid campaignId, [FromBody] CampaignModel campaignModel)
        {
            if (!campaignModel.CreatureTemplateId.HasValue)
            {
                return new BadRequestObjectResult("Missing creatureTemplateId");
            }
            await _campaignsService.UpdateAsync(campaignModel);
            await _creatureTemplateService.UpdateAsync(campaignModel.CreatureTemplateId.Value, campaignModel.CreatureTemplate);
            return Ok();
        }
        [HttpDelete("{campaignId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid campaignId)
        {
            await _campaignsService.DeleteAsync(campaignId);
            return Ok();
        }
    }
}
