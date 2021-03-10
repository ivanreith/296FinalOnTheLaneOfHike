using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnTheLaneOfHike.Models
{
    public class MemberViewModel
    {
        public IEnumerable<MemberModel> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}
