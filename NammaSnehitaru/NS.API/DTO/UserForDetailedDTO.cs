using System;
using System.Collections.Generic;
using NS.API.Models;

namespace NS.API.DTO
{
    public class UserForDetailedDTO
    {
        public int Id{ get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string AKA { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastActiveTime { get; set; }
        public string ProfileIntroduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDTO> Photos { get; set; }
    }
}