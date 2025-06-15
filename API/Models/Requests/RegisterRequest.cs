namespace API.Models.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        //public string FirstName { get; set; }
        //public string MiddleName { get; set; }  // optional
        //public string LastName { get; set; }
    }
}
