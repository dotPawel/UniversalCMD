using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class UniScript
    {
        public static bool UniScriptExecuting;

        public static void ExecuteMacro()
        {
            string text = Program.Command.Replace(".$", "");

            if (File.Exists(@"UniCMD.data\Macros\" + text + ".unsc"))
            {
                UniScriptExecuting = true;
                foreach (string line in File.ReadAllLines(@"UniCMD.data\Macros\" + text + ".unsc"))
                {
                    if (line.Length > 1)
                    {
                        Program.Command = line;

                        Program.CommandParser(line);
                    }
                }

                UniScriptExecuting = false;
                Console.WriteLine("\nUniScript macro finished.");
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("No macro named '{0}' exists", text);
                Program.Prompt();
            }
        }
        public static void Execute()
        {
            string file = Program.Command.Replace("uniscript ", "");
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
            
            if (File.Exists(file))
            {
                if (file.EndsWith(".unsc"))
                {
                    UniScriptExecuting = true;
                    foreach (string line in File.ReadAllLines(file))
                    {
                        if (line.Length > 1)
                        {
                            Program.Command = line;

                            Program.CommandParser(line);
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

        public static void ExecuteAutoexec() 
        {
            UniScriptExecuting = true;
            foreach (string line in File.ReadAllLines(@"UniCMD.data\autoexec.cfg"))
            {
                if (line.Length > 1)
                {
                    Program.UserCommand = line;

                    Program.CommandParser(line);
                }
            }
            Console.WriteLine("\nAutoexec finished");
            UniScriptExecuting = false;
        }
    }
}
