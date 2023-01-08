using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UniCMD
{
    static internal class Program
    {
        public static string version = "v2.1r";
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

            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                configcommands.ParseStarttext();
            }

            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine(" UniversalCMD / Rainman / Build " + version + "\n");
            Console.WriteLine("  Host OS");
            Console.WriteLine("   └ " + Environment.OSVersion);
            Console.WriteLine("  Memory");
            Console.WriteLine("   └ " + proc.PrivateMemorySize64 + " (KB)");
            Console.WriteLine("  CPU Name/Model");
            otherutils.cpuname();
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine("for command index run 'help'");
            Console.WriteLine("in order to set current run 'set dir'");

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
            catch // for preventing wierd crashes
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
            if (command == "clr")
            {
                otherutils.clearconsole();
            }
            if (command.StartsWith("echo "))
            {
                otherutils.echo();
            }

            if (command == "help")
            {
                commandusages.help();
            }

            // directory commands
            if (command == "sd")
            {
                fileutils.setdir();
            }
            if (command == "sd clr")
            {
                fileutils.clearsetdir();
            }
            if (command == "dir lst")
            {
                fileutils.listdir();
            }

            if (command == "dir make")
            {
                commandusages.dirmakeusage();
            }
            if (command.StartsWith("dir make /p "))
            {
                fileutils.createdirpath();
            }
            else if (command.StartsWith("dir make "))
            {
                fileutils.createdir();
            }

            if (command == "dir del")
            {
                commandusages.deletedirusage();
            }
            if (command.StartsWith("dir del /p "))
            {
                fileutils.deletedirpath();
            }
            else if (command.StartsWith("dir del "))
            {
                fileutils.deletedir();
            }

            if (command == "dir cln")
            {
                commandusages.clonedirusage();
            }
            if (command.StartsWith("dir cln /p "))
            {
                fileutils.clonedirpath();
            }
            else if (command.StartsWith("dir cln "))
            {
                fileutils.clonedir();
            }

            if (command == "dir rnm")
            {
                commandusages.renamedirusage();
            }
            if (command.StartsWith("dir rnm /p "))
            {
                fileutils.renamedirpath();
            }
            else if (command.StartsWith("dir rnm "))
            {
                fileutils.renamedir();
            }


            // file commands
            if (command == "file make")
            {
                commandusages.createfileusage();
            }
            if (command.StartsWith("file make /p "))
            {
                fileutils.createfilepath();
            }
            else if (command.StartsWith("file make "))
            {
                fileutils.makefile();
            }

            if (command == "file del")
            {
                commandusages.deletefileusage();
            }
            if (command.StartsWith("file del /p "))
            {
                fileutils.deletefilepath();
            }
            else if (command.StartsWith("file del "))
            {
                fileutils.deletefile();
            }

            if (command == "file rd")
            {
                commandusages.readfileusage();
            }
            if (command.StartsWith("file rd /p "))
            {
                fileutils.readfilepath();
            }
            else if (command.StartsWith("file rd "))
            {
                fileutils.readfile();
            }
            
            if (command == "file wrt")
            {
                commandusages.writefileusage();
            }
            if (command.StartsWith("file wrt /p "))
            {
                fileutils.writefilepath();
            }
            else if (command.StartsWith("file wrt "))
            {
                fileutils.writefile();
            }

            if (command == "file clr")
            {
                commandusages.clearfileusage();
            }
            if (command.StartsWith("file clr /p "))
            {
                fileutils.clearfilepath();
            }
            else if (command.StartsWith("file clr "))
            {
                fileutils.clearfile();
            }

            if (command == "file cln")
            {
                commandusages.clonefileusage();
            }
            if (command.StartsWith("file cln /p "))
            {
                fileutils.clonefilepath();
            }
            else if (command.StartsWith("file cln "))
            {
                fileutils.clonefile();
            }

            if (command == "file rnm")
            {
                commandusages.renamefileusage();
            }
            if (command.StartsWith("file rnm /p "))
            {
                fileutils.renamefilepath();
            }
            else if (command.StartsWith("file rnm "))
            {
                fileutils.renamefile();
            }

            // process commands
            if (command == "proc lst")
            {
                processutils.listprocess();
            }

            if (command == "proc run")
            {
                commandusages.processstartusage();
            }
            if (command.StartsWith("proc run /p "))
            {
                processutils.processrunpath();
            }
            else if (command.StartsWith("proc run "))
            {
                processutils.processrun();
            }

            if (command == "proc end")
            {
                commandusages.processkillusage();
            }
            if (command == "proc end /all")
            {
                processutils.killallprocess();
            }
            if (command.StartsWith("proc end "))
            {
                processutils.killprocess();
            }

            // python commands
            if (command == "ironpython")
            {
                commandusages.ironpythonusage();
            }
            if (command.StartsWith("ironpython /p "))
            {
                python3commands.runfilepath();
            }
            else if (command.StartsWith("ironpython "))
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

            // starttext commands
            if (command == "starttext help")
            {
                commandusages.starttxthelp();
            }
            if (command == "starttext parse")
            {
                configcommands.ParseStarttext();
            }
            if (command == "starttext create")
            {
                configcommands.starttxtcreate();
            }
            if (command == "starttext open")
            {
                configcommands.starttxtopen();
            }
            if (command == "starttext write-template")
            {
                configcommands.starttxtwritetemplate();
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