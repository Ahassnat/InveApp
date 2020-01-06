using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Test.API.Data;
using Test.API.Dtos;
using Test.API.Models;

namespace Test.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepo _repo;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(IAuthRepo repo, IConfiguration config,
                            SignInManager<User> signInManager,
                            UserManager<User> userManager)
        {
            _userManager = userManager;
            _repo = repo;
            _config = config;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            var userToCreate = new User
            {
                UserName = userForRegistration.UserName,
            };

            var result = await _userManager.CreateAsync(userToCreate, userForRegistration.Password);

            if (result.Succeeded)
                return StatusCode(201);

            return BadRequest(result.Errors);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLogin)
        {
            var user = await _userManager.FindByNameAsync(userForLogin.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLogin.Password, false);
            if (result.Succeeded)
            {
                var appUser = await _userManager.Users
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLogin.UserName.ToUpper());

                var userToReturn = new UserForListDto
                {
                    Id = appUser.Id,
                    UserName = appUser.UserName
                };

                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    user = userToReturn
                });

            }


            return Unauthorized();

        }

    
        private async Task<string> GenerateJwtToken(User user)
    {
        //Token By JWT

        var Claims = new List<Claim> // we change the [array] tol List becuse the array in C# is fixed 
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

        var roles = await _userManager.GetRolesAsync(user);

        foreach (var role in roles)
        {
            Claims.Add(new Claim(ClaimTypes.Role, role)); // add claim to the role of user => check the role in the jwt.io website after extract the the token from postman
        }

        var Key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));

        var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(Claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds

        };


        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);


        //ممكن نختصر الكودين اللي فوق بواحد 
        //var token= new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
    
    }

}

