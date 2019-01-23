using System;
using System.Collections.Generic;
namespace NS.API.Models
{
    public class User
    {
        public int Id{ get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AKA { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastActiveTime { get; set; }
        public string ProfileIntroduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}   