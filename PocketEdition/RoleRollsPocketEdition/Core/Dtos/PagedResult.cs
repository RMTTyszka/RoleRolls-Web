namespace RoleRollsPocketEdition.Core.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Content { get; set; } = new List<T>();
        public int TotalElements { get; set; }

        public PagedResult()
        {
            
        }

        public PagedResult(int totalElements, List<T> content)
        {
            TotalElements = totalElements;
            Content = content;
        }
    }
}
