using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.CreatureTypes.Services;
using RoleRollsPocketEdition.CreatureTypes.Validations;
using RoleRollsPocketEdition.Templates.Dtos;
using RoleRollsPocketEdition.Templates.Services;

namespace RoleRollsPocketEdition.Templates.Controllers
{
    [ApiController]
    [Route("creature-templates/{templateId:guid}/creature-types")]
    public class TemplateCreatureTypesController : ControllerBase
    {

        private readonly ICreatureTypeService _creatureTemplateService;

        public TemplateCreatureTypesController(ICreatureTypeService creatureTemplateService)
        {
            _creatureTemplateService = creatureTemplateService;
        }

        [HttpGet("{id}")]
        public async Task<CreatureTypeModel> GetAsync([FromRoute] Guid templateId, [FromRoute]Guid id) 
        {
            return await _creatureTemplateService.GetAsync(templateId, id);
        }      
        [HttpGet("")]
        public async Task<PagedResult<CreatureTypeModel>> GetListAsync([FromRoute] Guid templateId, [FromQuery] PagedRequestInput input) 
        {
            return await _creatureTemplateService.GetListAsync(templateId, input);
        }
        [HttpPost("")]
        public Task Create([FromRoute] Guid templateId, [FromBody] CreatureTypeModel template) 
        {
            return _creatureTemplateService.CreateAsync(templateId, template);
        }      
        [HttpPut("{id}")]
        public async Task<IActionResult>Update([FromRoute] Guid templateId, [FromRoute] Guid id, [FromBody] CreatureTypeModel template) 
        {
            var result = await _creatureTemplateService.UpdateAsync(templateId, id, template);
            if (result.Validation != CreatureTypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }      
        [HttpDelete("{id}")]
        public async Task<IActionResult>Delete([FromRoute] Guid templateId, [FromRoute] Guid id) 
        {
            var result = await _creatureTemplateService.RemoveAsync(templateId, id);
            if (result.Validation != CreatureTypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }
    }
}
