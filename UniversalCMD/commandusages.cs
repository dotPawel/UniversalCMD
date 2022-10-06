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
            Console.WriteLine("  ╚File Create");
            Console.WriteLine("   ╚File Create {name}");
            Console.WriteLine("   ╚File Create Path {path}");
            Console.WriteLine("  ╚File Delete");
            Console.WriteLine("   ╚File Delete {name}");
            Console.WriteLine("   ╚File Delete Path {path}");
            Console.WriteLine("  ╚File Read");
            Console.WriteLine("   ╚File Read {name}");
            Console.WriteLine("   ╚File Read Path {path}");
            Console.WriteLine("  ╚File Write");
            Console.WriteLine("   ╚File Write {name}");
            Console.WriteLine("   ╚File Write Path {path}");
            Console.WriteLine("  ╚File Clear");
            Console.WriteLine("   ╚File Clear {name}");
            Console.WriteLine("   ╚File Clear Path {path}");
            Console.WriteLine("  ╚File Clone");
            Console.WriteLine("   ╚File Clone {name}");
            Console.WriteLine("   ╚File Clone Path {path}");
            Console.WriteLine();
            Console.WriteLine(" Directory Management");
            Console.WriteLine("  ╚Set Directory");
            Console.WriteLine("  ╚Clear Set Directory");
            Console.WriteLine("  ╚Directory Create");
            Console.WriteLine("   ╚Directory Create {name}");
            Console.WriteLine("   ╚Directory Create Path {path}");
            Console.WriteLine("  ╚Directory Delete");
            Console.WriteLine("   ╚Directory Delete {name}");
            Console.WriteLine("   ╚Directory Delete Path {path}");
            Console.WriteLine("  ╚Directory Clone");
            Console.WriteLine("   ╚Directory Clone {name}");
            Console.WriteLine("   ╚Directory Clone Path {path}");
            Console.WriteLine("  ╚Directory Rename");
            Console.WriteLine("   ╚Directory Rename {name}");
            Console.WriteLine("   ╚Directory Rename Path {path}");
            Console.WriteLine();
            Console.WriteLine(" Process Commands");
            Console.WriteLine("  ╚Process Start");
            Console.WriteLine("   ╚Process Start {name}");
            Console.WriteLine("   ╚Process Start Path {path}");
            Console.WriteLine("  ╚Process Kill");
            Console.WriteLine("   ╚Process Kill {name}");
            Console.WriteLine("   ╚Process Kill Path {path}");
            Console.WriteLine("   ╚Process Kill All");
            Console.WriteLine();
            Console.WriteLine(" Python 3 Execution");
            Console.WriteLine("  ╚Python3 {name}");
            Console.WriteLine("  ╚Python3 Path {path}");
            Console.WriteLine();
            Console.WriteLine(" Other");
            Console.WriteLine("  ╚Clear");
            Console.WriteLine("  ╚About");
            Console.WriteLine();
            Console.WriteLine(" Configuration File");
            Console.WriteLine("  ╚Config Open");
            Console.WriteLine("  ╚Config Rewrite");
            Console.WriteLine("  ╚Config Print");
            Console.WriteLine();
            Console.WriteLine(" AeroCL Backbridge");
            Console.WriteLine("  ╚ACL_BB");
            Console.WriteLine("   ╚ACL_BB start");
            Console.WriteLine("   ╚ACL_BB about");

            Program.CMD();
        }
        //filesystem commands usage
        public static void createdirusage()
        {
            Console.WriteLine("Usage of 'directory create'");
            Console.WriteLine();
            Console.WriteLine("  input : directory create {name}");
            Console.WriteLine("          directory create path {path}");
            Console.WriteLine("  output : directory is created from name/path");

            Program.CMD();
        }
        public static void deletedirusage()
        {
            Console.WriteLine("Usage of 'directory delete'");
            Console.WriteLine();
            Console.WriteLine("  input : directory delete {name}");
            Console.WriteLine("          directory delete path {path}");
            Console.WriteLine("  output : name/path is deleted");
            Program.CMD();
        }
        public static void clonedirusage()
        {
            Console.WriteLine("Usage of 'directory clone'");
            Console.WriteLine();
            Console.WriteLine("  input : directory clone {name}");
            Console.WriteLine("          directory clone path {path}");
            Console.WriteLine("  output : name/path is cloned (a copy is made)");
            Program.CMD();
        }
        public static void renamedirusage()
        {
            Console.WriteLine("Usage of 'directory rename'");
            Console.WriteLine();
            Console.WriteLine("  input : directory rename {name}");
            Console.WriteLine("          directory rename path {path}");
            Console.WriteLine("  output : a renaming wizard for name/path is started");
            Program.CMD();
        }
        public static void createfileusage()
        {
            Console.WriteLine("Usage of 'file create'");
            Console.WriteLine();
            Console.WriteLine("  input : file create {name}");
            Console.WriteLine("          file create path {path}");
            Console.WriteLine("  output : name/path is created");
            Program.CMD();
        }
        public static void deletefileusage()
        {
            Console.WriteLine("Usage of 'file delete'");
            Console.WriteLine();
            Console.WriteLine("  input : file delete {name}");
            Console.WriteLine("          file delete path {path}");
            Console.WriteLine("  output : name/path is deleted");
            Program.CMD();
        }
        public static void readfileusage()
        {
            Console.WriteLine("Usage of 'file read'");
            Console.WriteLine();
            Console.WriteLine("  input : file read {name}");
            Console.WriteLine("          file read path {path}");
            Console.WriteLine("  output : contents of name/path are printed out");
            Program.CMD();
        }
        public static void writefileusage()
        {
            Console.WriteLine("Usage of 'file write'");
            Console.WriteLine();
            Console.WriteLine("  input : file write {name}");
            Console.WriteLine("          file write path {path}");
            Console.WriteLine("  output : a writing process is opened from name/path");
            Program.CMD();
        }
        public static void clearfileusage()
        {
            Console.WriteLine("Usage of 'file clear'");
            Console.WriteLine();
            Console.WriteLine("  input : file clear {name}");
            Console.WriteLine("          file clear path {path}");
            Console.WriteLine("  output : name/path is wiped of all data");
            Program.CMD();
        }
        public static void clonefileusage()
        {
            Console.WriteLine("Usage of 'file clone'");
            Console.WriteLine();
            Console.WriteLine("  input : file clone {name}");
            Console.WriteLine("          file clone path {path}");
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
        public static void pythonusage()
        {
            Console.WriteLine("Usage of 'python3'");
            Console.WriteLine();
            Console.WriteLine("  Python execution using IronPython");
            Console.WriteLine("  IronPython version : 2.7.11");
            Console.WriteLine("  input : python3 {name}");
            Console.WriteLine("          python3 path {path}");
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
            Console.WriteLine("Usage of 'process kill'");
            Console.WriteLine();
            Console.WriteLine("  input : process kill {name}");
            Console.WriteLine("  output : every process with name is killed");

            Program.CMD();
        }
        public static void processstartusage()
        {
            Console.WriteLine("Usage of 'process start'");
            Console.WriteLine();
            Console.WriteLine("  input : process start {name}");
            Console.WriteLine("          process start path {path}");
            Console.WriteLine("  output : name/path is started");

            Program.CMD();
        }
    }
}
