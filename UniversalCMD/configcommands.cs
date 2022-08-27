using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class configcommands
    {
        public static void openconfig()
        {
            try
            {
                string fileName = "UniCMD.data/config.cfg";
                FileInfo f = new FileInfo(fileName);
                string fullname = f.FullName;
                
                Console.WriteLine("  Opening configuration file..");
                Console.WriteLine("  full path : " + fullname);
                Process.Start(@"notepad.exe", fullname);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open configuration file");
                otherutils.exception_print(ex);
            }
            Program.CMD();
        }
        public static void rewriteconfig()
        {
            try
            {
                Console.WriteLine("  Wiping config.cfg ..");
                File.WriteAllBytes(@"UniCMD.data/config.cfg", new byte[0]);
                Console.WriteLine("  Writing template ..");
                Startup.writetemplate();
                Console.WriteLine("  Configuration restored to default");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to restore configuration file");
                otherutils.exception_print(ex);
            }
            Program.CMD();
        }
        public static void printconfig()
        {
            try
            {
                string file = File.ReadAllText(@"UniCMD.data/config.cfg", Encoding.UTF8);

                Console.WriteLine("  Current configuration file");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not print configuration file");
                otherutils.exception_print(ex);
            }
            Program.CMD();
        }
    }
}
