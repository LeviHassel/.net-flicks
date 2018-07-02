using Microsoft.AspNetCore.Mvc;
using System;

namespace DotNetFlicks.Common.Models
{
    public class DataTableRequest
    {
        public string SortOrder { get; set; }

        public string Search { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public DataTableRequest(Controller controller, string sortOrder, string search, int? pageIndex, int? pageSize)
        {
            //Get the unique identity of this controller request (for example, the prefix for Index in MovieController would be "Movie_Index_")
            var tempDataPrefix = controller.RouteData.Values["controller"].ToString() + "_" + controller.RouteData.Values["action"].ToString() + "_";

            //Retrieve the last known Data Table configuration for this controller request using TempData
            var tempSortOrder = (string)controller.TempData[tempDataPrefix + "SortOrder"];
            var tempSearch = (string)controller.TempData[tempDataPrefix + "Search"];
            var tempPageIndex = (int?)controller.TempData[tempDataPrefix + "PageIndex"];
            var tempPageSize = (int?)controller.TempData[tempDataPrefix + "PageSize"];

            //If all parameters for this controller request are empty, set them equal to the last known Data Table configuration for this page
            if (sortOrder == null && search == null && pageIndex == null && pageSize == null)
            {
                sortOrder = tempSortOrder;
                search = tempSearch;
                pageIndex = tempPageIndex;
                pageSize = tempPageSize;
            }

            SortOrder = string.IsNullOrEmpty(sortOrder) ? "Name_Asc" : sortOrder;
            Search = search;
            PageSize = pageSize.HasValue ? pageSize.Value : 10;

            //If the page size has changed, update the page index accordingly to show the expected items
            if (tempPageSize.HasValue && tempPageIndex.HasValue && tempPageSize.Value != PageSize)
            {
                var firstItemIndex = (double)((tempPageIndex.Value - 1) * tempPageSize.Value + 1);

                PageIndex = (int)Math.Ceiling(firstItemIndex / PageSize);
            }
            //If the desired page index has a value and the search string has not changed, set page index to the desired value
            else if (pageIndex.HasValue && Search == tempSearch)
            {
                PageIndex = pageIndex.Value;
            }
            //If the search string has changed or the page index has no value, reset the page index to the first page
            else
            {
                PageIndex = 1;
            }

            //Populate the TempData for this controller request so that this Data Table configuration can be returned to later
            controller.TempData[tempDataPrefix + "SortOrder"] = SortOrder;
            controller.TempData[tempDataPrefix + "Search"] = Search;
            controller.TempData[tempDataPrefix + "PageIndex"] = PageIndex;
            controller.TempData[tempDataPrefix + "PageSize"] = PageSize;

            controller.TempData.Keep();
        }
    }
}
