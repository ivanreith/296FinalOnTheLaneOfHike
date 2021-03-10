using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class PostViewModel
    {

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
        public string MemberName { get; set; }

        //[Required(ErrorMessage = "Please choose image")]  still not mandatory to add a picture
        [Display(Name = "Profile Picture")] 
        public IFormFile ProfileImage { get; set; }


    }
}
