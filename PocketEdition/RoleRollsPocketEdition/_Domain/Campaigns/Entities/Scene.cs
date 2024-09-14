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
        public string Name { get; set; }

        public void Update(SceneModel sceneModel)
        {
            Name = sceneModel.Name;
        }
    }
}
