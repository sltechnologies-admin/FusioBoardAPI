using System.ComponentModel.DataAnnotations;

namespace API.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        public int? UserId { get; set; }  // Required for update scenarios

        public required string Username { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [StringLength(100, MinimumLength = 6)]
        public required string Password { get; set; }
    }

}
