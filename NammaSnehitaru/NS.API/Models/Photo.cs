using System;

namespace NS.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime DateAddedOn { get; set; }
        public bool ProfilePic { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }

    }
}