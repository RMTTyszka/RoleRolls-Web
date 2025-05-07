using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Entities;

namespace RoleRollsPocketEdition.Rolls
{
    public class RollInput
    {
        public Guid PropertyId { get; set; }
        public RollPropertyType PropertyType { get; set; }

        public int Complexity { get; set; }
        public int Difficulty { get; set; }
        public int Advantage { get; set; }
        public int Bonus { get; set; }
        public bool Hidden { get; set; }

        public string Description { get; set; }
        public List<int> Rolls { get; set; } = new List<int>();
        public Property Property { get; set; }
    }
}