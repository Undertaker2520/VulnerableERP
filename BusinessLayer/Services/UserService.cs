using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) => _context = context;

    public List<UserResponse> GetAllUsers()
    {
        return _context.Users.Select(u => new UserResponse
        {
            Id = u.id,
            Username = u.Username,
            Email = u.Email,
            IsAdmin = u.isAdmin
        }).ToList();
    }
}