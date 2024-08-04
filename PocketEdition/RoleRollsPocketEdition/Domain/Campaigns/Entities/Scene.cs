using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Scenes.Domain.Models;

namespace RoleRollsPocketEdition.Domain.Campaigns.Entities
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
