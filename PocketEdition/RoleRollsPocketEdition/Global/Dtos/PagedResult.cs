namespace RoleRollsPocketEdition.Global.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Content { get; set; } = new List<T>();
        public int TotalElements { get; set; }
    }
}
