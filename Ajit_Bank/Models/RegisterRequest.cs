﻿namespace Ajit_Bank.Models
{
    public class RegisterRequest
    {
        public string Username { get; set; } 
        public string Password { get; set; }

        public string FullName { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; } 
    }
}
