using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Creatures.Application.Services;
using RoleRollsPocketEdition.Creatures.Domain;

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

        [HttpGet("{id}")]
        public async Task<CreatureTemplate> GetAsync(Guid id) 
        {
            return await _creatureTemplateService.Get(id);
        }
        [HttpPost("")]
        public Task Create([FromBody] CreatureTemplate template) 
        {
            return _creatureTemplateService.Create(template);
        }      
        [HttpPut("{id}")]
        public Task Update([FromRoute] Guid id, [FromBody] CreatureTemplate template) 
        {
            return _creatureTemplateService.Update(id, template);
        }
    }
}
