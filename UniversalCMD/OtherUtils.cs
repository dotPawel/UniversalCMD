using IronPython.Runtime.Operations;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;
using static IronPython.Modules._ast;

namespace UniCMD
{
    internal class OtherUtils
    {
        public static string unicmdName = System.AppDomain.CurrentDomain.FriendlyName;
        public static bool runningAsAdmin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
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
        public static void AeroCL_Loader()
        {
            Console.WriteLine("AeroCL Backbridge Loader");
            Console.WriteLine();
            Console.WriteLine(" AeroCL will load in shortly..\n");
            AeroCL_BB.acl_main();
        }
        public static void PrintException(Exception exc)
        {
            if (Startup.showExceptions == true)
            {
                Console.WriteLine(" exception : \n" + exc);
            }           
        }
        public static void Echo()
        {
            string text = Program.command.Replace("echo ", "");
            Console.WriteLine(text);
            Program.Prompt();
        }
        public static void EchoPTM()
        {
            string text = Program.command.Replace("echo /ptm ", "");

            text = ConfigCommands.ApplyTextModules(text);

            Console.WriteLine(text);
            Program.Prompt();
        }
        public static void Sleep()
        {
            string input = Program.command.Replace("sleep ", "");
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
            string input = Program.command.Replace("[ptm-cmd] ", "");

            input = ConfigCommands.ApplyTextModules(input);

            Program.command = input;
            Program.Command(input);

            Program.Prompt();
        }
    }
}
