using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class python3commands
    {
        public static void runfile()
        {
            string filename = Program.command.Replace("python3 ", "");
            if (File.Exists(Program.currentdir + filename) && filename.EndsWith(".py"))
            {
                try
                {
                    ScriptEngine engine = Python.CreateEngine();
                    Console.WriteLine("Initialized engine..");
                    Console.WriteLine("Executing " + filename);
                    Console.WriteLine("----------------------------------------------");
                    engine.ExecuteFile(Program.currentdir + filename);
                    Console.WriteLine("----------------------------------------------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Python execution crashed");
                    Console.WriteLine("Exception :" + ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist or isnt a .py file");
            }
            Program.CMD();
        }
        public static void runfilepath()
        {
            string filename = Program.command.Replace("python3 path ", "");
            if (File.Exists(filename) && filename.EndsWith(".py"))
            {
                try
                {
                    ScriptEngine engine = Python.CreateEngine();
                    Console.WriteLine("Initialized engine..");
                    Console.WriteLine("Executing " + filename);
                    Console.WriteLine("----------------------------------------------");
                    engine.ExecuteFile(filename);
                    Console.WriteLine("----------------------------------------------");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Python execution crashed");
                    Console.WriteLine("Exception :" + ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist or isnt a .py file");
            }
            Program.CMD();
        }
    }
}
