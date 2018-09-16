using System.Collections.Generic;
using System.Data;

namespace Test.DataAccess
{
    public interface IDbOperator
    {
        string ConnectionString { get; }
        (bool Success, string Message, IEnumerable<T> Data) Query<T>(string sql);
        (bool Success, string Message, T Data) QuerySingle<T>(string sql);
        (bool Success, string Message, T Value) ExecuteScalar<T>(string sql);
        (bool Success, string Message) Execute(string sql);
        (bool Success, string Message, DataTable Output) UniversalQuery(string sql);
    }
}
