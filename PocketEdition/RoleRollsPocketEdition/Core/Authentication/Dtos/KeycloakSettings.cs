namespace RoleRollsPocketEdition.Core.Authentication.Dtos
{
    public class KeycloakSettings
    {
        public bool Enabled { get; set; }
        public string? BaseUrl { get; set; }
        public string? Realm { get; set; }
        public string? Audience { get; set; }
        public bool RequireHttpsMetadata { get; set; } = false;
    }
}
