using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Test.API.Models
{
    public class User:IdentityUser<int>
    {
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string Interests { get; set; }
        public string LookingFor { get; set; }
        public string Gender { get; set; }
        public ICollection<BodyDetail> BodyDetails { get; set; }
        public ICollection<Gallary> Gallaries { get; set; }  
        public ICollection<Country> Countries { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}