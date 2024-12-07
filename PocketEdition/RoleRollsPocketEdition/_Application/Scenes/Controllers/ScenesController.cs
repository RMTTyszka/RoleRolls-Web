using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Application.Scenes.Services;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Models;

namespace RoleRollsPocketEdition._Application.Scenes.Controllers
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
        [HttpDelete("{sceneId}")]

        public async Task UpdateAsync([FromRoute] Guid campaignId, [FromRoute] Guid sceneId)
        {
            await _scenesService.DeleteAsync(campaignId, sceneId);
        }      
           
    }
}
