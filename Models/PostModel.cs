using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class PostModel
    {
        public List<CommentModel> comments = new List<CommentModel>();
        // EF Core will configure the database to generate this value
        [Key]
        public int  PostID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Title between 1 and 50 chars")]
        public string PostTitle { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "storyTopic between 1 and 25 chars")]
        public string PostTopic { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "storyText between 1 and 250 chars")]
        public string PostText { get; set; }

        // changfe due to identity  =>  stuff public string Name { get; set; }
        public MemberModel Member { get; set; }

        public DateTime PostTime { get; set; }
        // Not required to add a picture
        public string ProfilePicture { get; set; }

        public List<CommentModel> Comments
        {
            get { return comments; }
        }

        public string Slug =>
         PostTitle?.Replace(' ', '-').ToLower() + '-' + PostTopic?.ToString();

    }
}
