using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class MemberModel : IdentityUser
    {
        [Required(ErrorMessage = "Name is required....")]
        [MaxLength(50, ErrorMessage = "Name between 1 and 50 chars")]
        public string Name { get; set; }
        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
