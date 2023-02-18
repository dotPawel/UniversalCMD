using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class commandusages
    {
        public static void About()
        {
            Console.WriteLine("   UniversalCMD / Codename Rainman / Build " + Program.version + "\n");
            Console.WriteLine("  a better AeroCL & a easier command line solution");

            Program.Prompt();
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
                  ╚File Clear
                   ╚File Clr {name}
                   ╚File Clr /P {path}
                  ╚File Clone
                   ╚File Cln {name}
                   ╚File Cln /P {path}
                  ╚File Rename
                   ╚File Rnm {name}
                   ╚File Rnm /P {path}
                
                 Directory Management
                  ╚Set/Clear Directory
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
                   ╚Dir Rnm {name}
                   ╚Dir Rnm /P {path}
                
                 Process Commands
                  ╚Process Run
                   ╚Proc Run {name}
                   ╚Proc Run /P {path}
                  ╚Process End
                   ╚Proc End {name}
                   ╚Proc End /All

                 IronPython Execution
                  ╚IronPython {name}
                  ╚IronPython /P {path}

                 UniScript
                  ╚UniScript {name}
                  ╚UniScript /P {path}
               
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
                     ╚StartText Open
                     ╚StartText Create
                     ╚StartText Parse
                     ╚StartText Write-Template
                   ╚Prompttext
                    ╚Prompttext Open
                    ╚Prompttext Create
                    ╚Prompttext Write-Template
                   ╚TextModules
                    ╚TextModules Example
                  ╚Configuration File
                   ╚Config Open
                   ╚Config Rewrite
                   ╚Config Print
                
                 AeroCL Backbridge
                  ╚ACL_BB
                   ╚ACL_BB start
                   ╚ACL_BB about
                """);
            Program.Prompt();
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
            Program.Prompt();
        }
        public static void DirDeleteUsage()
        {
            Console.WriteLine("""
                Usage of 'dir del'

                  input : dir del {name}
                          dir del /p {path}
                  output : name/path is deleted
                """);
            
            Program.Prompt();
        }
        public static void DirCloneUsage()
        {
            Console.WriteLine("""
                Usage of 'directory clone'

                  input : dir cln {name}
                        dir cln /p {path}
                  output : name/path is cloned (a copy is made)
                """);
            
            Program.Prompt();
        }
        public static void DirRenameUsage()
        {
            Console.WriteLine("""
                Usage of 'dir rnm'

                  input : dir rnm {name}
                          dir rnm /p {path}
                  output : a renaming wizard for name/path is started
                """);
            
            Program.Prompt();
        }
        public static void FileCreateUsage()
        {
            Console.WriteLine("""
                Usage of 'file make'

                  input : file make {name}
                          file make /p {path}
                  output : name/path is created
                """);
            
            Program.Prompt();
        }
        public static void FileDeleteUsage()
        {
            Console.WriteLine("""
                Usage of 'file del'

                  input : file del {name}
                          file del /p {path}
                  output : name/path is deleted
                """);
            
            Program.Prompt();
        }
        public static void FileReadUsage()
        {
            Console.WriteLine("""
                Usage of 'file rd'

                  input : file rd {name}
                          file rd /p {path}
                  output : contents of name/path are printed out
                """);
            
            Program.Prompt();
        }
        public static void FileWriteUsage()
        {
            Console.WriteLine("""
                Usage of 'file wrt'

                  input : file wrt {name}
                          file wrt /p {path}
                  output : a writing process is opened from name/path
                """);
            
            Program.Prompt();
        }
        public static void FileClearUsage()
        {
            Console.WriteLine("""
                Usage of 'file clr'

                  input : file clr {name}
                          file clr /p {path}
                  output : name/path is wiped of all data
                """);
            
            Program.Prompt();
        }
        public static void FileCloneUsage()
        {
            Console.WriteLine("""
                Usage of 'file cln'

                  input : file cln {name}
                          file cln /p {path}
                  output : a copy of name/path is made
                """);   
            Program.Prompt();
        }
        public static void FileRenameUsage()
        {
            Console.WriteLine("""
                Usage of 'file rename'

                  input : file rename {name}
                          file rename path {path}
                  output : name/path is renamed
                """);         
            Program.Prompt();
        }

        // python commands usage
        public static void IronPythonUsage()
        {
            Console.WriteLine("""
                Usage of 'ironpython'

                  Python execution using IronPython
                  IronPython version : 2.7.11
                  input : ironpython {name}
                          ironpython /p {path}
                  output : name/path is executed with IronPython (*must be a .py file)
                """);
            Program.Prompt();
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
            Program.Prompt();
        }
        public static void BackbridgeAbout()
        {
            Console.WriteLine("""
                  AeroCL Backbridge
                a AeroCL loader built into UniCMD
                included AeroCL version : 2.0
                loader version : {0}
                """, aerocl_bb.bbver);  
            Program.Prompt();
        }

        // process commands usage
        public static void ProcessKillUsage()
        {
            Console.WriteLine("""
                Usage of 'process end'

                  input : process end {name}
                  output : every process with name is killed
                """);
            

            Program.Prompt();
        }
        public static void ProcessStartUsage()
        {
            Console.WriteLine("""
                Usage of 'process run'

                  input : process run {name}
                          process run /p {path}
                  output : name/path is started
                """);
            

            Program.Prompt();
        }

        // starttext & prompttext commands
        public static void StarttextHelp()
        {
            Console.WriteLine("""
                StartText is a feature that allows for custom text
                to be displayed when UniCMD is opened.
                To restore default StartText delete your custom file in
                  UniCMD.data\starttext.unicmd
                To create an StartText file simply enter 'starttext create'

                This feature supports TextModules, learn more by entering 'textmodules help'
                """);
            Program.Prompt();
        }
        public static void PrompttextHelp()
        {
            Console.WriteLine("""
                Prompttext is a feature that allows for custom prompt bars
                (kind of like ohmyzsh or ohmyposh but less advanced)
                The contents of prompttext.unicmd replaces your default prompt bar
                To restore default prompt bar delete your custom file in
                  UniCMD.data\prompttext.unicmd
                To create an Prompttext file simply enter 'prompttext create'

                This feature supports TextModules, learn more by entering 'textmodules help'
                """);
             
            Program.Prompt();
        }

        public static void TextModulesHelp()
        {
            Console.WriteLine("""
                TextModules is a feature that allows for inserting UniCMD-side data into text
                This is feature can by used in StartText or PromptText
                Enter 'TextModule Example' for an example of using TextModules

                 All TextModules are listed bellow
                -- Data modules
                 ::ver:: - UniCMD version
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
            Program.Prompt();
        }
        public static void TextModulesExample()
        {
            Console.WriteLine("TextModules example, for more refer to 'textmodules help'\n");
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
            Console.WriteLine(" you are using UniCMD version " + Program.version);
            Console.WriteLine(" your os version is " + Environment.OSVersion.ToString());
            Program.Prompt();
        }

        public static void UniScriptHelp()
        {
            Console.WriteLine("""
                UniScript is an scripting language that allows for
                executing UniCMD commands from an .unsc file
                for simple automation of tasks.

                  To run a UniScript file enter :
                  'uniscript {name}'
                  'uniscript /p {path}'
                """);
            Program.Prompt();
        }
    }
}
