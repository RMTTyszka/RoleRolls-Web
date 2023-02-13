﻿using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.Creatures.Application.Dtos
{
    public class GetAllCampaignCreaturesInput
    {
        public CreatureType? CreatureType { get; set; }
        public List<Guid> CreatureIds { get; set; }
        public Guid? OwnerId { get; set; }
    }
}
