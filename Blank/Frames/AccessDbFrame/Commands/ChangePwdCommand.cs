using Eonae.Terminal;
using System;

namespace Blank
{
    public partial class AccessDbFrame
    {
        private class ChangePwdCommand : Command
        {
            public ChangePwdCommand() : base
                ("Chpwd", Change, Valid, "@login @oldPassword @newPassword - Changes the password.")
            { }

            private static bool Change(params string[] args)
            {
                var check = _db.CheckCredentials(args[0], args[1]);
                if (check.Success)
                {
                    if (check.Result)
                    {
                        var result = _db.ChangePwd(args[0], args[2]);
                        if (result.Success)
                        {
                            Console.WriteLine("Password changed!");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Something gone wrong:\n{check.Message}"); return false;
                }
                return false;
            }
            private static bool Valid(params string[] args)
            {
                return ArgsCount(args) == 3;
            }
        }
    }
}
