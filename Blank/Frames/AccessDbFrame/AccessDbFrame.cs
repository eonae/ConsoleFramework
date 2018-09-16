using ConsoleAppLib;
using System;
using System.Linq;
using Test.DataAccess;
using System.Data;

namespace Blank
{
    public partial class AccessDbFrame : Frame
    {
        private static DbContext _db = new DbContext();

        public AccessDbFrame() : base(false)
        {
            Styler.GreetingsMessage = "Access to database";
            Styler.FarewellMessage = "Database closed!";


            Add(new SqlModeCommand());
            Add(new AddUserCommand());
            Add(new GetTableCommand());
            Add(new CheckCredentialsCommand());
            Add(new ChangePwdCommand());
            Add(new RemoveUserCommand());
        }
    }
}
