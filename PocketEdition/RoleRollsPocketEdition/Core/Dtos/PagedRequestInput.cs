namespace RoleRollsPocketEdition.Core.Dtos
{
    public class PagedRequestInput
    {
        public string? Filter { get; set; } = "";
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
    }
}


