namespace RoleRollsPocketEdition.Scenes.Domain.Models
{
    public class SceneModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public SceneModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
