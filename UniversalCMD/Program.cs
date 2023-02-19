using System.Diagnostics;

namespace UniCMD
{
    static internal class Program
    {
        public static string version = "v3.2r";
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
                ConfigCommands.ParseStarttext();
            }

            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine(" UniversalCMD / Rainman / Build " + version + "\n");
            Console.WriteLine("  Host OS");
            Console.WriteLine("   └ " + Environment.OSVersion);
            Console.WriteLine("  Memory");
            Console.WriteLine("   └ " + proc.PrivateMemorySize64 + " (KB)");
            Console.WriteLine("  CPU Name/Model");
            OtherUtils.ReturnCPUName();
            Console.WriteLine(" --------------------------------------------------");
            Console.WriteLine("for command index run 'help'");
            Console.WriteLine("in order to set current directory run 'sd'");

            Prompt();
        }
        public static void Prompt()
        {
            if (UniScript.UniScriptExecuting == true)
            {
                return;
            }

            Console.WriteLine();

            if (File.Exists(@"UniCMD.data\prompttext.unicmd"))
            {
                ConfigCommands.ParsePrompttext();
            }
            else
            {
                if (OtherUtils.runningAsAdmin == true)
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
                CommandUsages.About();
            }
            if (command == "clr")
            {
                OtherUtils.ClearConsole();
            }
            if (command.StartsWith("echo /ptm "))
            {
                OtherUtils.EchoPTM();
            }
            else if (command.StartsWith("echo "))
            {
                OtherUtils.Echo();
            }
            if (command == "pause")
            {
                Console.ReadKey();
                Prompt();
            }
            if (command.StartsWith("sleep "))
            {
                OtherUtils.Sleep();
            }

            if (command.StartsWith(".$"))
            {
                UniScript.ExecuteMacro();
            }

            if (command == "help")
            {
                CommandUsages.CommandIndex();
            }

            // directory commands
            if (command == "sd")
            {
                FileUtils.SetDirectory();
            }
            if (command == "sd clr")
            {
                FileUtils.ClearSetDirectory();
            }
            if (command == "dir lst")
            {
                FileUtils.ListDir();
            }

            if (command == "dir make")
            {
                CommandUsages.DirMakeUsage();
            }
            if (command.StartsWith("dir make /p "))
            {
                FileUtils.CreateDirPath();
            }
            else if (command.StartsWith("dir make "))
            {
                FileUtils.CreateDir();
            }

            if (command == "dir del")
            {
                CommandUsages.DirDeleteUsage();
            }
            if (command.StartsWith("dir del /p "))
            {
                FileUtils.DeleteDirPath();
            }
            else if (command.StartsWith("dir del "))
            {
                FileUtils.DeleteDir();
            }

            if (command == "dir cln")
            {
                CommandUsages.DirCloneUsage();
            }
            if (command.StartsWith("dir cln /p "))
            {
                FileUtils.CloneDirPath();
            }
            else if (command.StartsWith("dir cln "))
            {
                FileUtils.CloneDir();
            }

            if (command == "dir rnm")
            {
                CommandUsages.DirRenameUsage();
            }
            if (command.StartsWith("dir rnm /p "))
            {
                FileUtils.RenameDirPath();
            }
            else if (command.StartsWith("dir rnm "))
            {
                FileUtils.RenameDir();
            }


            // file commands
            if (command == "file make")
            {
                CommandUsages.FileCreateUsage();
            }
            if (command.StartsWith("file make /p "))
            {
                FileUtils.CreateFilePath();
            }
            else if (command.StartsWith("file make "))
            {
                FileUtils.CreateFile();
            }

            if (command == "file del")
            {
                CommandUsages.FileDeleteUsage();
            }
            if (command.StartsWith("file del /p "))
            {
                FileUtils.DeleteDirPath();
            }
            else if (command.StartsWith("file del "))
            {
                FileUtils.DeleteFile();
            }

            if (command == "file rd")
            {
                CommandUsages.FileReadUsage();
            }
            if (command.StartsWith("file rd /p "))
            {
                FileUtils.ReadFilePath();
            }
            else if (command.StartsWith("file rd "))
            {
                FileUtils.ReadFile();
            }

            if (command == "file wrt")
            {
                CommandUsages.FileWriteUsage();
            }
            if (command.StartsWith("file wrt /p "))
            {
                FileUtils.WriteFilePath();
            }
            else if (command.StartsWith("file wrt "))
            {
                FileUtils.WriteFile();
            }

            if (command == "file clr")
            {
                CommandUsages.FileClearUsage();
            }
            if (command.StartsWith("file clr /p "))
            {
                FileUtils.ClearFilePath();
            }
            else if (command.StartsWith("file clr "))
            {
                FileUtils.ClearFile();
            }

            if (command == "file cln")
            {
                CommandUsages.FileCloneUsage();
            }
            if (command.StartsWith("file cln /p "))
            {
                FileUtils.CloneFilePath();
            }
            else if (command.StartsWith("file cln "))
            {
                FileUtils.CloneFile();
            }

            if (command == "file rnm")
            {
                CommandUsages.FileRenameUsage();
            }
            if (command.StartsWith("file rnm /p "))
            {
                FileUtils.RenameFilePath();
            }
            else if (command.StartsWith("file rnm "))
            {
                FileUtils.RenameFile();
            }

            // process commands
            if (command == "proc lst")
            {
                ProcessUtils.ListProcess();
            }

            if (command == "proc run")
            {
                CommandUsages.ProcessStartUsage();
            }
            if (command.StartsWith("proc run /p "))
            {
                ProcessUtils.RunProcessPath();
            }
            else if (command.StartsWith("proc run "))
            {
                ProcessUtils.RunProcess();
            }

            if (command == "proc end")
            {
                CommandUsages.ProcessKillUsage();
            }
            if (command == "proc end /all")
            {
                ProcessUtils.KillAllProcess();
            }
            if (command.StartsWith("proc end "))
            {
                ProcessUtils.KillProcess();
            }

            // python commands
            if (command == "ironpython")
            {
                CommandUsages.IronPythonUsage();
            }
            if (command.StartsWith("ironpython /p "))
            {
                IronPythonCommands.RunFilePath();
            }
            else if (command.StartsWith("ironpython "))
            {
                IronPythonCommands.RunFile();
            }

            // backbridge
            if (command == "acl_bb")
            {
                CommandUsages.BackbridgeUsage();
            }
            if (command == "acl_bb about")
            {
                CommandUsages.BackbridgeAbout();
            }
            if (command == "acl_bb start")
            {
                OtherUtils.AeroCL_Loader();
            }

            // config commands
            if (command == "config open")
            {
                ConfigCommands.OpenConfig();
            }
            if (command == "config rewrite")
            {
                ConfigCommands.RewriteConfig();
            }
            if (command == "config print")
            {
                ConfigCommands.PrintConfig();
            }

            // starttext commands
            if (command == "starttext")
            {
                CommandUsages.StarttextHelp();
            }
            if (command == "starttext parse")
            {
                ConfigCommands.ParseStarttext();
            }
            if (command == "starttext create")
            {
                ConfigCommands.CreateStarttext();
            }
            if (command == "starttext open")
            {
                ConfigCommands.OpenStarttext();
            }
            if (command == "starttext write-template")
            {
                ConfigCommands.WriteTemplateStarttext();
            }

            // prompttext commands
            if (command == "prompttext")
            {
                CommandUsages.PrompttextHelp();
            }
            if (command == "prompttext create")
            {
                ConfigCommands.CreatePromptText();
            }
            if (command == "prompttext open")
            {
                ConfigCommands.OpenPromptText();
            }
            if (command == "prompttext write-template")
            {
                ConfigCommands.WritePromptTextTemplate();
            }

            // textmodules
            if (command == "textmodules")
            {
                CommandUsages.TextModulesHelp();
            }
            if (command == "textmodules example")
            {
                CommandUsages.TextModulesExample();
            }
            if (command == "[ptm-cmd]")
            {

            }
            if (command.StartsWith("[ptm-cmd] "))
            {
                OtherUtils.PTMCommand();
            }

            // uniscript
            if (command == "uniscript")
            {
                CommandUsages.UniScriptHelp();
            }
            if (command.StartsWith("uniscript /p "))
            {
                UniScript.ExecutePath();
            }
            else if (command.StartsWith("uniscript "))
            {
                UniScript.Execute();
            }

            // debug
            if (command == "dbg_start")
            {
                Debug.dbg_start();
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
                if (UniScript.UniScriptExecuting == true)
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