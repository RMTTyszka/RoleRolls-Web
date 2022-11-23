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
        [HttpPost("{id}/attributes")]
        public async Task<IActionResult> AddAttribute([FromRoute] Guid id, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.AddAttribute(id, attribute);
            return Ok();
        }       
        [HttpDelete("{id}/attributes/{attributeId}")]
        public async Task<IActionResult> RemoveAttribute([FromRoute] Guid id, [FromRoute] Guid attributeId)
        {
            await _campaignsService.RemoveAttribute(id, attributeId);
            return Ok();
        }
        [HttpPut("{id}/attributes/{attributeId}")]
        public async Task<IActionResult> UpdateAttribute([FromRoute] Guid id, [FromRoute] Guid attributeId, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.UpdateAttribute(id, attributeId, attribute);
            return Ok();
        }         
        [HttpPost("{id}/attributes/{attributeId}/skills")]
        public async Task<IActionResult> AddSkill([FromRoute] Guid id, [FromRoute] Guid attributeId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.AddSkill(id, attributeId, skill);
            return Ok();
        }       
        [HttpDelete("{id}/attributes/{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveAttribute([FromRoute] Guid id, [FromRoute] Guid attributeId, [FromRoute] Guid skillId)
        {
            await _campaignsService.RemoveSkill(id, attributeId, skillId);
            return Ok();
        }
        [HttpPut("{id}/attributes/{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid id, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.UpdateSkill(id, attributeId, skillId, skill);
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
