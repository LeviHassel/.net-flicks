using CoreTemplate.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoreTemplate.ViewModels.Movie
{
    public class CastMemberViewModel : EntityViewModel
    {
        public int Index { get; set; }

        public bool IsDeleted { get; set; }

        public int PersonId { get; set; }

        public string Role { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:%h}h {0:%m}m")]
        public TimeSpan ScreenTime { get; set; }

        public SelectList People { get; set; }
    }
}
