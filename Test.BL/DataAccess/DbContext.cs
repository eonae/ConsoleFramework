﻿using System;
using System.Collections.Generic;
using System.Data;

namespace Test.DataAccess
{

    public class DbContext
    {
        private enum SqlType { SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER, DROP, INVALID }
        private static SqlType GetSqlType(string sql)
        {
            string firstWord = sql.Split(' ')[0].ToUpper(); // Выдаст ли ошибку если в функцию будет передана пустая строка?
            var success = Enum.TryParse(firstWord, out SqlType result);
            if (success) return result;
            else return SqlType.INVALID;
        }

        private static IDbOperator _dbOperator = new DapperDbOperator();

        public (bool Success, string Message, DataTable Output) ExecuteSql(string sql)
        {
            SqlType type = GetSqlType(sql);
            switch (type)
            {
                case SqlType.SELECT:
                    var queryResult = _dbOperator.UniversalQuery(sql);
                    return queryResult;
                case SqlType.INVALID:
                    return (false, "Invalid query!", null);
                default:
                    var commandResult = _dbOperator.Execute(sql);
                    return (commandResult.Success, commandResult.Message, null);
            }
        }

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
