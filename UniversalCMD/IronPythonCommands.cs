using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class IronPythonCommands
    {
        public static void RunFile()
        {
            string filename = Program.command.Replace("ironpython ", "");
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
            Program.Prompt();
        }
        public static void RunFilePath()
        {
            string filename = Program.command.Replace("ironpython /p ", "");
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
            Program.Prompt();
        }
    }
}
