namespace Blyss.Entities
{

    using System;
    using System.Collections.Generic;

    public class Language
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }

}