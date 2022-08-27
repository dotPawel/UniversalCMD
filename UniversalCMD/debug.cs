using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class debug
    {
        public static void dbg_start()
        {
            Console.WriteLine("\n  UniCMD");
            Console.WriteLine("   Debug mode");
            dbg_prompt();
        }
        public static void dbg_prompt()
        {
            Console.WriteLine();
            Console.Write("DBG >");
            string dbg = Console.ReadLine();
            Console.WriteLine();

            if (dbg == "isAdmin(?)")
            {
                if (otherutils.runningAsAdmin == true)
                {
                    Console.WriteLine("runningAsAdmin == True");
                }
                else
                {
                    Console.WriteLine("runningAsAdmin == False");
                }
                dbg_prompt();
            }
            if (dbg == "exit")
            {
                Program.CMD();
            }
            if (dbg == "currentdir(?)")
            {
                if (Program.currentdir == null)
                {
                    Console.WriteLine("null");
                    dbg_prompt();
                }
                Console.WriteLine("Program.currentdir == " + Program.currentdir);
                dbg_prompt();
            }
            if (dbg == "currentdir(clear)")
            {
                if (Program.currentdir == null)
                {
                    Console.WriteLine("already null");
                    dbg_prompt();
                }
                Program.currentdir = null;
                Console.WriteLine("Sucessfully set currentdir to null (cleared)");
                Console.WriteLine("Program.currentdir = null");
                dbg_prompt();
            }
            if (dbg == "startUp()")
            {
                Startup.mainstartup();
            }
            if (dbg == "delete.data()")
            {
                try
                {
                    Console.WriteLine("Deleted UniCMD.data");
                    Directory.Delete(@"UniCMD.data", true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot delete UniCMD.data");
                    otherutils.exception_print(ex);
                }
                dbg_prompt();
            }

            else
            {
                Console.WriteLine("unknown");
                dbg_prompt();
            }
        }
    }
}
