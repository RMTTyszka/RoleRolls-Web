using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.CreaturesTemplates.Application.Services;
using RoleRollsPocketEdition.CreaturesTemplates.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Controllers
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
