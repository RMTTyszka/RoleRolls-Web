namespace RoleRollsPocketEdition.Core.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }

        public PagedResult()
        {
            
        }

        public PagedResult(int totalCount, List<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }
    }
}


