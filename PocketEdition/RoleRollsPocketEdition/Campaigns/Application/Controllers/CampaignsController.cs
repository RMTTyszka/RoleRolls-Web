using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication;
using RoleRollsPocketEdition.Campaigns.Application.Services;
using RoleRollsPocketEdition.Campaigns.Domain.Models;
using RoleRollsPocketEdition.Creatures.Application.Dtos;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Services;
using RoleRollsPocketEdition.Global.Dtos;
using RoleRollsPocketEdition.Rolls.Application;

namespace RoleRollsPocketEdition.Campaigns.Application.Controllers
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
        [HttpPost("{campaignId}/attributes")]
        public async Task<IActionResult> AddAttribute([FromRoute] Guid campaignId, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.AddAttribute(campaignId, attribute);
            return Ok();
        }       
        [HttpDelete("{campaignId}/attributes/{attributeId}")]
        public async Task<IActionResult> RemoveAttribute([FromRoute] Guid campaignId, [FromRoute] Guid attributeId)
        {
            await _campaignsService.RemoveAttribute(campaignId, attributeId);
            return Ok();
        }
        [HttpPut("{campaignId}/attributes/{attributeId}")]
        public async Task<IActionResult> UpdateAttribute([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromBody] AttributeTemplateModel attribute)
        {
            await _campaignsService.UpdateAttribute(campaignId, attributeId, attribute);
            return Ok();
        }         
        [HttpPost("{campaignId}/attributes/{attributeId}/skills")]
        public async Task<IActionResult> AddSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.AddSkill(campaignId, attributeId, skill);
            return Ok();
        }       
        [HttpDelete("{campaignId}/attributes/{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> RemoveSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId)
        {
            await _campaignsService.RemoveSkill(campaignId, attributeId, skillId);
            return Ok();
        }
        [HttpPut("{campaignId}/attributes/{attributeId}/skills/{skillId}")]
        public async Task<IActionResult> UpdateSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] SkillTemplateModel skill)
        {
            await _campaignsService.UpdateSkill(campaignId, attributeId, skillId, skill);
            return Ok();
        }       
        [HttpPost("{campaignId}/attributes/{attributeId}/skills/{skillId}/minor-skills")]
        public async Task<IActionResult> AddMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromBody] MinorSkillTemplateModel minorSkill)
        {
            await _campaignsService.AddMinorSkillAsync(campaignId, attributeId, skillId, minorSkill);
            return Ok();
        }       
        [HttpDelete("{campaignId}/attributes/{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> RemoveMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId)
        {
            await _campaignsService.RemoveMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId);
            return Ok();
        }
        [HttpPut("{campaignId}/attributes/{attributeId}/skills/{skillId}/minor-skills/{minorSkillId}")]
        public async Task<IActionResult> UpdateMinorSkill([FromRoute] Guid campaignId, [FromRoute] Guid attributeId, [FromRoute] Guid skillId, [FromRoute] Guid minorSkillId, [FromBody] MinorSkillTemplateModel minorSkill)
        {
            await _campaignsService.UpdateMinorSkillAsync(campaignId, attributeId, skillId, minorSkillId, minorSkill);
            return Ok();
        }

        [HttpPost("{campaignId}/lifes")]
        public async Task<IActionResult> AddLife([FromRoute] Guid campaignId, [FromBody] LifeTemplateModel life)
        {
            await _campaignsService.AddLife(campaignId, life);
            return Ok();
        }
        [HttpDelete("{campaignId}/lifes/{lifeId}")]
        public async Task<IActionResult> RemoveLife([FromRoute] Guid campaignId, [FromRoute] Guid lifeId)
        {
            await _campaignsService.RemoveLife(campaignId, lifeId);
            return Ok();
        }
        [HttpPut("{campaignId}/lifes/{lifeId}")]
        public async Task<IActionResult> UpdateLife([FromRoute] Guid campaignId, [FromRoute] Guid lifeId, [FromBody] LifeTemplateModel life)
        {
            await _campaignsService.UpdateLife(campaignId, lifeId, life);
            return Ok();
        }
        [HttpGet("{id}/scenes/{sceneId}/rolls")]
        public async Task<IActionResult> GetRolls([FromRoute] Guid sceneId, [FromRoute] Guid id, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(id, sceneId, input);
            return Ok(result);
        }      
        [HttpGet("{campaignId}/scenes/{sceneId}/rolls/{rollId}")]
        public async Task<IActionResult> GetRoll([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid rollId, [FromQuery] PagedRequestInput input)
        {
            var result = await _rollService.GetAsync(campaignId, sceneId, rollId);
            if (result is null) 
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost("{campaignId}/scenes/{sceneId}/rolls")]
        public async Task<IActionResult> RollDice([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, id = result.Id, sceneId = sceneId }, result);
        }    
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/rolls")]
        public async Task<IActionResult> RollDiceForCreature([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] RollInput input)
        {
            var result = await _rollService.RollAsync(campaignId, sceneId, creatureId, input);
            Response.Headers.AccessControlAllowHeaders = "Location";
            Response.Headers.AccessControlExposeHeaders = "Location";
            return CreatedAtAction(nameof(GetRoll), new { campaignId = campaignId, rollId = result.Id , sceneId  = sceneId }, result);
        }       
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/damage")]
        public async Task<IActionResult> TakeDamage([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureService.TakeDamage(campaignId, sceneId, creatureId, input);
            return Ok();
        }      
        [HttpPost("{campaignId}/scenes/{sceneId}/creatures/{creatureId}/heal")]
        public async Task<IActionResult> Heal([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromRoute] Guid creatureId, [FromBody] UpdateLifeInput input)
        {
            await _creatureService.Heal(campaignId, sceneId, creatureId, input);
            return Ok();
        }

    }
}
