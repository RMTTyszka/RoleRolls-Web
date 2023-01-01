using RoleRollsPocketEdition.Campaigns.Domain.Entities;

namespace RoleRollsPocketEdition.Campaigns.Application.Services
{
    public class RollInput
    {
        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }

        public int Complexity { get; set; }
        public int Difficulty { get; set; }
        public int PropertyBonus { get; set; }
        public int RollBonus { get; set; }
        public bool Hidden { get; set; }
    }
}