﻿using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Services;
using RoleRollsPocketEdition.Core.Dtos;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Controllers
{
    [ApiController]
    [Route("creature-templates")]
    public class TemplateController : ControllerBase
    {

        private readonly ICreatureTemplateService _creatureTemplateService;

        public TemplateController(ICreatureTemplateService creatureTemplateService)
        {
            _creatureTemplateService = creatureTemplateService;
        }

        [HttpGet("{id}")]
        public async Task<CampaignTemplateModel> GetAsync(Guid id) 
        {
            return await _creatureTemplateService.Get(id);
        }      
        [HttpGet("defaults")]
        public async Task<List<CampaignTemplateModel>> GetDefault(PagedRequestInput input) 
        {
            return await _creatureTemplateService.GetDefaults(input);
        }
        [HttpPost("")]
        public Task Create([FromBody] CampaignTemplateModel template) 
        {
            return _creatureTemplateService.Create(template);
        }      
        [HttpPut("{id}")]
        public async Task<IActionResult>Update([FromRoute] Guid id, [FromBody] CampaignTemplateModel template) 
        {
            var result = await _creatureTemplateService.UpdateAsync(id, template);
            if (result != CreatureTemplateValidationResult.Ok)
            {
                return new UnprocessableEntityObjectResult(result);
            }
            return Ok();
        }
    }
}