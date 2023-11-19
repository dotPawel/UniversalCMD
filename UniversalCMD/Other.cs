using IronPython.Runtime.Operations;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;
using static Community.CsharpSqlite.Sqlite3;
using static IronPython.Modules._ast;

namespace UniCMD
{
    internal class Other
    {
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
        public static void PrintException(Exception exc)
        {
            if (Startup.ConfigDict["showExceptions"] == true)
            {
                Console.WriteLine(" exception : \n" + exc.Message);
            }           
        }
        public static void Echo()
        {
            var text = string.Join(" ", Program.UserCommand.Split(' ').Skip(1));

            if (text.StartsWith("/ptm "))
            {
                text = text.Replace("/ptm ", "");
                text = Config.ApplyTextModules(text);
            }

            Console.WriteLine(text);
        }
        public static void Sleep()
        {
            var input = string.Join(" ", Program.UserCommand.Split(' ').Skip(1));
            bool isInt = Int32.TryParse(input, out int x);

            if (isInt == true)
            {
                Thread.Sleep(x);
            }
            else
            {
                Console.WriteLine("Invalid input value");
            }
        }
        public static void PTMCommand()
        {
            var input = string.Join(" ", Program.UserCommand.Split(' ').Skip(1));

            input = Config.ApplyTextModules(input);
            
            // this is a really retarded solution but it works. With the catch being that UserCommand goes lowercase :)
            Program.UserCommand = input;
            Program.Command = input;

            Program.CommandParser(input);
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
                    Console.WriteLine("\n");
                }
                else
                {
                    Console.WriteLine("\nReturning to main prompt..");
                    Program.ReadyToExecute = true;
                    Program.Prompt();
                }
            }
        }
    }
}
