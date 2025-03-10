﻿using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Dtos
{
    public class GetAllCampaignCreaturesInput
    {
        public CreatureCategory? CreatureType { get; set; }
        public List<Guid> CreatureIds { get; set; }
        public Guid? OwnerId { get; set; }

        public GetAllCampaignCreaturesInput()
        {
            CreatureIds = new List<Guid>();
        }
    }
}
