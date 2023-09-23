using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class CommandUsages
    {
        public static void About()
        {
            Console.WriteLine("     UniversalCMD / Codename {0} / Build {1}", Program.Codename, Program.Version);
            Console.WriteLine("  a better AeroCL & a easy command line solution");

            
        }
        public static void CommandIndex()
        {
            Console.WriteLine("""
                   Command Index

                 File Management
                  ╚File Make
                   ╚File Make {name}
                   ╚File Make /P {path}
                  ╚File Delete
                   ╚File Del {name}
                   ╚File Del /P {path}
                  ╚File Read
                   ╚File Rd {name}
                   ╚File Rd /P {path}
                  ╚File Write
                   ╚File Wrt {name}
                   ╚File Wrt /P {path}
                  ╚File WriteLine
                   ╚File WrtLn {name} /s {user string}
                   ╚File WrtLn /P {path} /s {user string}
                  ╚File Clear
                   ╚File Clr {name}
                   ╚File Clr /P {path}
                  ╚File Clone
                   ╚File Cln {name}
                   ╚File Cln /P {path}
                  ╚File Rename
                   ╚File Rnm {name} /name {new name}
                   ╚File Rnm /P {path} /name {new name}
                  ╚File Zip
                   ╚File Zip {name}
                   ╚File Zip /P {path}
                  ╚File Unzip
                   ╚File Unzip {name}
                   ╚File Unzip /P {path}
                
                 Directory Management
                  ╚Set/clear directory
                   ╚SD
                   ╚SD Clr
                  ╚Directory Make
                   ╚Dir Make {name}
                   ╚Dir Make /P {path}
                  ╚Directory Delete
                   ╚Dir Del {name}
                   ╚Dir Del /P {path}
                  ╚Dir Cln
                   ╚Dir Cln {name}
                   ╚Dir Cln /P {path}
                  ╚Dir Rnm
                   ╚Dir Rnm {name} /name {new_name}
                   ╚Dir Rnm /P {path} /name {new_name}
                
                 Process Commands
                  ╚Process Run
                   ╚Proc Run {name}
                   ╚Proc Run /P {path}
                  ╚Process End
                   ╚Proc End {name}
                   ╚Proc End /All
                  ╚Process List
                   ╚Proc Lst

                 IronPython Execution
                  ╚Irpy {name}
                  ╚Irpy /P {path}

                 UniScript
                  ╚UniScript {name} /in {optional user argument}
                  ╚UniScript /P {path} /in {optional user argument}
                  ╚User Input
                    ╚UserInput Replace
                     ╚UsrIn Repl {string 1} /in {string 2}
                    ╚UserInput Read
                     ╚UsrIn Rd
                    ╚UserInput ReadFile
                     ╚UsrIn RdF {name}
                     ╚UsrIn RdF /p {path}
                    ╚UserInput ToUpper/ToLower
                     ╚UsrIn ToUpp
                     ╚UsrIn ToLwr
                    ╚UsrIn Clr
                    ╚UsrIn Set {string}

                 UniPKG
                  ╚Online
                   ╚unipkg /inst {package name}
                   ╚unipkg /foinfo {package name}
                  ╚Local
                   ╚unipkg /dpkg {package name}
                   ╚unipkg /finfo {package name}
                   ╚unipkg /uinst {package name}
                   ╚unipkg /list

                 Version Manager
                  ╚List all releases
                   ╚VM Lst
                  ╚Pull release by ID
                   ╚VM Pull {version id}
                   ╚VM Pull Latest
                  ╚Compare local to latest
                   ╚VM Comp

                 Networking
                  ╚Network Ping
                   ╚Net Ping {ip adress}
                  ╚Network Download
                   ╚Net Dload {file url}
                   ╚Net Dload {file url} /p {path}
                  ╚Network FetchContents
                   ╚Net Fc {url}
               
                 Other
                  ╚Clr
                  ╚About
                  ╚Exit
                  ╚Echo
                   ╚Echo
                   ╚Echo /PTM
                  ╚Sleep {milliseconds}

                 User preferences & customization
                  ╚Customization
                    ╚StartText
                     ╚STXT Open
                     ╚STXT Make
                     ╚STXT Parse
                     ╚STXT Wrt-Template
                   ╚Prompttext
                    ╚PTXT Open
                    ╚PTXT Make
                    ╚PTXT Wrt-Template
                   ╚TextModules
                    ╚TMDL Example
                    ╚Parse Command
                     ╚[ptm-cmd] {command}
                  ╚Configuration file
                   ╚Cfg Open
                   ╚Cfg Wrt
                   ╚Cfg Rd
                
                 AeroCL Backbridge
                  ╚ACL_BB
                   ╚ACL_BB start
                   ╚ACL_BB about
                """);

            if (Startup.printMacroInIndex)
            {
                Console.WriteLine("\n Your macros (execute via .${macro name})");
                string[] Macros = Directory.GetFiles("UniCMD.data\\Macros\\");
                if (Macros.Length > 0)
                {
                    foreach (string macro in Macros)
                        Console.WriteLine("  ╚" + Path.GetFileNameWithoutExtension(macro));
                }
                else
                {
                    Console.WriteLine("  ╚No macros found");
                }
            }

            
        }
        //filesystem commands usage
        public static void DirMakeUsage()
        {
            Console.WriteLine("""
                Usage of 'dir make'

                  input : dir make {name}
                          dir make /p {path}
                  output : directory is created from name/path

                """);
            
        }
        public static void DirDeleteUsage()
        {
            Console.WriteLine("""
                Usage of 'dir del'

                  input : dir del {name}
                          dir del /p {path}
                  output : name/path is deleted
                """);
            
            
        }
        public static void DirCloneUsage()
        {
            Console.WriteLine("""
                Usage of 'dir cln'

                  input : dir cln {name} /frc
                        dir cln /p {path} /frc
                  output : a copy of name/path is made, overwritten if optional /frc argument used
                """);
            
            
        }
        public static void DirRenameUsage()
        {
            Console.WriteLine("""
                Usage of 'dir rnm'

                  input : dir rnm {name} /name {new name}
                          dir rnm /p {path} /name {new name}
                  output : name/path is renamed to provided new name
                """);
            
            
        }
        public static void FileCreateUsage()
        {
            Console.WriteLine("""
                Usage of 'file make'

                  input : file make {name}
                          file make /p {path}
                  output : name/path is created
                """);
            
            
        }
        public static void FileDeleteUsage()
        {
            Console.WriteLine("""
                Usage of 'file del'

                  input : file del {name}
                          file del /p {path}
                  output : name/path is deleted
                """);
            
            
        }
        public static void FileReadUsage()
        {
            Console.WriteLine("""
                Usage of 'file rd'

                  input : file rd {name}
                          file rd /p {path}
                  output : contents of name/path are printed out
                """);
            
            
        }
        public static void FileWriteUsage()
        {
            Console.WriteLine("""
                Usage of 'file wrt'

                  input : file wrt {name}
                          file wrt /p {path}
                  output : a writing process is opened from name/path
                """);
        }
        public static void FileWriteLineUsage()
        {
            Console.WriteLine("""
                Usage of 'file wrtln'

                  input : file wrtln {name} /s {string}
                          file wrtln /p {path} /s {string}
                  output : string is appended to name/path
                """);
        }
        public static void FileClearUsage()
        {
            Console.WriteLine("""
                Usage of 'file clr'

                  input : file clr {name}
                          file clr /p {path}
                  output : name/path is wiped of all data
                """);
            
        }
        public static void FileCloneUsage()
        {
            Console.WriteLine("""
                Usage of 'file cln'

                  input : file cln {name} /frc
                          file cln /p {path} /frc
                  output : a copy of name/path is made, overwritten if optional /frc argument used
                """);   
            
        }
        public static void FileRenameUsage()
        {
            Console.WriteLine("""
                Usage of 'file rnm'

                  input : file rnm {name} /name {new name}
                          file rnm /p {path} /name {new name}
                  output : name/path is renamed to provided new name
                """);         
            
        }
        public static void FileZipUsage()
        {
            Console.WriteLine("""
                Usage of 'file zip'

                  input : file zip {directory name}
                          file zip /p {directory path}
                  output : zip archive is created from directory
                """);
            
        }
        public static void FileUnzipUsage()
        {
            Console.WriteLine("""
                Usage of 'file unzip'

                  input : file unzip {name}
                          file unzip /p {path}
                  output : zip archive is extracted from name/path
                """);
            
        }

        // python commands usage
        public static void IronPythonUsage()
        {
            Console.WriteLine("""
                Usage of 'irpy'

                  Python execution using IronPython
                  IronPython version : 2.7.11
                  input : irpy {name}
                          irpy /p {path}
                  output : name/path is executed with IronPython (*must be a .py file)
                """);
            
        }

        // backbridge stuff
        public static void BackbridgeUsage()
        {
            Console.WriteLine("""
                Usage of 'ACL_BB'

                  input : ACL_BB start
                  output : AeroCL backbridge is started and switched to

                  input : ACL_BB about
                  output : AeroCL backbridge information is displayed
                """);
            
        }
        public static void BackbridgeAbout()
        {
            Console.WriteLine("""
                  AeroCL Backbridge
                a AeroCL loader built into UniCMD
                included AeroCL version : 2.0
                loader version : {0}
                """, AeroCL_BB.bbver);  
            
        }

        // process commands usage
        public static void ProcessKillUsage()
        {
            Console.WriteLine("""
                Usage of 'proc end'

                  input : proc end {name}
                  output : every process with name is killed
                """);
            

            
        }
        public static void ProcessStartUsage()
        {
            Console.WriteLine("""
                Usage of 'proc run'

                  input : proc run {name} /args {optional arguments}
                          proc run /p {path} /args {optional arguments}
                  output : name/path is started
                           (with arguments if provided at the end of command)
                """);
            

            
        }
        // network commands
        public static void NetworkPingUsage()
        {
            Console.WriteLine("""
                Usage of 'net ping'

                  input : net ping {ip adress}
                  output : ping is sent to ip adress
                """);


            
        }
        public static void NetworkDownloadUsage()
        {
            Console.WriteLine("""
                Usage of 'net dload'

                  input : net dload {file url}
                          net dload {file url} /p {path}
                  output : file is downloaded from url to current directory or path if provided
                """);


            
        }
        public static void NetworkFetchContentUsage()
        {
            Console.WriteLine("""
                Usage of 'net fc'

                  input : net fc {url}
                  output : url page contents are fetched
                """);


            
        }

        // starttext & prompttext commands
        public static void StarttextHelp()
        {
            Console.WriteLine("""
                StartText is a feature that allows for custom text
                to be displayed when UniCMD is opened.
                To restore default StartText delete your custom file in
                  UniCMD.data\starttext.unicmd
                To create an StartText file simply enter 'stxt make'

                This feature supports TextModules, learn more by entering 'tmdl'
                """);
            
        }
        public static void PrompttextHelp()
        {
            Console.WriteLine("""
                Prompttext is a feature that allows for custom prompt bars
                (kind of like ohmyzsh or ohmyposh but less advanced)
                The contents of prompttext.unicmd replaces your default prompt bar
                To restore default prompt bar delete your custom file in
                  UniCMD.data\prompttext.unicmd
                To create an Prompttext file simply enter 'ptxt make'

                This feature supports TextModules, learn more by entering 'tmdl'
                """);
             
            
        }

        public static void TextModulesHelp()
        {
            Console.WriteLine("""
                TextModules is a feature that allows for inserting UniCMD-side data into text
                This is feature can by used in StartText or PromptText
                Enter 'TMDL Example' for an example of using TextModules

                 All TextModules are listed bellow
                -- Data modules
                 ::ver:: - UniCMD version
                 ::cdnm:: - Version codename
                 ::osver:: - OS version
                 ::ram:: - RAM memory
                 ::time:: - System time
                 ::cdir:: - Current set directory
                 ::root:: - If running as admin '(#)' is inserted, If not it is replaced with three spaces
                 ::user:: - System username
                 ::host:: - Host machine name
                 ::proc:: - Processor count
                 ::mmem:: - Memory mapped to UniCMD
                 ::tick:: - Tick count
                 ::sysp:: - System page size
                 ::sysd:: - System directory
                 ::appd:: - Application directory (%appdata%)
                 ::desk:: - Desktop directory
                 ::usrd:: - User directory
                 ::unsc:input:: - User argument for UniScript set using the /in argument, see 'uniscript' for more
                -- Color modules
                 :[red]:
                 :[green]:
                 :[blue]:
                 :[cyan]:
                 :[yellow]:
                 :[purple]:
                 :[white]:
                 :[]: - Close both background and text color streams
                 Use :{}: instead of :[]: for setting the background color

                *Textmodules colors do not work under the plain cmd window, use an terminal that supports ANSI

                """);
            
        }
        public static void TextModulesExample()
        {
            Console.WriteLine("TextModules example, for more refer to 'TMDL help'\n");
            Console.WriteLine(" -- Unparsed text\n");
            Console.WriteLine(" :[red]: this text is red :[]:");
            Console.WriteLine(" :[green]: this text is green :[]:");
            Console.WriteLine(" :[blue]: this text is green :[]:");
            Console.WriteLine(" :{red}: the background of this text is red :[]:");
            Console.WriteLine(" :{green}: the background of this text is green :[]:");
            Console.WriteLine(" :{blue}: the background of this text is blue :[]:");
            Console.WriteLine();
            Console.WriteLine(" the current time is ::time::");
            Console.WriteLine(" you are using UniCMD version ::ver::");
            Console.WriteLine(" your os version is ::osver::");
            Console.WriteLine("\n -- Parsed text\n");
            Console.WriteLine(" \u001b[31m this text is red \u001b[0m");
            Console.WriteLine(" \u001b[32m this text is green \u001b[0m");
            Console.WriteLine(" \u001b[34m this text is green \u001b[0m");
            Console.WriteLine(" \u001b[41m the background of this text is red\u001b[0m");
            Console.WriteLine(" \u001b[42m the background of this text is green\u001b[0m");
            Console.WriteLine(" \u001b[44m the background of this text is blue\u001b[0m");
            Console.WriteLine();
            Console.WriteLine(" the current time is " + DateTime.Now.ToString("hh:mm tt"));
            Console.WriteLine(" you are using UniCMD version " + Program.Version);
            Console.WriteLine(" your os version is " + Environment.OSVersion.ToString());
            
        }

        public static void UniScriptHelp()
        {
            Console.WriteLine("""
                UniScript is an scripting language that allows for
                executing UniCMD commands from an .unsc file
                for simple automation of tasks.

                  To run a UniScript file enter :
                  'uniscript {name} /in {optional argument}'
                  'uniscript /p {path} /in {optional argument}'

                The '/in' argument sets the ::unsc:input:: TextModule
                Use the 'UsrIn' set of commands to edit the user argument
                """);
            
        }
        public static void UserInputUtilsHelp()
        {
            Console.WriteLine("""
                The 'usrin' set of commands allows for editing the
                UniScript user argument (::unsc:input:: textmodule) on the fly

                Refer to command index for commands.
                """);
        }
        public static void UserInputReplaceHelp()
        {
            Console.WriteLine("""
                Usage of 'usrin repl'

                  input : usrin repl {string 1} /in {string 2}
                  output : string 1 is replaced with string 2 in user input argument
                """);
        }
        public static void UserInputReadFileHelp()
        {
            Console.WriteLine("""
                Usage of 'usrin rdf'

                  input : usrin rdf {name}
                          usrin rdf /p {path}
                  output : user argument is set to contents of name/path
                """);
        }
        public static void UserInputSetHelp()
        {
            Console.WriteLine("""
                Usage of 'usrin set'

                  input : usrin set {string}
                  output : user argument is set to string
                """);
        }
        public static void ParseCommandHelp()
        {
            Console.WriteLine("""
                [PTM-CMD] is a module allowing for TextModules in commands

                  To apply TextModules to a command run :
                  '[ptm-cmd] {command}'
                """);
            
        }
        // unipkg
        public static void UniPKGHelp()
        {
            string ver = UniPKG.Version;
            Console.WriteLine("""
                UniPKG ver. {0}
                UniCMD's offical package manager.
                
                All UniPKG packages are hosted on;
                https://unipkg.vercel.app/
                """, ver);
            
        }
        public static void UniPKGInstallUsage()
        {
            Console.WriteLine("""
                Usage of 'unipkg /inst'

                  input : unipkg /inst {package name}
                  output : package is downloaded and installed from server
                """);

            
        }
        public static void UniPKGDepackageUsage()
        {
            Console.WriteLine("""
                Usage of 'unipkg /dpkg'

                  input : unipkg /dpkg {package name}
                  output : package from current directory is installed
                """);

            
        }
        public static void UniPKGUninstallUsage()
        {
            Console.WriteLine("""
                Usage of 'unipkg /uinst'

                  input : unipkg /uinst {package name}
                  output : package is uninstalled using .uninst data

                To list installed packages use 'unipkg /list'
                """);

            
        }
        public static void UniPKGFetchInfoUsage()
        {
            Console.WriteLine("""
                Usage of 'unipkg /finfo'

                  input : unipkg /finfo {package name}
                  output : package info is fetched from .pkginfo file
                """);

            
        }
        public static void UniPKGFetchOnlineInfoUsage()
        {
            Console.WriteLine("""
                Usage of 'unipkg /foinfo'

                  input : unipkg /foinfo {package name}
                  output : package info is fetched unipkg server
                """);

            
        }

        // vm
        public static void VersionManagerUsage()
        {
            Console.WriteLine("""
                VersionManager ver. {0}
                Allows for comparing and downloading UniCMD releases.

                  'vm comp' for comparing local to latest
                  'vm lst' for indexing all releases
                  'vm pull' for usage
                """, VersionManager.Version);
        }
        public static void VersionManagerPullUsage()
        {
            Console.WriteLine("""
                Usage of 'vm pull'

                  input : vm pull {version id}
                          vm pull latest
                  output : version is pulled to UniCMD.data into instance folder
                """);
        }
    }
}
