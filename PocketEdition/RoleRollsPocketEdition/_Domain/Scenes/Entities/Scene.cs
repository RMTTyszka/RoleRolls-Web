using RoleRollsPocketEdition._Domain.Rolls.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Entities;
using RoleRollsPocketEdition._Domain.Scenes.Models;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Campaigns.Entities
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

        public void Update(SceneModel sceneModel)
        {
            Name = sceneModel.Name;
        }
    }
}
