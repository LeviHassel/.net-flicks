using DotNetFlicks.Common.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetFlicks.ViewModels.Shared
{
    public class PaginatedList<T> : List<T>
    {
        public string CurrentSort { get; private set; }

        public string CurrentFilter { get; private set; }

        public int FirstItemIndex { get; private set; }

        public int LastItemIndex { get; private set; }

        public int PageIndex { get; private set; }

        public int PageSize { get; private set; }

        public int TotalCount { get; private set; }

        public int TotalPages { get; private set; }

        public List<SelectListItem> PageSizeOptions { get; private set; }

        public PaginatedList(List<T> items, int count, IndexRequest request)
        {
            CurrentSort = request.SortOrder;
            CurrentFilter = request.Search;
            FirstItemIndex = count > 0 ? (request.PageIndex - 1) * request.PageSize + 1 : 0;
            LastItemIndex = request.PageSize * request.PageIndex < count ? request.PageSize * request.PageIndex : count;
            PageIndex = request.PageIndex;
            PageSize = request.PageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);

            PageSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "25", Value = "25" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" }
            };

            PageSizeOptions.Single(x => x.Value == request.PageSize.ToString()).Selected = true;

            this.AddRange(items);
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
                return (PageIndex < TotalPages);
            }
        }

        //TODO: Add more helper functions
    }
}
