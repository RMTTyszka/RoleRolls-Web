using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Controllers;

[Route("creature-templates/{templadId}/defenses")]
public class DefenseTemplateController : ControllerBase
{
    [HttpPost]
    public void Create([FromBody] DefenseTemplateModel defenseModel)
    {
        
    }
}