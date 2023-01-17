using IronPython.Runtime.Operations;
using System.Diagnostics;
using System.Management;
using System.Security.Principal;

namespace UniCMD
{
    internal class otherutils
    {
        public static string unicmdName = System.AppDomain.CurrentDomain.FriendlyName;
        public static bool runningAsAdmin = WindowsIdentity.GetCurrent().Owner.IsWellKnown(WellKnownSidType.BuiltinAdministratorsSid);
        public static void ReturnCPUName()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                Console.WriteLine("   └ " + mo["Name"]);
            }
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
            aerocl_bb.acl_main();
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

            text = configcommands.ApplyTextModules(text);

            Console.WriteLine(text);
            Program.Prompt();
        }
    }
}
