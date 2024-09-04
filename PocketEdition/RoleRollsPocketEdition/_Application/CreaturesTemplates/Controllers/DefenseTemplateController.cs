using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.Application.CreaturesTemplates.Controllers;

[Route("creature-templates/{templadId}/defenses")]
public class DefenseTemplateController : ControllerBase
{
    [HttpPost]
    public void Create([FromBody] DefenseTemplateModel defenseModel)
    {
        
    }
}