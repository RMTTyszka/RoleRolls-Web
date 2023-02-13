using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Scenes.Application.Services;
using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Scenes.Application.Controllers
{
    [Route("campaigns/{campaignId}/scenes")]
    public class ScenesController : ControllerBase
    {
        private readonly IScenesService _scenesService;

        public ScenesController(IScenesService scenesService)
        {
            _scenesService = scenesService;
        }
        [HttpGet]

        public async Task<List<SceneModel>> GetListAsync([FromRoute] Guid campaignId)
        {
            var scenes = await _scenesService.GetAllAsync(campaignId);
            return scenes;
        }   
        [HttpGet("{sceneId}")]

        public async Task<SceneModel> GetAsync([FromRoute] Guid campaignId, [FromRoute] Guid sceneId)
        {
            var scene = await _scenesService.GetAsync(sceneId);
            return scene;
        }    
        [HttpPost("")]

        public async Task CreateAsync([FromRoute] Guid campaignId, [FromBody] SceneModel sceneModel)
        {
            await _scenesService.Create(campaignId, sceneModel);
        }      
        [HttpPut("{sceneId}")]

        public async Task UpdateAsync([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] SceneModel sceneModel)
        {
            await _scenesService.Update(campaignId, sceneId, sceneModel);
        }      
        [HttpPost("{sceneId}/heroes")]

        public async Task AddHeroes([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] List<SceneCreatureModel> creatureModels)
        {
            await _scenesService.AddHero(campaignId, sceneId, creatureModels);
        }       
        [HttpPost("{sceneId}/monsters")]

        public async Task AddMonster([FromRoute] Guid campaignId, [FromRoute] Guid sceneId, [FromBody] List<SceneCreatureModel> creatureModels)
        {
            await _scenesService.AddMonster(campaignId, sceneId, creatureModels);
        }
    }
}
