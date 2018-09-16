using ConsoleAppLib;
using System;
using System.Linq;
using Eonae.CollectionExtensions;
using Test.DataAccess;
using System.Data;
using Helper;

namespace Blank
{
    public class AccessDbFrame : Frame
    {
        public AccessDbFrame()
        {


            Add(
                 name: "Sql",
                 action: (args) =>
                 {
                     var v = new ValueInput("sql: ", new StringParser("\\q")).Read();
                     Console.WriteLine(v.Abort.ToString());
                     if (v.Abort)
                         return false;
                     else
                     {
                         var db = new DbContext();
                         var result = db.ExecuteSql(v.Result.ToString());
                         if (result.Output!=null)
                            Console.WriteLine(result.Output.CreateStringTable());
                         return result.Success;
                     }
                 },
                commandinfo: "Executes sqlStatement");

            Add(
                name: "Add",
                action: (args) =>
                {
                    var db = new DbContext();
                    var result = db.AddUser(args[0], args[1]);
                    if (result.Success)
                    {
                        Console.WriteLine("User added successfully!"); return true;
                    }
                    else
                    {
                        Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                    }
                },
                validation: (args) =>
                {
                    if (args != null)
                        if (args.GetType().IsArray)
                            return args.Length == 2;
                        else
                            return false;
                    else
                        return false;
                },
                commandinfo: "@login @password - Adds a user into database.");

            Add(
                name: "Get",
                action: (args) =>
                {
                    var db = new DbContext();
                    var result = db.GetUsers();
                    if (result.Success)
                    {
                        if (result.Item3.Count() != 0)
                            Console.WriteLine(result.Item3.CreateStringTable("Users"));
                        else
                            Console.WriteLine("No data in the table!");
                        return true;

                    }
                    else
                    {
                        Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                    }
                },
                commandinfo: "Pulling data from database");

            Add(
                name: "Check",
                action: (args) =>
                {
                    var db = new DbContext();
                    var result = db.CheckCredentials(args[0], args[1]);
                    if (result.Success)
                    {
                        if (result.Result)
                            Console.WriteLine("Match!");
                        else
                            Console.WriteLine("Invalid login of password!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                    }

                },
                validation: (args) =>
                {
                    if (args != null)
                        if (args.GetType().IsArray)
                            return args.Length == 2;
                        else
                            return false;
                    else
                        return false;
                },
                commandinfo: "@login @password - Checks match of login and password.");

            Add(
                name: "ChangePwd",
                action: (args) =>
                {
                    var db = new DbContext();
                    var check = db.CheckCredentials(args[0], args[1]);
                    if (check.Success)
                    {
                        if (check.Result)
                        {
                            var result = db.ChangePwd(args[0], args[2]);
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
                },
                validation: (args) =>
                {
                    if (args != null)
                        if (args.GetType().IsArray)
                            return args.Length == 3;
                        else
                            return false;
                    else
                        return false;
                },
                commandinfo: "@login @oldPassword @newPassword - Changes the password.");

            Add(
                name: "Remove",
                action: (args) =>
                {
                    var db = new DbContext();
                    var result = db.RemoveUser(int.Parse(args[0]));
                    if (result.Success)
                    {
                        Console.WriteLine("User removed!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine($"Something gone wrong:\n{result.Message}"); return false;
                    }
                },
                validation: (args) =>
                {
                    return args.Length == 1 && int.TryParse(args[0], out int i);
                },
                commandinfo: "@id - Removes a user form the database by id.");
        }
    }
}
