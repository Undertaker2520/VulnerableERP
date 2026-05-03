namespace Common.DTOs;

public class UserResponse
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } 
    //Vulnerabilita': in un'app sicura il DTO di registrazione (esposto) non dovrebbe mai contenere un campo di privilege excalation cosi esposto
}