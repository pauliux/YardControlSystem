using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YardControlSystem.Models.Enums;

namespace YardControlSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        //[Column(TypeName ="nvarchar(100)")]
        public string Name { get; set; }
        [PersonalData]
        //[Column(TypeName = "nvarchar(100)")]
        public string Surname { get; set; }
        [PersonalData]
        //[Column(TypeName = "int(100)")]
        public Role Role { get; set; }
    }
}
