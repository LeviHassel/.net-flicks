namespace DotNetFlicks.Common.Models
{
    public class DataTableRequest
    {
        public string SortOrder { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DataTableRequest(string sortOrder, string currentFilter, string searchString, int? page, int? pageSize)
        {
            SortOrder = sortOrder;
            Search = searchString == null ? currentFilter : searchString;
            PageIndex = searchString == null && page.HasValue ? page.Value : 1;
            PageSize = pageSize.HasValue ? pageSize.Value : 10;
        }
    }
}
