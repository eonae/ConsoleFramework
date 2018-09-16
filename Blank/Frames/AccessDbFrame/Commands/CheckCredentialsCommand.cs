using ConsoleAppLib;
using System;

namespace Blank
{
    public partial class AccessDbFrame
    {
        public class CheckCredentialsCommand : Command
        {
            public CheckCredentialsCommand() : base
                ("Check", Check, Valid, "@login @password - Checks match of login and password.")
            { }
            private static bool Check(params string[] args)
            {
                var result = _db.CheckCredentials(args[0], args[1]);
                if (result.Success)
                {
                    if (result.Result)
                        Console.WriteLine("Match!");
                    else
                        Console.WriteLine("Invalid login of password!");
                }
                else
                {
                    Console.WriteLine($"Something gone wrong:\n{result.Message}");
                }
                return result.Success;
            }
            private static bool Valid(params string[] args)
            {
                return ArgsCount(args)==2;
            }
        }
    }
}
