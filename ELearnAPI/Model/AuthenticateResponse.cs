namespace ELearnAPI.Model
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
