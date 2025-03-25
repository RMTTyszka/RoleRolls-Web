using RoleRollsPocketEdition.Core.Dtos;
using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Dtos
{
    public class GetAllCampaignCreaturesInput : PagedRequestInput
    {
        public CreatureCategory? CreatureCategory { get; set; }
        public bool? OnlyTemplates { get; set; }

        public GetAllCampaignCreaturesInput()
        {
        }
    }
}
