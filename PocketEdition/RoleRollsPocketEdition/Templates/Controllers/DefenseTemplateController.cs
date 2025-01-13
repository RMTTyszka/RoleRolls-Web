using Microsoft.AspNetCore.Mvc;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Controllers;

[Route("creature-templates/{templadId}/defenses")]
public class DefenseTemplateController : ControllerBase
{
    [HttpPost]
    public void Create([FromBody] DefenseTemplateModel defenseModel)
    {
        
    }
}