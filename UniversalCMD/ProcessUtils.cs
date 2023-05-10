using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class ProcessUtils
    {
        public static void ListProcess()
        {
            Process[] processlist = Process.GetProcesses();
            Console.WriteLine("  Processes running : Name | ID");
            Console.WriteLine("----------------------------------------------");
            foreach (Process process in processlist)
            {
                Console.WriteLine(" " + process.ProcessName + " | " + process.Id);
            }
            Program.Prompt();
        }
        public static void KillAllProcess()
        {
            var failed = 0;
            var success = 0;
            if (OtherUtils.runningAsAdmin == false)
            {
                Console.WriteLine("Due to lack of admin permissions this is likely to fail at most");
            }
            Console.Write("\n Are you sure? (Y)es / (N)o ");
            ConsoleKeyInfo choice = Console.ReadKey();
            if (choice.Key == ConsoleKey.Y)
            {
                Console.WriteLine("Getting process list..");
                Process[] processlist = Process.GetProcesses();
                Console.WriteLine("----------------------------------------------");
                foreach (Process process in processlist)
                {
                    try
                    {
                        if (process.ProcessName == OtherUtils.unicmdName)
                        {
                            Console.WriteLine("SKIPPING : UniCMD exit prevention");
                        }
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
                Program.Prompt();
            }
            Console.WriteLine("Task finished");
            Console.WriteLine("Success : " + success);
            Console.WriteLine("Failed : " + failed);
            Program.Prompt();
        }
        public static void KillProcess()
        {
            var killed = 0;
            string filename = Program.command.Replace("proc end ", "");
            Process[] process = Process.GetProcessesByName(filename);
            try
            {
                foreach (Process p in process)
                {
                    p.Kill();
                    killed =+ 1;
                }
                Console.WriteLine("Sucessfully killed " + killed + " with the name of " + filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not kill process");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void RunProcess()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string file = Program.command.Replace("proc run ", "");
            if (File.Exists(Program.currentdir + file))
            {
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = Program.currentdir + file;
                    proc.Start();

                    Console.WriteLine("Started process..");
                    Console.WriteLine("File : " + Program.currentdir + file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start process");
                    OtherUtils.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            Program.Prompt();
        }
        public static void RunProcessPath()
        {
            string file = Program.command.Replace("proc run /p ", "");
            if (File.Exists(file))
            {
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = file;
                    proc.Start();

                    Console.WriteLine("Started process..");
                    Console.WriteLine("File : " + file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start process");
                    OtherUtils.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            Program.Prompt();
        }
    }
}
