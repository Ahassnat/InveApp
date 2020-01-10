using System.Collections.Generic;

namespace Test.API.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}