using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Campaigns.Domain.Services;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.Global.Dtos;

namespace RoleRollsPocketEdition.Campaigns.Application.Controllers
{
        [Authorize]
    [ApiController]
    [Route("campaigns")]
    public class CampaignsController : ControllerBase
    {

        private readonly ICampaignsService _campaignsService;
        private readonly ICreatureTemplateService _creatureTemplateService;

        public CampaignsController(ICampaignsService campaignsService, ICreatureTemplateService creatureTemplateService)
        {
            _campaignsService = campaignsService;
            _creatureTemplateService = creatureTemplateService;
        }

        [HttpGet("{id}")]
        public async Task<CampaignModel> GetAsync(Guid id)
        {
            return await _campaignsService.GetAsync(id);
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
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CampaignModel campaignModel)
        {
            if (!campaignModel.CreatureTemplateId.HasValue)
            {
                return new BadRequestObjectResult("Missing creatureTemplateId");
            }
            await _campaignsService.UpdateAsync(campaignModel);
            await _creatureTemplateService.UpdateAsync(campaignModel.CreatureTemplateId.Value, campaignModel.CreatureTemplate);
            return Ok();
        }      
        [HttpPost("{id}")]
        public async Task<IActionResult> AddAttribute([FromRoute] Guid id, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.AddAttribute(id, attribute);
            return Ok();
        }    
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _campaignsService.DeleteAsync(id);
            return Ok();
        }
    }
}
