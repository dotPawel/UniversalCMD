using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class ConfigCommands
    {
        // config commands
        public static void OpenConfig()
        {
            try
            {
                string fileName = "UniCMD.data/config.cfg";
                FileInfo f = new FileInfo(fileName);
                string fullname = f.FullName;
                
                Console.WriteLine("  Opening configuration file..");
                Console.WriteLine("  full path : " + fullname);
                Process.Start(@"notepad.exe", fullname);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to open configuration file");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void RewriteConfig()
        {
            try
            {
                Console.WriteLine("  Wiping config.cfg ..");
                File.WriteAllBytes(@"UniCMD.data/config.cfg", new byte[0]);
                Console.WriteLine("  Writing template ..");
                Startup.WriteTemplate();
                Console.WriteLine("  Configuration restored to default");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to restore configuration file");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void PrintConfig()
        {
            try
            {
                string file = File.ReadAllText(@"UniCMD.data/config.cfg", Encoding.UTF8);

                Console.WriteLine("  Current configuration file");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not print configuration file");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }

        // starttext commands
        public static void ParseStarttext()
        {
            try
            {
                string starttext = File.ReadAllText(@"UniCMD.data\starttext.unicmd");

                starttext = ApplyTextModules(starttext);

                Console.WriteLine(starttext);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to parse starttext");
                OtherUtils.PrintException(e);
            }
            Program.Prompt();
        }
        public static void CreateStarttext()
        {
            if (!File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                File.Create(@"UniCMD.data\starttext.unicmd").Close();
                Console.WriteLine("StartText file created.");
                Program.Prompt();
            }
            Console.WriteLine("StartText file already exists.");
            Program.Prompt();
        }
        public static void OpenStarttext()
        {
            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                string fileName = "UniCMD.data/starttext.unicmd";
                FileInfo f = new FileInfo(fileName);
                string fullname = f.FullName;

                Console.WriteLine("  Opening StartText file..");
                Console.WriteLine("  full path : " + fullname);
                Process.Start("notepad.exe", @"UniCMD.data\starttext.unicmd");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            Program.Prompt();
        }
        public static void WriteTemplateStarttext()
        {
            if (File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                Console.WriteLine("Writing template to starttext.unicmd");
                using (StreamWriter sw = File.AppendText(@"UniCMD.data\starttext.unicmd"))
                {
                    sw.WriteLine("Example UniCMD StartText file.");
                    sw.WriteLine();
                    sw.WriteLine(" ver : ::ver::");
                    sw.WriteLine(" osver : ::osver::");
                    sw.WriteLine(" ram : ::ram::");
                    sw.WriteLine(" time : ::time::");
                    sw.Close();
                }
                Console.WriteLine("Finished writing template.");
            }
            else
            {
                Console.WriteLine("StartText file not found.");
            }
            Program.Prompt();
        }
        // prompttext commands  
        public static void ParsePrompttext()
        {
            string prompttext = File.ReadAllText(@"UniCMD.data\prompttext.unicmd");
            
            prompttext = ApplyTextModules(prompttext);

            Console.Write(prompttext);
        }
        public static void CreatePromptText()
        {
            if (!File.Exists(@"UniCMD.data\prompttext.unicmd"))
            {
                File.Create(@"UniCMD.data\prompttext.unicmd").Close();
                Console.WriteLine("PromptText file created.");

                Console.WriteLine(" Automaticly write template? (otherwise you will have no prompt bar)\n");
                Console.Write("  (Y)es / (N)o ");
                ConsoleKeyInfo result = Console.ReadKey();
                Console.WriteLine("\n");
                if (result.Key == ConsoleKey.Y)
                {
                    WritePromptTextTemplate();
                }
                else
                {
                    Program.Prompt();
                }
                Program.Prompt();
            }
            Console.WriteLine("PromptText file already exists.");
            Program.Prompt();
        }
        public static void OpenPromptText()
        {
            if (File.Exists(@"UniCMD.data\prompttext.unicmd"))
            {
                string fileName = "UniCMD.data/prompttext.unicmd";
                FileInfo f = new FileInfo(fileName);
                string fullname = f.FullName;

                Console.WriteLine("  Opening PromptText file..");
                Console.WriteLine("  full path : " + fullname);
                Process.Start("notepad.exe", @"UniCMD.data\prompttext.unicmd");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            Program.Prompt();
        }
        public static void WritePromptTextTemplate()
        {
            if (File.Exists(@"UniCMD.data\prompttext.unicmd"))
            {
                Console.WriteLine("Writing template to prompttext.unicmd");

                File.WriteAllText(@"UniCMD.data\prompttext.unicmd", "");

                using (StreamWriter sw = File.AppendText(@"UniCMD.data\prompttext.unicmd"))
                {
                    sw.Write(":[red]: ::cdir:: :[]:||:[green]: ::time:: :[]: >");
                    
                    sw.Close();
                }
                Console.WriteLine("Finished writing template.");
            }
            else
            {
                Console.WriteLine("StartText file not found.");
            }
            Program.Prompt();
        }

        public static string ApplyTextModules(string text)
        {
            Process proc = Process.GetCurrentProcess();

            text = text.Replace("::ver::", Program.version)
                .Replace("::osver::", Environment.OSVersion.ToString())
                .Replace("::ram::", proc.PrivateMemorySize64.ToString())
                .Replace("::time::", DateTime.Now.ToString("hh:mm tt"))
                .Replace("::user::", Environment.UserName)
                .Replace("::host::", Environment.MachineName)
                .Replace("::proc::", Environment.ProcessorCount.ToString())
                .Replace("::mmem::", Environment.WorkingSet.ToString())
                .Replace("::tick::", Environment.TickCount.ToString())
                .Replace("::sysp::", Environment.SystemPageSize.ToString());

            if (OtherUtils.runningAsAdmin) { text = text.Replace("::root::", "(#)");  }
            else { text = text.Replace("::root::", "   "); }

            if (Program.currentdir == null) { text = text.Replace("::cdir::", "NULL"); }
            else { text = text.Replace("::cdir::", Program.currentdir); }

            text = text.Replace(":[red]:", "\u001B[31m") // text colors
                .Replace(":[green]:", "\u001B[32m")
                .Replace(":[blue]:", "\u001B[34m")
                .Replace(":[cyan]:", "\u001B[36m")
                .Replace(":[yellow]:", "\u001B[33m")
                .Replace(":[purple]:", "\u001B[35m")
                .Replace(":[white]:", "\u001B[37m")

                .Replace(":{red}:", "\u001b[41m")  // background colors
                .Replace(":{green}:", "\u001b[42m")
                .Replace(":{blue}:", "\u001b[44m")
                .Replace(":{cyan}:", "\u001b[46m")
                .Replace(":{yellow}:", "\u001b[43m")
                .Replace(":{purple}:", "\u001b[45m")
                .Replace(":{white}:", " \u001b[47m")
                .Replace(":[]:", "\u001B[0m");
            return text;     
        }
    }
}
