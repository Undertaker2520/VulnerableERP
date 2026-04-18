using BusinessLayer.Interfaces;
using Common.DTOs;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore; 

namespace BusinessLayer.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context) => _context = context;

    public async Task<List<UserResponse>> GetAllUsersAsync()
    {
        return await _context.Users.Select(u => new UserResponse
        {
            Id = u.id,
            Username = u.Username,
            Email = u.Email,
            IsAdmin = u.isAdmin
        }).ToListAsync();
    }
}