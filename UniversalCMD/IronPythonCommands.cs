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
            string file = Program.Command.Replace("irpy ", "");
            if (file.StartsWith("/p "))
            {
                file = file.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileUtils.NoDirSet();
                }
                file = Program.CurrentDir + file;
            }

            if (File.Exists(file) && file.EndsWith(".py"))
            {
                try
                {
                    ScriptEngine engine = Python.CreateEngine();
                    Console.WriteLine("Initialized engine..");
                    Console.WriteLine("Executing " + file);
                    Console.WriteLine("----------------------------------------------");
                    engine.ExecuteFile(file);
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
        }
    }
}
