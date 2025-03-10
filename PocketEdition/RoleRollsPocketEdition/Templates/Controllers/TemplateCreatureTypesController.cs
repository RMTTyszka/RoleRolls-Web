﻿using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Bonuses.Models;
using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.CreatureTypes.Services;
using RoleRollsPocketEdition.CreatureTypes.Validations;

namespace RoleRollsPocketEdition.Templates.Controllers
{
    [ApiController]
    [Route("campaign-templates/{templateId:guid}/creature-types")]
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
        [HttpPost("{creatureTypeId}/bonuses")]
        public async Task<IActionResult> AddBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromBody] BonusModel bonus)
        {
            var result = await _creatureTemplateService.AddBonus(templateId, creatureTypeId, bonus);
            if (result.Validation != CreatureTypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }

        [HttpPut("{creatureTypeId}/bonuses/{bonusId}")]
        public async Task<IActionResult> UpdateBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromRoute] Guid bonusId, [FromBody] BonusModel bonus)
        {
            var result = await _creatureTemplateService.UpdateBonus(templateId, creatureTypeId, bonus);
            if (result.Validation != CreatureTypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }

        [HttpDelete("{creatureTypeId}/bonuses/{bonusId}")]
        public async Task<IActionResult> RemoveBonus([FromRoute] Guid templateId, [FromRoute] Guid creatureTypeId, [FromRoute] Guid bonusId)
        {
            var result = await _creatureTemplateService.RemoveBonus(templateId, creatureTypeId, bonusId);
            if (result.Validation != CreatureTypeValidation.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }
    }
}
