using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public enum SqlType { SELECT, INSERT, UPDATE, DELETE, CREATE, DROP, INVALID }
    public class SqlHelper
    {

        public static SqlType GetSqlType(string sql)
        {
            string firstWord = sql.Split(' ')[0].ToUpper(); // Выдаст ли ошибку если в функцию будет передана пустая строка?
            var success = Enum.TryParse(firstWord, out SqlType result);
            if (success) return result;
            else return SqlType.INVALID;
        }
    }
}
