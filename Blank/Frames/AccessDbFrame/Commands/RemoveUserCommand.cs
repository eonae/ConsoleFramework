using Eonae.Terminal;
using System;

namespace Blank
{
    public partial class AccessDbFrame
    {
        private class RemoveUserCommand : Command
        {
            public RemoveUserCommand() : base
                ("Remove", RemoveUser, Valid, "@id - Removes a user form the database by id.")
            { }
            private static bool RemoveUser(params string[] args)
            {
                var result = _db.RemoveUser(int.Parse(args[0]));
                if (result.Success)
                    Console.WriteLine("User removed!");
                else
                    Console.WriteLine($"Something gone wrong:\n{result.Message}");
                return result.Success;
            }
            private static bool Valid(params string[] args)
            {
                if (ArgsCount(args) == 1)
                    if (int.TryParse(args[0], out int i)) return true;
                return false;
            }
        }
    }
}
