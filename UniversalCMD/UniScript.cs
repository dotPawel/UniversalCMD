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
            string text = Program.command.Replace(".$", "");

            if (File.Exists(@"UniCMD.data\Macros\" + text + ".unsc"))
            {
                UniScriptExecuting = true;
                foreach (string line in File.ReadAllLines(@"UniCMD.data\Macros\" + text + ".unsc"))
                {
                    if (line.Length > 1)
                    {
                        Program.command = line;

                        Program.Command(line);
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
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
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

        public static void ExecuteAutoexec() 
        {
            UniScriptExecuting = true;
            foreach (string line in File.ReadAllLines(@"UniCMD.data\autoexec.cfg"))
            {
                if (line.Length > 1)
                {
                    Program.uncvcommand = line;

                    Program.Command(line);
                }
            }
            Console.WriteLine("\nAutoexec finished");
            UniScriptExecuting = false;
        }
    }
}
