using System.Data;
using Ajit_Bank.Models;
using BCrypt.Net;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Ajit_Bank.DataDB
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        // Login Check
        public bool ValidateUser(string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("sp_ValidateUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                var result = cmd.ExecuteScalar();

                if (result == null) return false;

                string hashedPassword = result.ToString()!;
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
        }

        // Register New User
        public bool RegisterUser(string username, string password, string fullName, string email, string phone)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("sp_RegisterUser", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                con.Open();
                var result = cmd.ExecuteScalar();

                return result != null && Convert.ToInt32(result) == 1;
            }
        }

        // Get full user details by username
        public User GetUserByUsername(string username)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_GetUserByUsername", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Username", username);

                con.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Username = reader["Username"].ToString(),
                            Email = reader["Email"]?.ToString(),
                            Phone = reader["PhoneNumber"]?.ToString()
                        };
                    }
                }
            }

            return null!;
        }

        // ✅ Corrected UpdateUserProfile Method
        public bool UpdateUserProfile(int userId, string username, string email, string phone)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand("sp_UpdateUserProfile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", userId);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
