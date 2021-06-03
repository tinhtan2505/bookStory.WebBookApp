using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStory.Data.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Dob { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Translation> Translations { get; set; }
        public List<Report> Reports { get; set; }
        public List<Rating> Ratings { get; set; }
        public List<Project> Projects { get; set; }
    }
}