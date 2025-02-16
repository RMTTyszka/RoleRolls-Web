using System.ComponentModel.DataAnnotations;

namespace RoleRollsPocketEdition.Core.Dtos
{
    public class PagedRequestInput
    {
        [Required(AllowEmptyStrings = true)]
        public string Filter { get; set; } = "";
        public int SkipCount { get; set; }
        public int MaxResultCount { get; set; }
    }
}
