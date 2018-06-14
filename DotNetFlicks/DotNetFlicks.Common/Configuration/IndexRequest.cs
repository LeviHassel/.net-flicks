namespace DotNetFlicks.Common.Configuration
{
    public class IndexRequest
    {
        public string SortOrder { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
