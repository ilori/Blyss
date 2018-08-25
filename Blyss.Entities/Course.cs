namespace Blyss.Entities
{

    using System;

    public class Course
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsApproved { get; set; }

        public string CategoryId { get; set; }

        public string VideoId { get; set; }

        public string LanguageId { get; set; }

        public string UserId { get; set; }

        public DateTime CreatedOn { get; set; }

        public Category Category { get; set; }

        public Language Language { get; set; }

        public User User { get; set; }

        public Video Video { get; set; }

    }

}