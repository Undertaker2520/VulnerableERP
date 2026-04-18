using Common.DTOs;
using Common.DTOs.User;
using Common.Models;

namespace BusinessLayer.Interfaces;

public interface IAuthService
{
    // Task permette al chiamante di attendere l'operazione senza bloccare il thread
    Task<(bool Success, string Message, User? User)> RegisterUserAsync(RegisterRequest request);

    Task<(bool Success, string Message, string? Token)> LoginAsync(LoginRequest request);
}