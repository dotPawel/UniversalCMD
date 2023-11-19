using System.Diagnostics;

namespace UniCMD
{
    static internal class Program
    {
        public static string Version = "9.0r";
        public static string Codename = "Andromeda";
        // r - release
        // rc - release candidate
        // d - debug

        public static string CurrentDir;
        public static string Command;
        public static string UserCommand;

        public static bool ReadyToExecute;

        public static void Main()
        {
            Startup.MainStartUp();
            StartupText();
        }
        static void StartupText()
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();
            Console.Title = "UniCMD (" + Version + ")";
            string cpu = "";
            cpu = Other.ReturnCPUName(cpu);

            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                Config.ParseStarttext();
            }
            else
            {
                Console.WriteLine("""
                     UniversalCMD / {0} / {1}
                  Host OS - {2}
                  Memory - {3} (KB)
                  CPU Name/model - {4}
                     For command index execute "help"
                """, Codename, Version, Environment.OSVersion, proc.PrivateMemorySize64, cpu);
            }     

            if (Startup.ConfigDict["checkForUpdates"])
            {
                Console.WriteLine();
                VersionManager.CompareVersionToLatest(false);
            }

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
                Config.ParsePrompttext();
            }
            else
            {
                if (Other.IsAdmin == true)
                {
                    Console.Write("(#) ");
                }

                if (CurrentDir == null)
                {
                    Console.Write("@ NULL >");
                }
                else
                {
                    Console.Write("@ {0} >", CurrentDir);
                }
            }

            try
            {
                UserCommand = Console.ReadLine();
                Command = UserCommand.ToLower();
            }
            catch { } // for preventing wierd crashes
            Console.WriteLine();

            Console.Title = "UniCMD (" + Version + ") - " + Command;

            CommandParser(Command);
        }
        public static void CommandParser(string command)
        {
            ReadyToExecute = false;
            // here the commands start

            // Behold no more! the if/else monster is gone.
            switch (command)
            {
                case string s when s.StartsWith("!!"): // uniscript code comments
                    break;

                case string s when s.StartsWith("[ptm-cmd] "):
                    Other.PTMCommand();
                    break;

                // Misc. commands
                case "exit":
                    Console.Write("Quitting UniCMD..");
                    Environment.Exit(0);
                    break;

                case "about":
                    CommandUsages.About();
                    break;

                case "clr":
                    Console.Clear();
                    break;

                case string s when s.StartsWith("echo "):
                    Other.Echo();
                    break;

                case "pause":
                    Console.ReadKey();
                    break;

                case string s when s.StartsWith("sleep "):
                    Other.Sleep();
                    break;

                case string s when s.StartsWith(".$"):
                    UniScript.ExecuteMacro();
                    break;

                case "help":
                    CommandUsages.CommandIndex();
                    break;


                // Directory commands
                case "sd clr":
                    FileMan.ClearSetDirectory();
                    break;

                case string s when s.StartsWith("sd "):
                    FileMan.SetDirectory();
                    break;

                case "dir lst":
                    FileMan.ListDir();
                    break;

                // dir make
                case "dir make":
                    CommandUsages.DirMakeUsage();
                    break;

                case string s when s.StartsWith("dir make "):
                    FileMan.CreateDir();
                    break;

                // dir del
                case "dir del":
                    CommandUsages.DirDeleteUsage();
                    break;

                case string s when s.StartsWith("dir del "):
                    FileMan.DeleteDir();
                    break;

                // dir cln
                case "dir cln":
                    CommandUsages.DirCloneUsage();
                    break;

                case string s when s.StartsWith("dir cln "):
                    FileMan.CloneDir();
                    break;

                // dir rnm
                case "dir rnm":
                    CommandUsages.DirRenameUsage();
                    break;

                case string s when s.StartsWith("dir rnm "):
                    FileMan.RenameDir();
                    break;



                // File commands

                // file make
                case "file make":
                    CommandUsages.FileCreateUsage();
                    break;

                case string s when s.StartsWith("file make "):
                    FileMan.CreateFile();
                    break;

                // file del
                case "file del":
                    CommandUsages.FileDeleteUsage();
                    break;

                case string s when s.StartsWith("file del "):
                    FileMan.DeleteFile();
                    break;

                // file rd
                case "file rd":
                    CommandUsages.FileReadUsage();
                    break;

                case string s when s.StartsWith("file rd "):
                    FileMan.ReadFile();
                    break;

                // file wrt
                case "file wrt":
                    CommandUsages.FileWriteUsage();
                    break;

                case string s when s.StartsWith("file wrt "):
                    FileMan.WriteFile();
                    break;

                // file wrtln
                case "file wrtln":
                    CommandUsages.FileWriteLineUsage();
                    break;

                case string s when s.StartsWith("file wrtln "):
                    FileMan.WriteLineFile();
                    break;

                // file clr
                case "file clr":
                    CommandUsages.FileClearUsage();
                    break;

                case string s when s.StartsWith("file clr "):
                    FileMan.ClearFile();
                    break; 

                // file cln
                case "file cln":
                    CommandUsages.FileCloneUsage();
                    break;

                case string s when s.StartsWith("file cln "):
                    FileMan.CloneFile();
                    break;

                // file rnm
                case "file rnm":
                    CommandUsages.FileRenameUsage();
                    break;

                case string s when s.StartsWith("file rnm "):
                    FileMan.RenameFile();
                    break;
 
                // file zip
                case "file zip":
                    CommandUsages.FileZipUsage();
                    break;

                case string s when s.StartsWith("file zip "):
                    FileMan.ZipFile();
                    break;

                // file zip
                case "file unzip":
                    CommandUsages.FileUnzipUsage();
                    break;

                case string s when s.StartsWith("file unzip "):
                    FileMan.UnzipFile();
                    break;


                // Process commands
                case "proc lst":
                    Process.ListProcess();
                    break;

                // proc run
                case "proc run":
                    CommandUsages.ProcessStartUsage();
                    break;

                case string s when s.StartsWith("proc run "):
                    Process.RunProcess();
                    break;

                // proc end
                case "proc end":
                    CommandUsages.ProcessKillUsage();
                    break;

                case "proc end /all":
                    Process.KillAllProcess();
                    break;

                case string s when s.StartsWith("proc end "):
                    Process.KillProcess();
                    break;


                // IronPython commands
                case "irpy":
                    CommandUsages.IronPythonUsage();
                    break;

                case string s when s.StartsWith("irpy "):
                    IronPython.RunFile();
                    break;


                // AeroCL backbridge commands
                case "acl_bb":
                    CommandUsages.BackbridgeUsage();
                    break;

                case "acl_bb about":
                    CommandUsages.BackbridgeAbout();
                    break;

                case "acl_bb start":
                    AeroCL_BB.acl_main();
                    break;


                // Config commands
                case "cfg rd":
                    Config.PrintCurrentConfig();
                    break;

                case string s when s.StartsWith("cfg set "):
                    Config.SetConfigEntry();
                    break;

                case "cfg wrt":
                    Config.ApplyConfig();
                    break;


                // StartText commands
                case "stxt":
                    CommandUsages.StarttextHelp();
                    break;

                case "stxt parse":
                    Config.ParseStarttext();
                    break;

                case "stxt make":
                    Config.CreateStarttext();
                    break;

                case "stxt open":
                    Config.OpenStarttext();
                    break;

                case "stxt wrt-template":
                    Config.WriteTemplateStarttext();
                    break;


                // PromptText commands
                case "ptxt":
                    CommandUsages.PrompttextHelp();
                    break;

                case "ptxt make":
                    Config.CreatePromptText();
                    break;

                case "ptxt open":
                    Config.OpenPromptText();
                    break;

                case "ptxt wrt-template":
                    Config.WriteTemplateStarttext();
                    break;


                // TextModules commands
                case "tmdl":
                    CommandUsages.TextModulesHelp();
                    break;

                case "tmdl example":
                    CommandUsages.TextModulesExample();
                    break;

                case "[ptm-cmd]":
                    CommandUsages.ParseCommandHelp();
                    break;

                
                // UniScript commands
                case "uniscript":
                    CommandUsages.UniScriptHelp();
                    break;

                case string s when s.StartsWith("uniscript "):
                    UniScript.Execute();
                    break;

                // UniScript user input commands
                case "usrin":
                    CommandUsages.UserInputUtilsHelp();
                    break;
                    
                case "usrin clr":
                    UserData.ClearUserInput();
                    break;

                case "usrin tolwr":
                    UserData.ToLowerUserInput();
                    break;

                case "usrin toupp":
                    UserData.ToUpperUserInput();
                    break;

                case "usrin rd":
                    UserData.ReadUserInput();
                    break;

                // usrin rdf
                case "usrin rdf":
                    CommandUsages.UserInputReadFileHelp();
                    break;

                case string s when s.StartsWith("usrin rdf "):
                    UserData.ReadFileUserInput();
                    break;

                // usrin set
                case "usrin set":
                    CommandUsages.UserInputSetHelp();
                    break;

                case string s when s.StartsWith("usrin set "):
                    UserData.SetUserInput();
                    break;

                // usrin repl
                case "usrin repl":
                    CommandUsages.UserInputReplaceHelp();
                    break;

                case string s when s.StartsWith("usrin repl "):
                    UserData.ReplaceUserInput();
                    break;


                // String dictionary
                case "dict":
                    CommandUsages.DictionaryHelp();
                    break;

                case "dict rd":
                    UserData.PrintDictionary();
                    break;

                // dict add
                case "dict add":
                    CommandUsages.DictionaryAddKeyHelp();
                    break;

                case string s when s.StartsWith("dict add "):
                    UserData.AddDictionaryKey();
                    break;

                // dict rem
                case "dict rem":
                    CommandUsages.DictionaryRemoveKeyHelp();
                    break;

                case string s when s.StartsWith("dict rem "):
                    UserData.RemoveDictionaryKey();
                    break;

                // dict rem
                case "dict set":
                    CommandUsages.DictionarySetKeyHelp();
                    break;

                case string s when s.StartsWith("dict set "):
                    UserData.SetDictionaryKey();
                    break;

                // dict clr
                case "dict clr":
                    UserData.ClearDictionary();
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

                // VersionManager commands
                case "vm":
                    CommandUsages.VersionManagerUsage();
                    break;

                case "vm lst":
                    VersionManager.ListAllReleases();
                    break;

                case "vm comp":
                    VersionManager.CompareVersionToLatest(true);
                    break;

                case "vm pull":
                    CommandUsages.VersionManagerPullUsage();
                    break;

                case string s when s.StartsWith("vm pull "):
                    VersionManager.PullRelease();
                    break;

                // Networking commands

                // net ping
                case "net ping":
                    CommandUsages.NetworkPingUsage();
                    break;

                case string s when s.StartsWith("net ping "):
                    Networking.Ping();
                    break;

                // net dload
                case "net dload":
                    CommandUsages.NetworkDownloadUsage();
                    break;

                case string s when s.StartsWith("net dload "):
                    Networking.Download();
                    break;

                // net dload
                case "net fc":
                    CommandUsages.NetworkFetchContentUsage();
                    break;

                case string s when s.StartsWith("net fc "):
                    Networking.FetchContents();
                    break;


                // Misc.

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

            ReadyToExecute = true;
            Prompt();

            // debug
            if (command == "dbg_start")
            {
                Debug.dbg_start();
            }          
        }
        public static void InvalidCommand()
        {
            Console.WriteLine("Command syntax error");
            Console.WriteLine("the entered command '" + UserCommand + "'");
            Console.WriteLine("isnt a valid UniCMD command");
            Prompt();
        }
    }
}