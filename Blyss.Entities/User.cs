namespace Blyss.Entities
{

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Country { get; set; }

        [NotMapped]
        public string FullName => this.ToString();

        public string Description { get; set; }

        public byte[] ProfilePicture { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }

    }

}