using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class CommentModel
    {
        [Key]
        public int CommentId { get; set; }
        public int PostID { get; set; }
        [Required]
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        [Required]
        public MemberModel Member { get; set; }

    }
}
