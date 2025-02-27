using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Archetypes.Services;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.Archetypes.Validations;

namespace RoleRollsPocketEdition.Templates.Controllers
{
    [ApiController]
    [Route("campaign-templates/{templateId:guid}/archetypes")]
    public class TemplateArchetypesController : ControllerBase
    {

        private readonly IArchetypeService _creatureTemplateService;

        public TemplateArchetypesController(IArchetypeService creatureTemplateService)
        {
            _creatureTemplateService = creatureTemplateService;
        }

        [HttpGet("{id}")]
        public async Task<ArchetypeModel> GetAsync([FromRoute] Guid templateId, [FromRoute]Guid id) 
        {
            return await _creatureTemplateService.GetAsync(templateId, id);
        }      
        [HttpGet("")]
        public async Task<PagedResult<ArchetypeModel>> GetListAsync([FromRoute] Guid templateId, [FromQuery] PagedRequestInput input) 
        {
            return await _creatureTemplateService.GetListAsync(templateId, input);
        }
        [HttpPost("")]
        public Task Create([FromRoute] Guid templateId, [FromBody] ArchetypeModel template) 
        {
            return _creatureTemplateService.CreateAsync(templateId, template);
        }      
        [HttpPut("{id}")]
        public async Task<IActionResult>Update([FromRoute] Guid templateId, [FromRoute] Guid id, [FromBody] ArchetypeModel template) 
        {
            var result = await _creatureTemplateService.UpdateAsync(templateId, id, template);
            if (result.Validation != ArchetypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }      
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete([FromRoute] Guid templateId, [FromRoute] Guid id) 
        {
            var result = await _creatureTemplateService.RemoveAsync(templateId, id);
            if (result.Validation != ArchetypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }
        [HttpPost("{creatureTypeId}/bonuses")]
        public async Task<IActionResult> AddBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromBody] BonusModel bonus)
        {
            var result = await _creatureTemplateService.AddBonus(templateId, creatureTypeId, bonus);
            if (result.Validation != ArchetypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }

        [HttpPut("{creatureTypeId}/bonuses/{bonusId}")]
        public async Task<IActionResult> UpdateBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromRoute] Guid bonusId, [FromBody] BonusModel bonus)
        {
            var result = await _creatureTemplateService.UpdateBonus(templateId, creatureTypeId, bonus);
            if (result.Validation != ArchetypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }

        [HttpDelete("{creatureTypeId}/bonuses/{bonusId}")]
        public async Task<IActionResult> RemoveBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromRoute] Guid bonusId)
        {
            var result = await _creatureTemplateService.RemoveBonus(templateId, creatureTypeId, bonusId);
            if (result.Validation != ArchetypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }
    }
}
