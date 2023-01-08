using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class commandusages
    {
        public static void about()
        {
            Console.WriteLine("   UniversalCMD / Codename Rainman / Build " + Program.version + "\n");
            Console.WriteLine("  a better AeroCL & a easier command line solution");

            Program.CMD();
        }
        public static void help()
        {
            Console.WriteLine("    Command Index\n");
            Console.WriteLine(" File Management");
            Console.WriteLine("  ╚File Make");
            Console.WriteLine("   ╚File Make {name}");
            Console.WriteLine("   ╚File Make /P {path}");
            Console.WriteLine("  ╚File Delete");
            Console.WriteLine("   ╚File Del {name}");
            Console.WriteLine("   ╚File Del /P {path}");
            Console.WriteLine("  ╚File Read");
            Console.WriteLine("   ╚File Rd {name}");
            Console.WriteLine("   ╚File Rd /P {path}");
            Console.WriteLine("  ╚File Write");
            Console.WriteLine("   ╚File Wrt {name}");
            Console.WriteLine("   ╚File Wrt /P {path}");
            Console.WriteLine("  ╚File Clear");
            Console.WriteLine("   ╚File Clr {name}");
            Console.WriteLine("   ╚File Clr /P {path}");
            Console.WriteLine("  ╚File Clone");
            Console.WriteLine("   ╚File Cln {name}");
            Console.WriteLine("   ╚File Cln /P {path}");
            Console.WriteLine("  ╚File Rename");
            Console.WriteLine("   ╚File Rnm {name}");
            Console.WriteLine("   ╚File Rnm /P {path}");
            Console.WriteLine();
            Console.WriteLine(" Directory Management");
            Console.WriteLine("  ╚Set/Clear Directory");
            Console.WriteLine("   ╚SD");
            Console.WriteLine("   ╚SD Clr");
            Console.WriteLine("  ╚Directory Make");
            Console.WriteLine("   ╚Dir Make {name}");
            Console.WriteLine("   ╚Dir Make /P {path}");
            Console.WriteLine("  ╚Directory Delete");
            Console.WriteLine("   ╚Dir Del {name}");
            Console.WriteLine("   ╚Dir Del /P {path}");
            Console.WriteLine("  ╚Dir Cln");
            Console.WriteLine("   ╚Dir Cln {name}");
            Console.WriteLine("   ╚Dir Cln /P {path}");
            Console.WriteLine("  ╚Dir Rnm");
            Console.WriteLine("   ╚Dir Rnm {name}");
            Console.WriteLine("   ╚Dir Rnm /P {path}");
            Console.WriteLine();
            Console.WriteLine(" Process Commands");
            Console.WriteLine("  ╚Process Run");
            Console.WriteLine("   ╚Proc Run {name}");
            Console.WriteLine("   ╚Proc Run /P {path}");
            Console.WriteLine("  ╚Process End");
            Console.WriteLine("   ╚Proc End {name}");
            Console.WriteLine("   ╚Proc End /All");
            Console.WriteLine();
            Console.WriteLine(" IronPython Execution");
            Console.WriteLine("  ╚IronPython {name}");
            Console.WriteLine("  ╚IronPython /P {path}");
            Console.WriteLine();
            Console.WriteLine(" Other");
            Console.WriteLine("  ╚Clr");
            Console.WriteLine("  ╚About");
            Console.WriteLine("  ╚Exit");
            Console.WriteLine("  ╚Echo");
            Console.WriteLine();
            Console.WriteLine(" Configuration File");
            Console.WriteLine("  ╚Config Open");
            Console.WriteLine("  ╚Config Rewrite");
            Console.WriteLine("  ╚Config Print");
            Console.WriteLine();
            Console.WriteLine(" StartText File");
            Console.WriteLine("  ╚StartText Help");
            Console.WriteLine("  ╚StartText Open");
            Console.WriteLine("  ╚StartText Create");
            Console.WriteLine("  ╚StartText Parse");
            Console.WriteLine("  ╚StartText Write-Template");
            Console.WriteLine();
            Console.WriteLine(" AeroCL Backbridge");
            Console.WriteLine("  ╚ACL_BB");
            Console.WriteLine("   ╚ACL_BB start");
            Console.WriteLine("   ╚ACL_BB about");

            Program.CMD();
        }
        //filesystem commands usage
        public static void dirmakeusage()
        {
            Console.WriteLine("Usage of 'dir make'");
            Console.WriteLine();
            Console.WriteLine("  input : dir make {name}");
            Console.WriteLine("          dir make /p {path}");
            Console.WriteLine("  output : directory is created from name/path");

            Program.CMD();
        }
        public static void deletedirusage()
        {
            Console.WriteLine("Usage of 'dir del'");
            Console.WriteLine();
            Console.WriteLine("  input : dir del {name}");
            Console.WriteLine("          dir del /p {path}");
            Console.WriteLine("  output : name/path is deleted");
            Program.CMD();
        }
        public static void clonedirusage()
        {
            Console.WriteLine("Usage of 'directory clone'");
            Console.WriteLine();
            Console.WriteLine("  input : dir cln {name}");
            Console.WriteLine("          dir cln /p {path}");
            Console.WriteLine("  output : name/path is cloned (a copy is made)");
            Program.CMD();
        }
        public static void renamedirusage()
        {
            Console.WriteLine("Usage of 'dir rnm'");
            Console.WriteLine();
            Console.WriteLine("  input : dir rnm {name}");
            Console.WriteLine("          dir rnm /p {path}");
            Console.WriteLine("  output : a renaming wizard for name/path is started");
            Program.CMD();
        }
        public static void createfileusage()
        {
            Console.WriteLine("Usage of 'file make'");
            Console.WriteLine();
            Console.WriteLine("  input : file make {name}");
            Console.WriteLine("          file make /p {path}");
            Console.WriteLine("  output : name/path is created");
            Program.CMD();
        }
        public static void deletefileusage()
        {
            Console.WriteLine("Usage of 'file del'");
            Console.WriteLine();
            Console.WriteLine("  input : file del {name}");
            Console.WriteLine("          file del /p {path}");
            Console.WriteLine("  output : name/path is deleted");
            Program.CMD();
        }
        public static void readfileusage()
        {
            Console.WriteLine("Usage of 'file rd'");
            Console.WriteLine();
            Console.WriteLine("  input : file rd {name}");
            Console.WriteLine("          file rd /p {path}");
            Console.WriteLine("  output : contents of name/path are printed out");
            Program.CMD();
        }
        public static void writefileusage()
        {
            Console.WriteLine("Usage of 'file wrt'");
            Console.WriteLine();
            Console.WriteLine("  input : file wrt {name}");
            Console.WriteLine("          file wrt /p {path}");
            Console.WriteLine("  output : a writing process is opened from name/path");
            Program.CMD();
        }
        public static void clearfileusage()
        {
            Console.WriteLine("Usage of 'file clr'");
            Console.WriteLine();
            Console.WriteLine("  input : file clr {name}");
            Console.WriteLine("          file clr /p {path}");
            Console.WriteLine("  output : name/path is wiped of all data");
            Program.CMD();
        }
        public static void clonefileusage()
        {
            Console.WriteLine("Usage of 'file cln'");
            Console.WriteLine();
            Console.WriteLine("  input : file cln {name}");
            Console.WriteLine("          file cln /p {path}");
            Console.WriteLine("  output : a copy of name/path is made");
            Program.CMD();
        }
        public static void renamefileusage()
        {
            Console.WriteLine("Usage of 'file rename'");
            Console.WriteLine();
            Console.WriteLine("  input : file rename {name}");
            Console.WriteLine("          file rename path {path}");
            Console.WriteLine("  output : name/path is renamed");

            Program.CMD();
        }

        // python commands usage
        public static void ironpythonusage()
        {
            Console.WriteLine("Usage of 'ironpython'");
            Console.WriteLine();
            Console.WriteLine("  Python execution using IronPython");
            Console.WriteLine("  IronPython version : 2.7.11");
            Console.WriteLine("  input : ironpython {name}");
            Console.WriteLine("          ironpython /p {path}");
            Console.WriteLine("  output : name/path is executed with IronPython (*must be a .py file)");

            Program.CMD();
        }

        // backbridge stuff
        public static void backbridgeusage()
        {
            Console.WriteLine("Usage of 'ACL_BB'");
            Console.WriteLine();
            Console.WriteLine("  input : ACL_BB start");
            Console.WriteLine("  output : AeroCL backbridge is started and switched to");
            Console.WriteLine();
            Console.WriteLine("  input : ACL_BB about");
            Console.WriteLine("  output : AeroCL backbridge information is displayed");

            Program.CMD();
        }
        public static void backbridgeabout()
        {
            Console.WriteLine("  AeroCL Backbridge");
            Console.WriteLine(" a AeroCL loader built into UniCMD");
            Console.WriteLine(" included AeroCL version : 2.0");
            Console.WriteLine(" loader version : {0}", aerocl_bb.bbver);
            Program.CMD();
        }

        // process commands usage
        public static void processkillusage()
        {
            Console.WriteLine("Usage of 'process end'");
            Console.WriteLine();
            Console.WriteLine("  input : process end {name}");
            Console.WriteLine("  output : every process with name is killed");

            Program.CMD();
        }
        public static void processstartusage()
        {
            Console.WriteLine("Usage of 'process run'");
            Console.WriteLine();
            Console.WriteLine("  input : process run {name}");
            Console.WriteLine("          process run /p {path}");
            Console.WriteLine("  output : name/path is started");

            Program.CMD();
        }

        // starttext commands
        public static void starttxthelp()
        {
            Console.WriteLine(" StartText is a feature that allows for custom text");
            Console.WriteLine(" to be displayed when UniCMD is opened.");
            Console.WriteLine(" To restore default StartText delete your custom file in");
            Console.WriteLine(@" UniCMD.data\starttext.unicmd");
            Console.WriteLine();
            Console.WriteLine(" Use in starttext.unicmd file");
            Console.WriteLine("  ::ver:: -- UniCMD version");
            Console.WriteLine("  ::osver:: -- OS version");
            Console.WriteLine("  ::ram:: -- RAM memory");
            Console.WriteLine("  ::time:: -- System time");
            Program.CMD();
        }

    }
}
