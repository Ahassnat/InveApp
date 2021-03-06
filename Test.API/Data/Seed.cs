using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Test.API.Models;

namespace Test.API.Data
{
    public class Seed
    {
        private readonly UserManager<User> _userManager;
        public Seed(UserManager<User> userManager)
        {
            _userManager = userManager;

        }

        public void SeedUser()
        {
            if (!_userManager.Users.Any())
            {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                foreach (var user in users)
                {   
                    _userManager.CreateAsync(user, "password").Wait();
                }
            }
        }

        // public void SeedCountry()
        // {
        //     var countryData = System.IO.File.ReadAllText("");
        //     var country = JsonConvert.DeserializeObject<List<Country>>(countryData);
        //     foreach (var c in country)
        //     {
                
        //     }
        // }
    }
}