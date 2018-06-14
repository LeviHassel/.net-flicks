namespace DotNetFlicks.Common.Configuration
{
    public class IndexQuery
    {
        public string SortOrder { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
