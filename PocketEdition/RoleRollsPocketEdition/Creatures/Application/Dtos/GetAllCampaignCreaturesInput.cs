﻿using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Entities;

namespace RoleRollsPocketEdition.Creatures.Application.Dtos
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
