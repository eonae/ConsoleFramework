using Eonae.Terminal;
using System;

namespace Blank
{
    public partial class AccessDbFrame
    {
        public class AddUserCommand : Command
        {
            public AddUserCommand() : base
                ("Add", AddUser, Valid, "@login @password - Adds a user into database.")
            { }

            private static bool AddUser(params string[] args)
            {
                var result = _db.AddUser(args[0], args[1]);
                if (result.Success)
                {
                    Console.WriteLine("User added successfully!"); return true;
                }
                else
                {
                    Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                }
            }
            private static bool Valid(params string[] args)
            {
                return ArgsCount(args)==2;
            }
        }
    }
}
