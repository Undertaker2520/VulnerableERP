using Common.DTOs;

namespace BusinessLayer.Interfaces;

public interface IUserService
{
    Task<List<UserResponse>> GetAllUsersAsync();
}