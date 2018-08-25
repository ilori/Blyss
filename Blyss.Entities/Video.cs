namespace Blyss.Entities
{

    using System;

    public class Video
    {

        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string YouTubeId { get; set; }

        public string PlainYoutubeId { get; set; }

        public Course Course { get; set; }

        public string CourseId { get; set; }

    }

}