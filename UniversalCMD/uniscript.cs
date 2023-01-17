using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class uniscript
    {
        public static bool UniScriptExecuting;
        public static void Execute()
        {
            if (Program.currentdir == null)
            {
                fileutils.NoDirSet();
            }
            string text = Program.command.Replace("uniscript ", "");
            if (File.Exists(Program.currentdir + text))
            {
                if (text.EndsWith(".unsc"))
                {
                    UniScriptExecuting = true;
                    foreach (string line in File.ReadAllLines(Program.currentdir + text))
                    {
                        if (line.Length > 1)
                        {
                            Program.command = line;

                            Program.Command(line);
                        }      
                    }
                }
                else
                {
                    Console.WriteLine("File is not an UniScript file (.unsc)");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            UniScriptExecuting = false;
            Console.WriteLine("\nUniScript file finished.");
            Program.Prompt();
        }
        public static void ExecutePath()
        {
            string text = Program.command.Replace("uniscript /p ", "");
            if (File.Exists(text))
            {
                if (text.EndsWith(".unsc"))
                {
                    UniScriptExecuting = true;
                    foreach (string line in File.ReadAllLines(Program.currentdir + text))
                    {
                        if (line.Length > 1)
                        {
                            Program.command = line;

                            Program.Command(line);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File is not an UniScript file (.unsc)");
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            UniScriptExecuting = false;
            Console.WriteLine("\nUniScript file finished.");
            Program.Prompt();
        }
    }
}
