using Eonae.Terminal;
using System;
using Eonae.CollectionExtensions;

namespace Blank
{
    public partial class AccessDbFrame
    {
        private class GetTableCommand : Command
        {
            public GetTableCommand():base
                ("Get", GetTable, Valid, "Retrieves data from specified database table")
            { }

            private static bool GetTable(params string[] args)
            {
                var results = _db.ExecuteSql($"SELECT * FROM {args[0]}");
                if (results.Success)
                {
                    Console.WriteLine();
                    Console.WriteLine(results.Output.CreateStringTable());
                }
                    
                else
                    Console.WriteLine(results.Message);
                return true;
            }

            private static bool Valid(params string[] args)
            {
                return ArgsCount(args)==1;
            }
        }
    }
}
