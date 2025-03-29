using RoleRollsPocketEdition.Campaigns.Entities;
using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Rolls.Entities;
using RoleRollsPocketEdition.Scenes.Models;

namespace RoleRollsPocketEdition.Scenes.Entities
{
    public class Scene : Entity
    {
        public Scene()
        {
        }

        public Scene(Guid campaignId, SceneModel sceneModel)
        {
            CampaignId = campaignId;
            this.Id = sceneModel.Id;
            this.Name = sceneModel.Name;
        }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public ICollection<SceneAction> Actions { get; set; } = new List<SceneAction>();
        public ICollection<SceneCreature> Creatures { get; set; } = new List<SceneCreature>();
        public ICollection<Roll> Rolls { get; set; } = new List<Roll>();

        public string Name { get; set; }
        public SceneStatus Status { get; set; }

        public void Update(SceneModel sceneModel)
        {
            Name = sceneModel.Name;
        }
    }

    public enum SceneStatus
    {
        Active = 0,
        Hidden = 1,
        Finalized = 2,
    }
}
