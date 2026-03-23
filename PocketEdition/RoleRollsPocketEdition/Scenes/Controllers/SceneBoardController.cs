using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Scenes.Models;
using RoleRollsPocketEdition.Scenes.Services;

namespace RoleRollsPocketEdition.Scenes.Controllers;

[Route("campaigns/{campaignId}/scenes/{sceneId}/board")]
public class SceneBoardController : ControllerBase
{
    private readonly ISceneBoardService _sceneBoardService;

    public SceneBoardController(ISceneBoardService sceneBoardService)
    {
        _sceneBoardService = sceneBoardService;
    }

    [HttpGet]
    public async Task<ActionResult<SceneBoardDocument>> GetAsync([FromRoute] Guid campaignId, [FromRoute] Guid sceneId)
    {
        var board = await _sceneBoardService.GetAsync(campaignId, sceneId);
        return board is null ? NotFound() : Ok(board);
    }

    [HttpPost("strokes")]
    public async Task<ActionResult<BoardOperationEnvelope>> AddStrokeAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromBody] BoardCommand<SceneBoardStroke> command)
    {
        var operation = await _sceneBoardService.AddStrokeAsync(campaignId, sceneId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpDelete("strokes/{strokeId}")]
    public async Task<ActionResult<BoardOperationEnvelope>> RemoveStrokeAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid strokeId,
        [FromBody] BoardDeleteCommand command)
    {
        var operation = await _sceneBoardService.RemoveStrokeAsync(campaignId, sceneId, strokeId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpPut("tokens/{tokenId}")]
    public async Task<ActionResult<BoardOperationEnvelope>> UpsertTokenAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid tokenId,
        [FromBody] BoardCommand<SceneBoardToken> command)
    {
        var operation = await _sceneBoardService.UpsertTokenAsync(campaignId, sceneId, tokenId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpPut("tokens/{tokenId}/position")]
    public async Task<ActionResult<BoardOperationEnvelope>> MoveTokenAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid tokenId,
        [FromBody] BoardCommand<BoardTokenMoveInput> command)
    {
        var operation = await _sceneBoardService.MoveTokenAsync(campaignId, sceneId, tokenId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpPut("tokens/{tokenId}/label")]
    public async Task<ActionResult<BoardOperationEnvelope>> RenameTokenAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid tokenId,
        [FromBody] BoardCommand<BoardTokenRenameInput> command)
    {
        var operation = await _sceneBoardService.RenameTokenAsync(campaignId, sceneId, tokenId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpPut("tokens/{tokenId}/lock")]
    public async Task<ActionResult<BoardOperationEnvelope>> SetTokenLockedAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid tokenId,
        [FromBody] BoardCommand<BoardTokenLockInput> command)
    {
        var operation = await _sceneBoardService.SetTokenLockedAsync(campaignId, sceneId, tokenId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpDelete("tokens/{tokenId}")]
    public async Task<ActionResult<BoardOperationEnvelope>> RemoveTokenAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromRoute] Guid tokenId,
        [FromBody] BoardDeleteCommand command)
    {
        var operation = await _sceneBoardService.RemoveTokenAsync(campaignId, sceneId, tokenId, command);
        return operation is null ? NotFound() : Ok(operation);
    }

    [HttpPost("clear")]
    public async Task<ActionResult<BoardOperationEnvelope>> ClearAsync(
        [FromRoute] Guid campaignId,
        [FromRoute] Guid sceneId,
        [FromBody] BoardDeleteCommand command)
    {
        var operation = await _sceneBoardService.ClearAsync(campaignId, sceneId, command);
        return operation is null ? NotFound() : Ok(operation);
    }
}
