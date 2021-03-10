using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class CommentViewModel
    {
        [Required]
        public int PostID { get; set; }

        [Required]
        public string CommentText { get; set; }

        public string CommenterName { get; set; } // if not authenticated user
    }
}
