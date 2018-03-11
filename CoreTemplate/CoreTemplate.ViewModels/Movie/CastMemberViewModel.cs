using CoreTemplate.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace CoreTemplate.ViewModels.Movie
{
    public class CastMemberViewModel : EntityViewModel
    {
        public int Index { get; set; }

        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }

        public string Role { get; set; }

        public TimeSpan ScreenTime { get; set; }

        public SelectList People { get; set; }
    }
}
