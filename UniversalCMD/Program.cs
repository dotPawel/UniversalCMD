using System.Diagnostics;

namespace UniCMD
{
    static internal class Program
    {
        public static string version = "v5.1r";
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
            string cpu = "";
            cpu = OtherUtils.ReturnCPUName(cpu);

            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                ConfigCommands.ParseStarttext();
            }

            Console.WriteLine("""
                     UniversalCMD / Neptune / {0}
                  Host OS - {1}
                  Memory - {2} (KB)
                  CPU Name/model - {3}
                     For command index execute "help"
                """, version, Environment.OSVersion, proc.PrivateMemorySize64, cpu);

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

            // Behold no more! the if/else monster is gone.
            switch (command)
            {
                // Misc. commands
                case "exit":
                    Console.Write("Quitting UniCMD..");
                    Environment.Exit(0);
                    break;

                case "about":
                    CommandUsages.About();
                    break;

                case "clr":
                    OtherUtils.ClearConsole();
                    break;

                case string s when s.StartsWith("echo /ptm "):
                    OtherUtils.EchoPTM();
                    break;

                case string s when s.StartsWith("echo "):
                    OtherUtils.Echo();
                    break;

                case "pause":
                    Console.ReadKey();
                    break;

                case string s when s.StartsWith("sleep "):
                    OtherUtils.Sleep();
                    break;

                case string s when s.StartsWith(".$"):
                    UniScript.ExecuteMacro();
                    break;

                case "help":
                    CommandUsages.CommandIndex();
                    break;


                // Directory commands
                case "sd clr":
                    FileUtils.ClearSetDirectory();
                    break;

                case string s when s.StartsWith("sd "):
                    FileUtils.SetDirectory();
                    break;

                case "dir lst":
                    FileUtils.ListDir();
                    break;

                // dir make
                case "dir make":
                    CommandUsages.DirMakeUsage();
                    break;

                case string s when s.StartsWith("dir make /p "):
                    FileUtils.CreateDirPath();
                    break;

                case string s when s.StartsWith("dir make "):
                    FileUtils.CreateDir();
                    break;

                // dir del
                case "dir del":
                    CommandUsages.DirDeleteUsage();
                    break;

                case string s when s.StartsWith("dir del /p "):
                    FileUtils.DeleteDirPath();
                    break;

                case string s when s.StartsWith("dir del "):
                    FileUtils.DeleteDir();
                    break;

                // dir cln
                case "dir cln":
                    CommandUsages.DirCloneUsage();
                    break;

                case string s when s.StartsWith("dir cln /p "):
                    FileUtils.CloneDirPath();
                    break;

                case string s when s.StartsWith("dir cln "):
                    FileUtils.CloneDir();
                    break;

                // dir rnm
                case "dir rnm":
                    CommandUsages.DirRenameUsage();
                    break;

                case string s when s.StartsWith("dir rnm /p "):
                    FileUtils.RenameDirPath();
                    break;

                case string s when s.StartsWith("dir rnm "):
                    FileUtils.RenameDir();
                    break;



                // File commands

                // file make
                case "file make":
                    CommandUsages.FileCreateUsage();
                    break;

                case string s when s.StartsWith("file make /p "):
                    FileUtils.CreateFilePath();
                    break;

                case string s when s.StartsWith("file make "):
                    FileUtils.CreateFile();
                    break;

                // file del
                case "file del":
                    CommandUsages.FileDeleteUsage();
                    break;

                case string s when s.StartsWith("file del /p "):
                    FileUtils.DeleteFilePath();
                    break;

                case string s when s.StartsWith("file del "):
                    FileUtils.DeleteFile();
                    break;

                // file rd
                case "file rd":
                    CommandUsages.FileReadUsage();
                    break;

                case string s when s.StartsWith("file rd /p "):
                    FileUtils.ReadFilePath();
                    break;

                case string s when s.StartsWith("file rd "):
                    FileUtils.ReadFile();
                    break;

                // file wrt
                case "file wrt":
                    CommandUsages.FileWriteUsage();
                    break;

                case string s when s.StartsWith("file wrt /p "):
                    FileUtils.WriteFilePath();
                    break;

                case string s when s.StartsWith("file wrt "):
                    FileUtils.WriteFile();
                    break;

                // file clr
                case "file clr":
                    CommandUsages.FileClearUsage();
                    break;

                case string s when s.StartsWith("file clr /p "):
                    FileUtils.ClearFilePath();
                    break;

                case string s when s.StartsWith("file clr "):
                    FileUtils.ClearFile();
                    break; 

                // file cln
                case "file cln":
                    CommandUsages.FileCloneUsage();
                    break;

                case string s when s.StartsWith("file cln /p "):
                    FileUtils.CloneFilePath();
                    break;

                case string s when s.StartsWith("file cln "):
                    FileUtils.CloneFile();
                    break;

                // file rnm
                case "file rnm":
                    CommandUsages.FileRenameUsage();
                    break;

                case string s when s.StartsWith("file rnm /p "):
                    FileUtils.RenameFile();
                    break;

                case string s when s.StartsWith("file rnm "):
                    FileUtils.RenameFile();
                    break;
 
                // file zip
                case "file zip":
                    CommandUsages.FileZipUsage();
                    break;

                case string s when s.StartsWith("file zip /p "):
                    FileUtils.ZipFilePath();
                    break;

                case string s when s.StartsWith("file zip "):
                    FileUtils.ZipFile();
                    break;

                // file zip
                case "file unzip":
                    CommandUsages.FileUnzipUsage();
                    break;

                case string s when s.StartsWith("file unzip /p "):
                    FileUtils.UnzipFilePath();
                    break;

                case string s when s.StartsWith("file unzip "):
                    FileUtils.UnzipFile();
                    break;


                // Process commands
                case "proc lst":
                    ProcessUtils.ListProcess();
                    break;

                // proc run
                case "proc run":
                    CommandUsages.FileClearUsage();
                    break;

                case string s when s.StartsWith("proc run /p "):
                    ProcessUtils.RunProcessPath();
                    break;

                case string s when s.StartsWith("proc run "):
                    ProcessUtils.RunProcess();
                    break;

                // proc end
                case "proc end":
                    CommandUsages.ProcessKillUsage();
                    break;

                case string s when s.StartsWith("proc end /all "):
                    ProcessUtils.KillAllProcess();
                    break;

                case string s when s.StartsWith("proc end "):
                    ProcessUtils.KillProcess();
                    break;


                // IronPython commands
                case "ironpython":
                    CommandUsages.IronPythonUsage();
                    break;

                case string s when s.StartsWith("ironpython /p "):
                    IronPythonCommands.RunFilePath();
                    break;

                case string s when s.StartsWith("ironpython "):
                    IronPythonCommands.RunFile();
                    break;


                // AeroCL backbridge commands
                case "acl_bb":
                    CommandUsages.BackbridgeUsage();
                    break;

                case "acl_bb about":
                    CommandUsages.BackbridgeAbout();
                    break;

                case "acl_bb start":
                    OtherUtils.AeroCL_Loader();
                    break;


                // Config commands
                case "config open":
                    ConfigCommands.OpenConfig();
                    break;

                case "config rewrite":
                    ConfigCommands.RewriteConfig();
                    break;

                case "config print":
                    ConfigCommands.PrintConfig();
                    break;


                // StartText commands
                case "starttext":
                    CommandUsages.StarttextHelp();
                    break;

                case "starttext parse":
                    ConfigCommands.ParseStarttext();
                    break;

                case "starttext create":
                    ConfigCommands.CreateStarttext();
                    break;

                case "starttext open":
                    ConfigCommands.OpenStarttext();
                    break;

                case "starttext write-template":
                    ConfigCommands.WriteTemplateStarttext();
                    break;


                // PromptText commands
                case "prompttext":
                    CommandUsages.StarttextHelp();
                    break;

                case "prompttext create":
                    ConfigCommands.CreatePromptText();
                    break;

                case "prompttext open":
                    ConfigCommands.OpenPromptText();
                    break;

                case "prompttext write-template":
                    ConfigCommands.WriteTemplateStarttext();
                    break;


                // TextModules commands
                case "textmodules":
                    CommandUsages.TextModulesHelp();
                    break;

                case "textmodules example":
                    CommandUsages.TextModulesExample();
                    break;

                case string s when s.StartsWith("[ptm-cmd] "):
                    OtherUtils.PTMCommand();
                    break;

                case "[ptm-cmd]":
                    CommandUsages.ParseCommandHelp();
                    break;
                
                // UniScript commands
                case "uniscript":
                    CommandUsages.UniScriptHelp();
                    break;

                case string s when s.StartsWith("uniscript /p "):
                    UniScript.ExecutePath();
                    break;

                case string s when s.StartsWith("uniscript "):
                    UniScript.Execute();
                    break;

                // UniPKG commands
                case "unipkg":
                    CommandUsages.UniPKGHelp();
                    break;

                case "unipkg /inst":
                    CommandUsages.UniPKGInstallUsage();
                    break;

                case string s when s.StartsWith("unipkg /inst "):
                    UniPKG.InstallOnlinePackage();
                    break;

                case "unipkg /dpkg":
                    CommandUsages.UniPKGDepackageUsage();
                    break;

                case string s when s.StartsWith("unipkg /dpkg "):
                    UniPKG.Depackage();
                    break;

                case "unipkg /foinfo":
                    CommandUsages.UniPKGFetchOnlineInfoUsage();
                    break;

                case string s when s.StartsWith("unipkg /foinfo "):
                    UniPKG.FetchOnlineInfo();
                    break;

                case "unipkg /finfo":
                    CommandUsages.UniPKGFetchInfoUsage();
                    break;

                case string s when s.StartsWith("unipkg /finfo "):
                    UniPKG.FetchInfo();
                    break;

                case "unipkg /uinst":
                    CommandUsages.UniPKGUninstallUsage();
                    break;

                case string s when s.StartsWith("unipkg /uinst "):
                    UniPKG.Uninstall();
                    break;

                case "unipkg /list":
                    UniPKG.ListInstalledPackages();
                    break;

                // Networking commands

                // net ping
                case "net ping":
                    CommandUsages.NetworkPingUsage();
                    break;

                case string s when s.StartsWith("net ping "):
                    Networking.Ping();
                    break;




                case "dbg_start":
                    Debug.dbg_start();
                    break;

                // Error messages
                case "":
                    Console.WriteLine("Command syntax error");
                    Console.WriteLine("no command entered (null)");
                    Prompt();
                    break;

                default:
                    InvalidCommand();
                    Prompt();
                    break;
            }
            Prompt();

            // debug
            if (command == "dbg_start")
            {
                Debug.dbg_start();
            }          
        }
        public static void InvalidCommand()
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