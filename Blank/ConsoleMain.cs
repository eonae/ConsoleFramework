using ConsoleAppLib;
using Test.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using Eonae.CollectionExtensions;

namespace Blank
{
    class ConsoleMain
    {
        static void Main(string[] args)
        {
            Frame frame = new MainFrame();

            frame.Run();
        }

    }
    public class MainFrame : Frame
    {
        public MainFrame()
        {
            Dispatcher.Add(new CommandNonParams(name: "Frame",
                                       action: (args) =>
                                       {
                                           var frame = new Frame();
                                           frame.Run();
                                           return true;
                                       }));
            Dispatcher.Add(new Command(name: "add",
                                       action: (args) =>
                                       {
                                            var db = new DbContext();
                                            var result = db.AddUser(args[0],args[1]);
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
                                           return (args.Length == 2);
                                       },
                                       commandinfo: "Adds a user into database"));
            Dispatcher.Add(new CommandNonParams(name: "get",
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
                                       commandinfo: "Pulling data from database"));
            Dispatcher.Add(new Command(name: "Check",
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
                                            if (args.GetType().IsArray)
                                                return args.Length == 2;
                                            else
                                                return false;
                                        },
                                        commandinfo: "Checks match of login and password"));
            Dispatcher.Add(new Command(name: "ChangePwd",
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
                                            if (args.GetType().IsArray)
                                                return args.Length == 3;
                                            else
                                                return false;
                                        },
                                        commandinfo: "Changes the password"));
            Dispatcher.Add(new Command(name: "Remove",
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
                                        commandinfo: "Removes a user form the database by id."));
            // На основе фреймов нужно 
        }
    }
}
