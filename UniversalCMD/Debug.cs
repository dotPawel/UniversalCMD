using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Community.CsharpSqlite.Sqlite3;

namespace UniCMD
{
    internal class Debug
    {
        public static void dbg_start()
        {
            Console.WriteLine("  UniCMD");
            Console.WriteLine("   Debug mode");
            Console.WriteLine();
            Console.WriteLine(" Using this mode may make UniCMD unstable");
            Console.WriteLine(" Use with cauction.");
            Console.WriteLine(" Execute 'exit' if you dont know what you are doing.");
            Console.WriteLine();
            dbg_prompt();
        }
        public static void dbg_prompt()
        {
            Console.WriteLine();
            Console.Write("DBG >");
            string dbg = Console.ReadLine();
            Console.WriteLine();

            if (dbg == "autoexec_print")
            {
                string file = File.ReadAllText(@"UniCMD.data\autoexec.cfg", Encoding.UTF8);

                Console.WriteLine("  Contents of autoexec.cfg");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
                dbg_prompt();
            }
            if (dbg == "isroot_?")
            {
                if (OtherUtils.runningAsAdmin == true)
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
                Program.Prompt();
            }
            if (dbg == "cdir_?")
            {
                if (Program.currentdir == null)
                {
                    Console.WriteLine("null");
                    dbg_prompt();
                }
                Console.WriteLine("Program.currentdir == " + Program.currentdir);
                dbg_prompt();
            }
            if (dbg == "clr_cdir")
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
            if (dbg == "startup")
            {
                Startup.MainStartUp();
            }
            if (dbg == "del_data")
            {
                try
                {
                    Console.WriteLine("Deleted UniCMD.data");
                    Directory.Delete(@"UniCMD.data", true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot delete UniCMD.data");
                    OtherUtils.PrintException(ex);
                }
                dbg_prompt();
            }
            if (dbg == "unipkg.deltemp")
            {
                try
                {
                    Console.WriteLine("Deleted UniPKG/TEMP");
                    Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot delete UniPKG/TEMP");
                    OtherUtils.PrintException(ex);
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
