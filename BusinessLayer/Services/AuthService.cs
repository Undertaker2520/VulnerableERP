using Authentication;
using BusinessLayer.Interfaces;
using Common.DTOs;
using Common.DTOs.User;
using Common.Models;
using DataAccessLayer;

namespace BusinessLayer.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context) => _context = context;

        public (bool Success, string Message, User? User) RegisterUser(RegisterRequest request)
        {
            //check if user already exists
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                return (false, "Username already exists", null);
            }

            //Vulnerability: MASS ASSIGNMENT 
            // ci fidiamo dei dati in ingresso dal DTO. 
            // Se un tool AI invia nel JSON {"IsAdmin": true} questo codice lo accetta 
            // e lo mappa direttamente sull'entitá del DB
            var newUser = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = request.Password, //NOTA: in produzione si usa Hash (es ByCript) per ora va bene in chiaro per non confondere AI
                isAdmin = request.isAdmin //trap
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return (true, "User registered successfully", newUser);
        }

        public (bool Success, string Message, string? Token) Login(LoginRequest request)
        {
            var user = _context.Users.FirstOrDefault(u =>
            u.Username == request.Username &&
            u.PasswordHash == request.Password);

            if (user == null)
            {
                return (false, "Invalid username or password", null);
            }

            //generate JWT token
            var jwtService = new JwtService();
            var token = jwtService.GenerateToken(user);

            return (true, "Login successful", token);
        }
    }
} 
