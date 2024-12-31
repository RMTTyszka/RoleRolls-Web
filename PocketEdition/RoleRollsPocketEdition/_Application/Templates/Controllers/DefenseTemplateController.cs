using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Controllers;

[Route("creature-templates/{templadId}/defenses")]
public class DefenseTemplateController : ControllerBase
{
    [HttpPost]
    public void Create([FromBody] DefenseTemplateModel defenseModel)
    {
        
    }
}