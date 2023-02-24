namespace RoleRollsPocketEdition.Scenes.Domain.Models
{
    public class SceneModel
    {
        public Guid Id { get; set; }
        public Guid CampaignId { get; set; }
        public string Name { get; set; }

        public SceneModel(Guid id, string name, Guid campaignId)
        {
            Id = id;
            Name = name;
            CampaignId = campaignId;
        }
    }
}
