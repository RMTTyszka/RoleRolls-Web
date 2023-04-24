using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Campaigns.Domain.Entities
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
        public string Name { get; set; }

        internal void Update(SceneModel sceneModel)
        {
            Name = sceneModel.Name;
        }
    }
}
