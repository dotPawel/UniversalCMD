using System.Diagnostics;

namespace UniCMD
{
    static internal class Program
    {
        public static string version = "v1.2r";
        // r - release
        // rc - release candidate
        // d - debug

        public static string currentdir;
        public static string command;
        public static string uncvcommand;
        internal static object filename;

        public static void Main()
        {
            Startup.mainstartup();
            startupscreen();
        }
        static void startupscreen()
        {
            Process proc = Process.GetCurrentProcess();
            Console.Title = "UniCMD (" + version + ")";

            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine(" UniversalCMD / Rainman / Build " + version + "\n");
            Console.WriteLine("  Host OS");
            Console.WriteLine("   └ " + Environment.OSVersion);
            Console.WriteLine("  Memory Used (by UniCMD)");
            Console.WriteLine("   └ " + proc.PrivateMemorySize64 + " (KB)");
            Console.WriteLine("  CPU Name/Model");
            otherutils.cpuname();
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine("for command index run 'help'");
            Console.WriteLine("in order to set current run 'set directory'");

            CMD();
        }
        public static void CMD()
        {
            Console.WriteLine();
            if (otherutils.runningAsAdmin == true)
            {
                Console.Write("(#) ");
            }
            if (currentdir == null)
            {
                Console.Write("@ NULL >");
            }
            else
            {
                Console.Write("@ " + currentdir + " >");
            }            
            try
            {
                uncvcommand = Console.ReadLine();
                command = uncvcommand.ToLower();
            }
            catch // for preventing crash after starting process and quiting UniCMD
            {

            }
            Console.WriteLine();

            Console.Title = "UniCMD (" + version + ") - " + command;

            // here the commands start
            if (command == "exit")
            {
                Console.WriteLine();
                Console.Write("Quitting UniCMD in.. ");
                Console.Write("3 ");
                Thread.Sleep(1000);
                Console.Write("2 ");
                Thread.Sleep(1000);
                Console.Write("1 ");
                Thread.Sleep(1000);
                Environment.Exit(0);
            }
            if (command == "about")
            {
                commandusages.about();
            }
            if (command == "clear")
            {
                otherutils.clearconsole();
            }

            if (command == "help")
            {
                commandusages.help();
            }

            // directory commands
            if (command == "set directory")
            {
                fileutils.setdir();
            }
            if (command == "clear set directory")
            {
                fileutils.clearsetdir();
            }
            if (command == "directory list")
            {
                fileutils.listdir();
            }

            if (command == "directory create")
            {
                commandusages.createdirusage();
            }
            if (command.StartsWith("directory create path "))
            {
                fileutils.createdirpath();
            }
            else if (command.StartsWith("directory create "))
            {
                fileutils.createdir();
            }

            if (command == "directory delete")
            {
                commandusages.deletedirusage();
            }
            if (command.StartsWith("directory delete path "))
            {
                fileutils.deletedirpath();
            }
            else if (command.StartsWith("directory delete "))
            {
                fileutils.deletedir();
            }

            if (command == "directory clone")
            {
                commandusages.clonedirusage();
            }
            if (command.StartsWith("directory clone path"))
            {
                fileutils.clonedirpath();
            }
            else if (command.StartsWith("directory clone "))
            {
                fileutils.clonedir();
            }

            if (command == "directory rename")
            {
                commandusages.renamedirusage();
            }
            if (command.StartsWith("directory rename path"))
            {
                fileutils.renamedirpath();
            }
            else if (command.StartsWith("directory rename "))
            {
                fileutils.renamedir();
            }


            // file commands
            if (command == "file create")
            {
                commandusages.createfileusage();
            }
            if (command.StartsWith("file create path "))
            {
                fileutils.createfilepath();
            }
            else if (command.StartsWith("file create "))
            {
                fileutils.createfile();
            }

            if (command == "file delete")
            {
                commandusages.deletefileusage();
            }
            if (command.StartsWith("file delete path "))
            {
                fileutils.deletefilepath();
            }
            else if (command.StartsWith("file delete "))
            {
                fileutils.deletefile();
            }

            if (command == "file read")
            {
                commandusages.readfileusage();
            }
            if (command.StartsWith("file read path "))
            {
                fileutils.readfilepath();
            }
            else if (command.StartsWith("file read "))
            {
                fileutils.readfile();
            }
            
            if (command == "file write")
            {
                commandusages.writefileusage();
            }
            if (command.StartsWith("file write path "))
            {
                fileutils.writefilepath();
            }
            else if (command.StartsWith("file write "))
            {
                fileutils.writefile();
            }

            if (command == "file clear")
            {
                commandusages.clearfileusage();
            }
            if (command.StartsWith("file clear path "))
            {
                fileutils.clearfilepath();
            }
            else if (command.StartsWith("file clear "))
            {
                fileutils.clearfile();
            }

            if (command == "file clone")
            {
                commandusages.clonefileusage();
            }
            if (command.StartsWith("file clone path "))
            {
                fileutils.clonefilepath();
            }
            else if (command.StartsWith("file clone "))
            {
                fileutils.clonefile();
            }

            if (command == "file rename")
            {
                commandusages.renamefileusage();
            }
            if (command.StartsWith("file rename path "))
            {
                fileutils.renamefilepath();
            }
            else if (command.StartsWith("file rename "))
            {
                fileutils.renamefile();
            }

            // process commands
            if (command == "process list")
            {
                processutils.listprocess();
            }

            if (command == "process start")
            {
                commandusages.processstartusage();
            }
            if (command.StartsWith("process start path "))
            {
                processutils.startprocesspath();
            }
            else if (command.StartsWith("process start "))
            {
                processutils.startprocess();
            }

            if (command == "process kill")
            {
                commandusages.processkillusage();
            }
            if (command == "process kill all")
            {
                processutils.killallprocess();
            }
            if (command.StartsWith("process kill "))
            {
                processutils.killprocess();
            }

            // python commands
            if (command == "ironpython3")
            {
                commandusages.ironpythonusage();
            }
            if (command.StartsWith("ironpython3 path "))
            {
                python3commands.runfilepath();
            }
            else if (command.StartsWith("ironpython3 "))
            {
                python3commands.runfile();
            }

            // backbridge
            if (command == "acl_bb")
            {
                commandusages.backbridgeusage();
            }
            if (command == "acl_bb about")
            {
                commandusages.backbridgeabout();
            }
            if (command == "acl_bb start")
            {
                otherutils.acl_loader();
            }

            // config commands
            if (command == "config open")
            {
                configcommands.openconfig();
            }
            if (command == "config rewrite")
            {
                configcommands.rewriteconfig();
            }
            if (command == "config print")
            {
                configcommands.printconfig();
            }

            // debug
            if (command == "dbg_start")
            {
                debug.dbg_start();
            }
            
            // error messages
            if (command == "")
            {
                Console.WriteLine("Command syntax error");
                Console.WriteLine("no command entered (null)");
                CMD();
            }
            else
            {
                Console.WriteLine("Command syntax error");
                Console.WriteLine("the entered command '" + uncvcommand + "'");
                Console.WriteLine("isnt a valid UniCMD command");
                CMD();
            }
        }
    }
}