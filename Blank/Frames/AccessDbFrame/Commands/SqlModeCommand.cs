using Eonae.Terminal;
using System;
using Eonae.CollectionExtensions;

namespace Blank
{
    public partial class AccessDbFrame
    {
        private class SqlModeCommand : Command
        {
            public SqlModeCommand() : base
                ("Sql", Sql, Valid, commandinfo: "Activates SQL mode: use can interact with database via T-SQL")
            { }
            
            private static bool Sql(params string[] args)
            {
                {
                    Console.WriteLine("SQL-mode activated..");
                    while (true)
                    {
                        var v = new ValueInput("sql: ", new StringParser("\\q")).Read();
                        if (v.Abort)
                            break;
                        else
                        {
                            var result = _db.ExecuteSql(v.Result.ToString());
                            if (result.Success)
                            {
                                if (result.Output != null)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine(result.Output.CreateStringTable());
                                }

                                else
                                    Console.WriteLine("SQL command success!");
                            }
                            else
                            {
                                Console.WriteLine("SQL failed!");
                                Console.WriteLine(result.Message);
                            }
                        }
                    }
                    Console.WriteLine("SQL-mode deactivated..");
                    return true;
                }
            }
            private static bool Valid(params string[] args)
            {
                return ArgsCount(args)==0;
            }

        }
    }
}
