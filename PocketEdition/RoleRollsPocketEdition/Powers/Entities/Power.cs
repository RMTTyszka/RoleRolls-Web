using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Powers.Entities
{
    public class Power : Entity
    {
        public PowerType  Type { get; set; }
        public PowerActionType  ActionType { get; set; }
        public string Name { get; set; }
        public string UseAttribute { get; set; }
        public string TargetDefense { get; set; }
    }
}