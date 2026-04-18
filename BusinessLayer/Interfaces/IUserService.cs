using Common.DTOs;
namespace BusinessLayer.Interfaces;

public interface IUserService
{
    List<UserResponse> GetAllUsers();
}