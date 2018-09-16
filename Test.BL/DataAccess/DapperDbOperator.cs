using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Test.DataAccess
{
    public class DapperDbOperator : IDbOperator
    {
        public string ConnectionString { get; }

        public DapperDbOperator(string connStr = "")
        {
            if (connStr == "")
                ConnectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            else
                ConnectionString = connStr;
        }

        public (bool Success, string Message, DataTable Output) UniversalQuery(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    var adapter = new SqlDataAdapter(sql, conn as SqlConnection);
                    var ds = new DataSet();
                    adapter.Fill(ds);
                    return (true, string.Empty, ds.Tables[0]);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }

        public (bool Success, string Message) Execute(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    var output = conn.Execute(sql);
                    return (true, string.Empty);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }
        public (bool Success, string Message, T Value) ExecuteScalar<T>(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    var output = (T)conn.ExecuteScalar(sql);
                    return (true, string.Empty, output);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, default(T));
            }
        }
        public (bool Success, string Message, IEnumerable<T> Data) Query<T>(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    var output = conn.Query<T>(sql).ToList();
                    return (true, string.Empty, output);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, null);
            }
        }
        public (bool Success, string Message, T Data) QuerySingle<T>(string sql)
        {
            try
            {
                using (IDbConnection conn = new SqlConnection(ConnectionString))
                {
                    var output = conn.QuerySingle<T>(sql);
                    return (true, string.Empty, output);
                }
            }
            catch (Exception e)
            {
                return (false, e.Message, default(T));
            }
        }
    }


}
