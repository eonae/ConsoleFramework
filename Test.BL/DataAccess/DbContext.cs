using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Helper;

namespace Test.DataAccess
{

    public class DbContext
    {
        private static IDbOperator _dbOperator = new DbOperator();
       
        private enum SqlType { Select, Insert, Update, Delete, Create, Invalid }
        private SqlType GetSqlType(string sql)
        {
            string firstWord = sql.Split(' ')[0]; // Выдаст ли ошибку если в функцию будет передана пустая строка?
            var success = Enum.TryParse(firstWord, out SqlType result);
            if (success) return result;
            else return SqlType.Invalid;
        }

        ////////

        public (bool Success, string Message) AddUser(string login, string password)
        {
            return _dbOperator.Execute($"INSERT INTO Users VALUES ('{login}','{password}')");
        }
        public (bool Success, string Message) ChangePwd(string login, string newPwd)
        {
            return _dbOperator.Execute($"UPDATE Users SET Pwd = '{newPwd}' WHERE Lgn = '{login}'");
        }
        public (bool Success, string Message, bool Result) CheckCredentials(string login, string password)
        {
            return _dbOperator.ExecuteScalar<bool>($"SELECT Pwd FROM Users WHERE Lgn ='{login}'");
        }
        public (bool Success, string Message, User Data) GetUser<User>(int id)
        {
            return _dbOperator.QuerySingle<User>($"SELECT Id, Lgn as Login, Pwd as Password FROM Users WHERE id = {id}");
        }
        public (bool Success, string Message, IEnumerable<User> Data) GetUsers()
        {
            return _dbOperator.Query<User>($"SELECT Id, Lgn as Login, Pwd as Password FROM Users");
        }
        public (bool Success, string Message) RemoveUser(int id)
        {
            return _dbOperator.Execute($"DELETE FROM Users WHERE Id = {id}");
        }
    }



}
