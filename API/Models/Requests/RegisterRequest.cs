namespace API.Models.Requests
{
    public class RegisterRequest
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }

        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }  // optional
        //public string LastName { get; set; }
    }
}
