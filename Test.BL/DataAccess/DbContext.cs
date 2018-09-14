using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
    
namespace Test.BL
{
    public interface IDbContext
    {
        (bool Success, string Message) AddUser(string login, string password);
        (bool Success, string Message, IEnumerable<User> Data) GetUsers();
        (bool Success, string Message) ChangePwd(string login, string newPwd);
        (bool Success, string Message) RemoveUser(int id);
        (bool Success, string Message, bool Result) CheckCredentials(string login, string password);
    }

    public class DbContext : IDbContext
    {
        public (bool Success, string Message) AddUser(string login, string password)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnStr()))
                {
                    conn.Execute($"INSERT INTO Users VALUES ('{login}','{password}')");
                    return (true, string.Empty);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public (bool Success, string Message) ChangePwd(string login, string newPwd)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnStr()))
                {
                    conn.Execute($"UPDATE Users SET Pwd = '{newPwd}' WHERE Lgn = '{login}'");
                    return (true, string.Empty);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public (bool Success, string Message, bool Result) CheckCredentials(string login, string password)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnStr()))
                {
                    var pwd = conn.ExecuteScalar($"SELECT Pwd FROM Users WHERE Lgn = '{login}'");
                    return (true, string.Empty, password.Equals(pwd));
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, false);
            }
        }

        public (bool Success, string Message, IEnumerable<User> Data) GetUsers()
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnStr()))
                {
                    var output = conn.Query<User>("SELECT Id, Lgn as Login, Pwd as Password FROM Users").ToList();
                    return (true, string.Empty, output);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public (bool Success, string Message) RemoveUser(int id)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnStr()))
                {
                    conn.Execute($"DELETE FROM Users WHERE Id = {id}");
                    return (true, string.Empty);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        private string ConnStr()
        {
            return ConfigurationManager.ConnectionStrings["main"].ConnectionString;
        }
    }


    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

}
