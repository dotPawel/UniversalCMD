using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Community.CsharpSqlite.Sqlite3;

namespace UniCMD
{
    internal class Process
    {
        public static void ListProcess()
        {
            System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();
            Console.WriteLine("  Processes running : Name | ID");
            Console.WriteLine("----------------------------------------------");
            foreach (System.Diagnostics.Process process in processlist)
            {
                Console.WriteLine(" " + process.ProcessName + " | " + process.Id);
            }
        }
        public static void KillAllProcess()
        {
            // this is so fucking stupid lol
            // why does this exist in the first place

            var failed = 0;
            var success = 0;
            if (Other.IsAdmin == false)
            {
                Console.WriteLine("Due to lack of admin permissions this is likely to fail at most");
            }
            Console.Write("\n Are you sure? (Y)es / (N)o ");
            ConsoleKeyInfo choice = Console.ReadKey();
            if (choice.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Getting process list..");
                System.Diagnostics.Process[] processlist = System.Diagnostics.Process.GetProcesses();
                Console.WriteLine("----------------------------------------------");
                foreach (System.Diagnostics.Process process in processlist)
                {
                    try
                    {
                        process.Kill();
                        Console.WriteLine("SUCCESS : " + process.ProcessName + " | " + process.Id);
                        success =+ 1;
                    }
                    catch
                    {
                        Console.WriteLine("FAILED : " + process.ProcessName + " | " + process.Id);
                        failed =+ 1;
                    }
                }
            }
            else
            {
                return;
            }
            Console.WriteLine("Task finished");
            Console.WriteLine("Success : " + success);
            Console.WriteLine("Failed : " + failed);
        }
        public static void KillProcess()
        {
            var killed = 0;
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(filename);
            try
            {
                foreach (System.Diagnostics.Process p in process)
                {
                    p.Kill();
                    killed =+ 1;
                }
                Console.WriteLine("Sucessfully killed " + killed + " with the name of " + filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not kill process");
                Other.PrintException(ex);
            }
        }
        public static void RunProcess()
        {
            var file = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            bool waitforend = false;

            if (file.StartsWith("/p "))
            {
                file = file.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                file = Program.CurrentDir + file;
            }

            if (file.Contains(" /s"))
            {
                file = file.Replace(" /s", "");
                waitforend = true;
            }

            if (file.Contains(" /args "))
            {
                file = file.Split(" /args ")[0];
            }

            string[] args = Program.UserCommand.Split(" /args ");
            if (File.Exists(file))
            {
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = file;
                    if (args.Length > 1)
                    {
                        proc.StartInfo.Arguments = args[1];
                    }

                    proc.Start();

                    Console.WriteLine("Started process..");
                    Console.WriteLine("File : " + file);
                    if (args.Length > 1)
                    {
                        Console.WriteLine("Args : " + args[1]);
                    }

                    if (waitforend == true)
                    {
                        Console.WriteLine(" Waiting for process to finish");
                        proc.WaitForExit();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start process");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
    }
}
