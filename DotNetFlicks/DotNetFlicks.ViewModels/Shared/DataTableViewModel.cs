using DotNetFlicks.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.ViewModels.Shared
{
    public class DataTableViewModel
    {
        public string SortOrder { get; private set; }

        public string Search { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int FirstItemIndex { get; private set; }

        public int LastItemIndex { get; private set; }

        public int FilteredCount { get; private set; }

        public int TotalCount { get; private set; }

        public int PageCount { get; private set; }

        public List<SelectListItem> PageSizeOptions { get; private set; }

        public DataTableViewModel(DataTableRequest request, int filteredCount, int totalCount)
        {
            SortOrder = request.SortOrder;
            Search = request.Search;
            PageIndex = request.PageIndex;
            PageSize = request.PageSize;
            FirstItemIndex = filteredCount > 0 ? (request.PageIndex - 1) * request.PageSize + 1 : 0;
            LastItemIndex = request.PageSize * request.PageIndex < filteredCount ? request.PageSize * request.PageIndex : filteredCount;
            FilteredCount = filteredCount;
            TotalCount = totalCount;
            PageCount = (int)Math.Ceiling(filteredCount / (double)request.PageSize);

            PageSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" }
            };

            PageSizeOptions.Single(x => x.Value == request.PageSize.ToString()).Selected = true;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < PageCount);
            }
        }
    }
}
