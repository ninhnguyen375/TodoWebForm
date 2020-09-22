using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TodoListWebForm.App_Code.DTO;
using TodoListWebForm.App_Code.DTO.Response;

namespace TodoListWebForm.App_Code.DAL
{
    public class AuthDAL
    {
        public static UsersDTO Login(string email, string password)
        {
            ConnectionDatabase.getConnection();
            string query = "select * from users where email = @email and password = @password";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            IDataReader reader = cmd.ExecuteReader();
            UsersDTO user = null;

            while (reader.Read())
            {
                int userId = Convert.ToInt32(reader["id"]);
                string userName = reader["name"].ToString();
                string userEmail = reader["email"].ToString();
                string userRole = reader["role"].ToString();
                user = new UsersDTO
                {
                    ID = userId,
                    Name = userName,
                    Email = userEmail,
                    Role = userRole
                };
            }

            ConnectionDatabase.closeConnection();

            return user;
        }
        public static ResponseRegisterDTO Register(UsersDTO user)
        {
            ConnectionDatabase.getConnection();

            // check duplicate email
            string queryCheckDup = "select count(1) from users where email = @email";
            SqlCommand cmdCheckDup = new SqlCommand(queryCheckDup, ConnectionDatabase.conn);
            cmdCheckDup.Parameters.AddWithValue("@email", user.Email);
            int isDup = Convert.ToInt32(cmdCheckDup.ExecuteScalar());

            if (isDup > 0)
            {
                return new ResponseRegisterDTO
                {
                    Error = "This Email has been used.",
                    Success = false
                };
            }

            string query = "insert into users (name, email, password, role)" +
                "values (@name, @email, @password, @role)";
            SqlCommand cmd = new SqlCommand(query, ConnectionDatabase.conn);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@role", user.Role);

            int rowEffected = cmd.ExecuteNonQuery();

            ConnectionDatabase.closeConnection();

            if (rowEffected == 0)
            {
                return new ResponseRegisterDTO
                {
                    Error = "Fail",
                    Success = false
                };
            }
            else
            {
                return new ResponseRegisterDTO
                {
                    Success = true
                };
            }
        }
    }

}