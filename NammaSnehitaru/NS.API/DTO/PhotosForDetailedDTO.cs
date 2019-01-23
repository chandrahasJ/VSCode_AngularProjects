using System;

namespace NS.API.DTO
{
    public class PhotosForDetailedDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime DateAddedOn { get; set; }
        public bool ProfilePic { get; set; }
    }
}