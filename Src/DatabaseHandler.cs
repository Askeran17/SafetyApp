using System;
using System.Data;
using MySql.Data.MySqlClient;
using SafeVault.Auth;

namespace SafeVault
{
    public class DatabaseHandler
    {
        private readonly string connectionString = "Server=localhost;Database=safevault_db;Uid=root;Pwd=YourPasswordHere;";

        
        public bool CreateUser(string username, string email, string hashedPassword, string role)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Username, Email, HashedPassword, Role) VALUES (@username, @email, @password, @role)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@password", hashedPassword);
                    command.Parameters.AddWithValue("@role", role);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        
        public User? GetUserByUsername(string username)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Username, HashedPassword, Role FROM Users WHERE Username = @username";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Username = reader.GetString("Username"),
                                HashedPassword = reader.GetString("HashedPassword"),
                                Role = reader.GetString("Role")
                            };
                        }
                    }
                }
            }

            return null;
        }

    
    }
}



