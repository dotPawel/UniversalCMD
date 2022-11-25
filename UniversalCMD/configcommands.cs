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
        // config commands
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
        // starttext commands
        public static void ParseStarttext()
        {
            try
            {
                string starttext = File.ReadAllText(@"UniCMD.data\starttext.unicmd");

                Process proc = Process.GetCurrentProcess();

                starttext = starttext.Replace("::ver::", Program.version).Replace("::osver::", Environment.OSVersion.ToString()).Replace("::ram::", proc.PrivateMemorySize64.ToString()).Replace("::time::", DateTime.Now.ToString("hh:mm tt"));

                Console.WriteLine(starttext);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to parse starttext");
                otherutils.exception_print(e);
            }
            Program.CMD();
        }
        public static void starttxtcreate()
        {
            if (!File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                File.Create(@"UniCMD.data\starttext.unicmd").Close();
                Console.WriteLine("StartText file created.");
                Program.CMD();
            }
            Console.WriteLine("StartText file already exists.");
            Program.CMD();
        }
        public static void starttxtopen()
        {
            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                string fileName = "UniCMD.data/starttext.unicmd";
                FileInfo f = new FileInfo(fileName);
                string fullname = f.FullName;

                Console.WriteLine("  Opening StartText file..");
                Console.WriteLine("  full path : " + fullname);
                Process.Start("notepad.exe", @"UniCMD.data\starttext.unicmd");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            Program.CMD();
        }
        public static void starttxtwritetemplate()
        {
            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                Console.WriteLine("Writing template to starttext.unicmd");
                using (StreamWriter sw = File.AppendText(@"UniCMD.data\starttext.unicmd"))
                {
                    sw.WriteLine("Example UniCMD StartText file.");
                    sw.WriteLine();
                    sw.WriteLine(" ver : ::ver::");
                    sw.WriteLine(" osver : ::osver::");
                    sw.WriteLine(" ram : ::ram::");
                    sw.WriteLine(" time : ::time::");
                    sw.Close();
                }
                Console.WriteLine("Finished writing template.");
            }
            else
            {
                Console.WriteLine("StartText file not found.");
            }
            Program.CMD();
        }

    }
}
