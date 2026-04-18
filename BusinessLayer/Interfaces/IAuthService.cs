using Common.DTOs;
using Common.DTOs.User;
using Common.Models;

namespace BusinessLayer.Interfaces;

public interface IAuthService
{
    // using a tuple or an object in order to return both the success status and the message
    (bool Success, string Message, User? User) RegisterUser(RegisterRequest request);

    (bool Success, string Message, string? Token) Login(LoginRequest request);

}