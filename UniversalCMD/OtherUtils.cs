using IronPython.Runtime.Operations;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;
using static Community.CsharpSqlite.Sqlite3;
using static IronPython.Modules._ast;

namespace UniCMD
{
    internal class OtherUtils
    {
        public static string UniCMD_Name = System.AppDomain.CurrentDomain.FriendlyName;
        public static bool IsAdmin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
        public static string ReturnCPUName(string cpu)
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                cpu = mo["Name"].ToString();
            }
            return cpu;
        }
        public static void ClearConsole()
        {
            Console.Clear();
            Program.Prompt();
        }
        public static void PrintException(Exception exc)
        {
            if (Startup.showExceptions == true)
            {
                Console.WriteLine(" exception : \n" + exc.Message);
            }           
        }
        public static void Echo()
        {
            string text = Program.Command.Replace("echo ", "");

            if (text.StartsWith("/ptm "))
            {
                text = text.Replace("/ptm ", "");
                text = ConfigCommands.ApplyTextModules(text);
            }

            Console.WriteLine(text);
            Program.Prompt();
        }
        public static void Sleep()
        {
            string input = Program.Command.Replace("sleep ", "");
            bool isInt = Int32.TryParse(input, out int x);

            if (isInt == true)
            {
                Thread.Sleep(x);
            }
            else
            {
                Console.WriteLine("Invalid input value");
            }
            Program.Prompt();
        }
        public static void PTMCommand()
        {
            string input = Program.Command.Replace("[ptm-cmd] ", "");

            input = ConfigCommands.ApplyTextModules(input);

            Program.Command = input;
            Program.CommandParser(input);

            Program.Prompt();
        }
        public static void RootCheck()
        {
            if (IsAdmin == false)
            {
                Console.WriteLine("This operation requires administrator privileges, continue without?");
                Console.WriteLine("(Errors may occur)\n");
                Console.Write("  (Y)es / (N)o ");
                ConsoleKeyInfo result = Console.ReadKey();
                
                if (result.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("");
                    return;
                }
                if (result.Key == ConsoleKey.N)
                {
                    Console.WriteLine("\nReturning to main prompt..");
                    Program.Prompt();
                }
                else
                {
                    Console.WriteLine("\nReturning to main prompt..");
                    Program.Prompt();
                }
            }
        }
    }
}
