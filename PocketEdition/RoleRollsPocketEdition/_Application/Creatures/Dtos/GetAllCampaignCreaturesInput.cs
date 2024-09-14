using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Dtos
{
    public class GetAllCampaignCreaturesInput
    {
        public CreatureType? CreatureType { get; set; }
        public List<Guid> CreatureIds { get; set; }
        public Guid? OwnerId { get; set; }

        public GetAllCampaignCreaturesInput()
        {
            CreatureIds = new List<Guid>();
        }
    }
}
