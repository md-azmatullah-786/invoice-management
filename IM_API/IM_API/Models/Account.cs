namespace IM_API.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public Customer Customer { get; set; }
        public string Token { get; set; }
    }
}
