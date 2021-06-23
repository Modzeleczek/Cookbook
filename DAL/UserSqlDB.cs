using Cookbook.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Cookbook.DAL
{
    public class UserSqlDB
    {
        private SqlConnection con;
        public UserSqlDB(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("CookbookDB");
            con = new SqlConnection(connectionString); // tworzymy nowy obiekt połączenia, ale jeszcze go nie otwieramy
        }
        public void Add(User u)
        {
            string query = "UserCreate"; // nazwa procedury składowanej
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", u.userName);
            cmd.Parameters.AddWithValue("@pPassword", u.password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(SqlException) {}
            finally
            {
                con.Close();
            }
        }
        public bool Exists(string userName)
        {
            string query = "UserExists";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
            }
            catch(SqlException) // jeżeli nie można połączyć się z bazą danych
            {
                con.Close();
                return false;
            }
            var reader = cmd.ExecuteReader();
            bool result = reader.Read();
            con.Close();
            reader.Close();
            return result;
        }
        public string GetPassword(string userName)
        {
            string query = "UserGetPassword";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
            }
            catch(SqlException) // jeżeli nie można połączyć się z bazą danych
            {
                con.Close();
                return null;
            }
            var reader = cmd.ExecuteReader();
            string password;
            if(reader.Read() == false)
                password = null;
            else
                password = (string)reader["password"]; // reader.GetString(0)
            con.Close();
            reader.Close();
            return password;
        }
        public void UpdatePassword(string userName, string password) // możnaby zrobić Update do aktualizowania całego użytkownika, ale na razie ma on tylko nazwę, czyli klucz i hasło, więc tutaj tak naprawdę aktualizujemy wszystkie niekluczowe pola
        {
            string query = "UserUpdatePassword";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            cmd.Parameters.AddWithValue("@pPassword", password);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(SqlException) {}
            finally
            {
                con.Close();
            }
        }
        public void Delete(string userName)
        {
            string query = "UserDelete";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pUserName", userName);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch(SqlException) {}
            finally
            {
                con.Close();
            }
        }
    }
}
