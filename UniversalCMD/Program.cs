using System.Diagnostics;

namespace UniCMD
{
    static internal class Program
    {
        public static string version = "v3.1r";
        // r - release
        // rc - release candidate
        // d - debug

        public static string currentdir;
        public static string command;
        public static string uncvcommand;
        internal static object filename;

        public static void Main()
        {
            Startup.MainStartUp();
            StartupText();
        }
        static void StartupText()
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
            otherutils.ReturnCPUName();
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine("for command index run 'help'");
            Console.WriteLine("in order to set current directory run 'sd'");

            Prompt();
        }
        public static void Prompt()
        {
            if (uniscript.UniScriptExecuting == true)
            {
                return;
            }

            Console.WriteLine();

            if (File.Exists(@"UniCMD.data\prompttext.unicmd"))
            {
                configcommands.ParsePrompttext();
            }
            else
            {
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
                    Console.Write("@ {0} >", currentdir);
                }
            }

            try
            {
                uncvcommand = Console.ReadLine();
                command = uncvcommand.ToLower();
            }
            catch { } // for preventing wierd crashes
            Console.WriteLine();

            Console.Title = "UniCMD (" + version + ") - " + command;

            Command(command);
        }
        public static void Command(string command)
        {
            // here the commands start

            // Behold! the power of the great if/else monster
            // how did it even get this bad

            // misc stuff
            if (command == "exit")
            {
                Console.Write("Quitting UniCMD...");
                Environment.Exit(0);
            }
            if (command == "about")
            {
                commandusages.About();
            }
            if (command == "clr")
            {
                otherutils.ClearConsole();
            }
            if (command.StartsWith("echo /ptm "))
            {
                otherutils.EchoPTM();
            }
            else if (command.StartsWith("echo "))
            {
                otherutils.Echo();
            }
            if (command == "pause")
            {
                Console.ReadKey();
                Prompt();
            }
            if (command.StartsWith("sleep "))
            {
                otherutils.Sleep();
            }


            if (command.StartsWith(".$"))
            {
                uniscript.ExecuteMacro();
            }

            if (command == "help")
            {
                commandusages.CommandIndex();
            }

            // directory commands
            if (command == "sd")
            {
                fileutils.SetDirectory();
            }
            if (command == "sd clr")
            {
                fileutils.ClearSetDirectory();
            }
            if (command == "dir lst")
            {
                fileutils.ListDir();
            }

            if (command == "dir make")
            {
                commandusages.DirMakeUsage();
            }
            if (command.StartsWith("dir make /p "))
            {
                fileutils.CreateDirPath();
            }
            else if (command.StartsWith("dir make "))
            {
                fileutils.CreateDir();
            }

            if (command == "dir del")
            {
                commandusages.DirDeleteUsage();
            }
            if (command.StartsWith("dir del /p "))
            {
                fileutils.DeleteDirPath();
            }
            else if (command.StartsWith("dir del "))
            {
                fileutils.DeleteDir();
            }

            if (command == "dir cln")
            {
                commandusages.DirCloneUsage();
            }
            if (command.StartsWith("dir cln /p "))
            {
                fileutils.CloneDirPath();
            }
            else if (command.StartsWith("dir cln "))
            {
                fileutils.CloneDir();
            }

            if (command == "dir rnm")
            {
                commandusages.DirRenameUsage();
            }
            if (command.StartsWith("dir rnm /p "))
            {
                fileutils.RenameDirPath();
            }
            else if (command.StartsWith("dir rnm "))
            {
                fileutils.RenameDir();
            }


            // file commands
            if (command == "file make")
            {
                commandusages.FileCreateUsage();
            }
            if (command.StartsWith("file make /p "))
            {
                fileutils.CreateFilePath();
            }
            else if (command.StartsWith("file make "))
            {
                fileutils.CreateFile();
            }

            if (command == "file del")
            {
                commandusages.FileDeleteUsage();
            }
            if (command.StartsWith("file del /p "))
            {
                fileutils.DeleteDirPath();
            }
            else if (command.StartsWith("file del "))
            {
                fileutils.DeleteFile();
            }

            if (command == "file rd")
            {
                commandusages.FileReadUsage();
            }
            if (command.StartsWith("file rd /p "))
            {
                fileutils.ReadFilePath();
            }
            else if (command.StartsWith("file rd "))
            {
                fileutils.ReadFile();
            }

            if (command == "file wrt")
            {
                commandusages.FileWriteUsage();
            }
            if (command.StartsWith("file wrt /p "))
            {
                fileutils.WriteFilePath();
            }
            else if (command.StartsWith("file wrt "))
            {
                fileutils.WriteFile();
            }

            if (command == "file clr")
            {
                commandusages.FileClearUsage();
            }
            if (command.StartsWith("file clr /p "))
            {
                fileutils.ClearFilePath();
            }
            else if (command.StartsWith("file clr "))
            {
                fileutils.ClearFile();
            }

            if (command == "file cln")
            {
                commandusages.FileCloneUsage();
            }
            if (command.StartsWith("file cln /p "))
            {
                fileutils.CloneFilePath();
            }
            else if (command.StartsWith("file cln "))
            {
                fileutils.CloneFile();
            }

            if (command == "file rnm")
            {
                commandusages.FileRenameUsage();
            }
            if (command.StartsWith("file rnm /p "))
            {
                fileutils.RenameFilePath();
            }
            else if (command.StartsWith("file rnm "))
            {
                fileutils.RenameFile();
            }

            // process commands
            if (command == "proc lst")
            {
                processutils.ListProcess();
            }

            if (command == "proc run")
            {
                commandusages.ProcessStartUsage();
            }
            if (command.StartsWith("proc run /p "))
            {
                processutils.RunProcessPath();
            }
            else if (command.StartsWith("proc run "))
            {
                processutils.RunProcess();
            }

            if (command == "proc end")
            {
                commandusages.ProcessKillUsage();
            }
            if (command == "proc end /all")
            {
                processutils.KillAllProcess();
            }
            if (command.StartsWith("proc end "))
            {
                processutils.KillProcess();
            }

            // python commands
            if (command == "ironpython")
            {
                commandusages.IronPythonUsage();
            }
            if (command.StartsWith("ironpython /p "))
            {
                python3commands.RunFilePath();
            }
            else if (command.StartsWith("ironpython "))
            {
                python3commands.RunFile();
            }

            // backbridge
            if (command == "acl_bb")
            {
                commandusages.BackbridgeUsage();
            }
            if (command == "acl_bb about")
            {
                commandusages.BackbridgeAbout();
            }
            if (command == "acl_bb start")
            {
                otherutils.AeroCL_Loader();
            }

            // config commands
            if (command == "config open")
            {
                configcommands.OpenConfig();
            }
            if (command == "config rewrite")
            {
                configcommands.RewriteConfig();
            }
            if (command == "config print")
            {
                configcommands.PrintConfig();
            }

            // starttext commands
            if (command == "starttext")
            {
                commandusages.StarttextHelp();
            }
            if (command == "starttext parse")
            {
                configcommands.ParseStarttext();
            }
            if (command == "starttext create")
            {
                configcommands.CreateStarttext();
            }
            if (command == "starttext open")
            {
                configcommands.OpenStarttext();
            }
            if (command == "starttext write-template")
            {
                configcommands.WriteTemplateStarttext();
            }

            // prompttext commands
            if (command == "prompttext")
            {
                commandusages.PrompttextHelp();
            }
            if (command == "prompttext create")
            {
                configcommands.CreatePromptText();
            }
            if (command == "prompttext open")
            {
                configcommands.OpenPromptText();
            }
            if (command == "prompttext write-template")
            {
                configcommands.WritePromptTextTemplate();
            }

            // textmodules
            if (command == "textmodules")
            {
                commandusages.TextModulesHelp();
            }
            if (command == "textmodules example")
            {
                commandusages.TextModulesExample();
            }

            // uniscript
            if (command == "uniscript")
            {
                commandusages.UniScriptHelp();
            }
            if (command.StartsWith("uniscript /p "))
            {
                uniscript.ExecutePath();
            }
            else if (command.StartsWith("uniscript "))
            {
                uniscript.Execute();
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
                Prompt();
            } 
            else
            {
                if (uniscript.UniScriptExecuting == true)
                {
                    return;
                }
                Console.WriteLine("Command syntax error");
                Console.WriteLine("the entered command '" + uncvcommand + "'");
                Console.WriteLine("isnt a valid UniCMD command");
                Prompt();
            }
        }
    }
}