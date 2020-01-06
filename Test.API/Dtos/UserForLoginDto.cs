using System.ComponentModel.DataAnnotations;

namespace Test.API.Dtos
{
    public class UserForLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8,MinimumLength=4,ErrorMessage="User Name or Password Not right")]
        public string Password { get; set; }
    }
}