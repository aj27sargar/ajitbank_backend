using System.Data;
using System.Numerics;
using Ajit_Bank.DataDB;
using Ajit_Bank.Models;

namespace Ajit_Bank.AppServices
{
    public class AuthService
    {
        private readonly DatabaseHelper _db;
        private readonly JwtService _jwtService;

        // Constructor with dependency injection
        public AuthService(DatabaseHelper db, JwtService jwtService)
        {
            _db = db;
            _jwtService = jwtService;
        }

        public LoginResponse Login(LoginRequest request)
        {
            // Check user credentials
            bool isValid = _db.ValidateUser(request.Username, request.Password);

            if (!isValid)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid credentials"
                };
            }

            // If valid, fetch full user data(id, email, phone, etc.)
            var user = _db.GetUserByUsername(request.Username);
            // Generate a JWT token using user details
            var token = _jwtService.GenerateToken(user);

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                Token = token
            };
        }

        public LoginResponse Register(RegisterRequest request)
        {
            bool isRegistered = _db.RegisterUser(
                request.Username,
                request.Password,
                request.FullName,
                request.Email,
                request.PhoneNumber
            );

            return new LoginResponse
            {
                Success = isRegistered,
                Message = isRegistered ? "Registration successful" : "Username already exists"
            };
        }
        public bool UpdateUserProfile(int userId, string username, string email, string phone)
        {
            return _db.UpdateUserProfile(userId, username, email, phone);
        }




    }
}
