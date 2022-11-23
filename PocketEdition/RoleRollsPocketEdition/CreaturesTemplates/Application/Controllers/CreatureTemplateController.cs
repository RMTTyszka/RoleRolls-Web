﻿using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Authentication;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Controllers
{
    [ApiController]
    [Route("creature-templates")]
    public class CreatureTemplateController : ControllerBase
    {

        private readonly ICreatureTemplateService _creatureTemplateService;

        public CreatureTemplateController(ICreatureTemplateService creatureTemplateService)
        {
            _creatureTemplateService = creatureTemplateService;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<CreatureTemplateModel> GetAsync(Guid id) 
        {
            return await _creatureTemplateService.Get(id);
        }
        [HttpPost("")]
        public Task Create([FromBody] CreatureTemplateModel template) 
        {
            return _creatureTemplateService.Create(template);
        }      
        [HttpPut("{id}")]
        public async Task<IActionResult>Update([FromRoute] Guid id, [FromBody] CreatureTemplateModel template) 
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