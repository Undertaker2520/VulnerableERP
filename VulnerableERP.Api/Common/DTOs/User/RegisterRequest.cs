namespace Common.DTOs.User
{
    public class RegisterRequest 
    {
        public String Username { get; set; } = String.Empty;
        public String Password { get; set; } = String.Empty;
        public String Email { get; set; } = String.Empty;   
        public bool isAdmin { get; set; }
    }
}
