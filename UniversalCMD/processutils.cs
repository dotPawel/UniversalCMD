using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class processutils
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
            Console.WriteLine("This will try to end every process seen by UniCMD");
            Console.WriteLine("and will end UniCMD itself");
            if (otherutils.runningAsAdmin == false)
            {
                Console.WriteLine("due to lack of admin permissions this is likely to fail at most");
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
                        if (process.ProcessName == otherutils.unicmdName)
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
                otherutils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void RunProcess()
        {
            if (Program.currentdir == null)
            {
                fileutils.NoDirSet();
            }
            string file = Program.command.Replace("proc run ", "");
            if (File.Exists(Program.currentdir + file))
            {
                Console.WriteLine("Selected : " + file);
                Console.WriteLine("Enter arguments to selected file");
                Console.WriteLine("for none leave blank\n");
                Console.Write(" >");
                string arguments = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = Program.currentdir + file;
                    proc.StartInfo.Arguments = arguments;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();

                    Console.WriteLine("Started process..");
                    Console.WriteLine("File : " + Program.currentdir + file);
                    Console.WriteLine("Args : " + arguments);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start process");
                    otherutils.PrintException(ex);
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
                Console.WriteLine("Selected : " + file);
                Console.WriteLine("Enter arguments to selected file");
                Console.WriteLine("for none leave blank\n");
                Console.Write(" >");
                string arguments = Console.ReadLine();
                Console.WriteLine();
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = file;
                    proc.StartInfo.Arguments = arguments;
                    proc.StartInfo.CreateNoWindow = false;
                    proc.StartInfo.UseShellExecute = true;
                    proc.Start();

                    Console.WriteLine("Started process..");
                    Console.WriteLine("File : " + file);
                    Console.WriteLine("Args : " + arguments);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not start process");
                    otherutils.PrintException(ex);
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
