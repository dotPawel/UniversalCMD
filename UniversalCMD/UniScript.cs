﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD // brain operated via bluetooth speaker
{
    internal class UniScript
    {
        public static bool UniScriptExecuting;

        // unsc execution
        public async static void ExecuteMacro()
        {
            string text = Program.Command.Replace(".$", "");

            if (text.Contains(" /in "))
            {
                string[] SplitCommand = text.Split(" /in ");
                text = SplitCommand[0];

                UserData.UnscUserArg = SplitCommand[1];
            }

            if (File.Exists(@"UniCMD.data\Macros\" + text + ".unsc"))
            {
                UniScriptExecuting = true;
                foreach (string line in File.ReadAllLines(@"UniCMD.data\Macros\" + text + ".unsc"))
                {
                    if (line.Length > 1)
                    {
                        Program.Command = line.ToLower();
                        Program.UserCommand = line;

                        Program.CommandParser(line);

                        while (!Program.ReadyToExecute)
                        {
                            await Task.Delay(1);
                        }
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
        public async static void Execute()
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
                    FileMan.NoDirSet();
                }
                file = Program.CurrentDir + file;
            }

            if (file.Contains(" /in "))
            {
                string[] SplitCommand = file.Split(" /in ");
                file = SplitCommand[0];

                UserData.UnscUserArg = SplitCommand[1];
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
                            Program.Command = line.ToLower();
                            Program.UserCommand = line;

                            Program.CommandParser(line);

                            while (!Program.ReadyToExecute)
                            {
                                await Task.Delay(1);
                            }
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
        }
        public async static void ExecuteAutoexec() 
        {
            UniScriptExecuting = true;
            foreach (string line in File.ReadAllLines(@"UniCMD.data\autoexec.cfg"))
            {
                if (line.Length > 1)
                {
                    Program.UserCommand = line;

                    Program.CommandParser(line);

                    while (!Program.ReadyToExecute)
                    {
                        await Task.Delay(1);
                    }
                }
            }
            Console.WriteLine("\nAutoexec finished");
            UniScriptExecuting = false;
        }
    }
}
