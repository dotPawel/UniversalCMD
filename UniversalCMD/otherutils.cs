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
        public static void cpuname()
        {
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject mo in mos.Get())
            {
                Console.WriteLine("   └ " + mo["Name"]);
            }
        }
        public static void clearconsole()
        {
            Console.Clear();
            Program.CMD();
        }
        public static void acl_loader()
        {
            Console.WriteLine("AeroCL Backbridge Loader");
            Console.WriteLine();
            Console.WriteLine(" AeroCL will load in shortly..\n");
            aerocl_bb.acl_main();
        }
        public static void exception_print(Exception exc)
        {
            if (Startup.showExceptions == true)
            {
                Console.WriteLine(" exception : \n" + exc);
            }           
        }
        public static void echo()
        {
            string text = Program.command.Replace("echo ", "");
            Console.WriteLine(text);
            Program.CMD();
        }
    }
}
