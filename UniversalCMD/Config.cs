using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class Config
    {
        // config commands
        public static void PrintCurrentConfig()
        {
            Console.WriteLine("  Current configuration table");
            Console.WriteLine("----------------------------------------------");
            foreach (var key in Startup.ConfigDict.Keys)
            {
                Console.WriteLine(key + " -> " + Startup.ConfigDict[key]);
            }
        }
        public static void SetConfigEntry()
        {
            var key = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));

            try
            {
                Startup.ConfigDict[key] = !Startup.ConfigDict[key];
                Console.WriteLine("[cfg] changed entry value, key : " + key + ", value : " + Startup.ConfigDict[key]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[cfg] cannot set entry");
                Other.PrintException(ex);
            }
        }
        public static void ApplyConfig()
        {
            try
            {
                Console.WriteLine("[cfg] clearing config file..");
                File.WriteAllText(@"UniCMD.data\config.cfg", "");
                using (StreamWriter sw = File.AppendText(@"UniCMD.data\config.cfg"))
                {
                    Console.WriteLine("[cfg] applying new config data..");
                    sw.WriteLine("// UniversalCMD config file");
                    sw.WriteLine("// Editing via configurator recommended");
                    foreach (var key in Startup.ConfigDict)
                    {
                        if (key.Value == true)
                        {
                            sw.WriteLine(key.Key + " = y");
                        }
                        else
                        {
                            sw.WriteLine(key.Key + " = n");
                        }
                    }
                }
                Console.WriteLine("[cfg] successfully applied new config table");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[cfg] cannot apply config changes");
                Other.PrintException(ex);
            }
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
                Other.PrintException(e);
            }
        }
        public static void CreateStarttext()
        {
            if (!File.Exists(@"UniCMD.data\starttext.unicmd"))
            {
                File.Create(@"UniCMD.data\starttext.unicmd").Close();
                Console.WriteLine("StartText file created.");
                return;
            }
            Console.WriteLine("StartText file already exists.");
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
                System.Diagnostics.Process.Start("notepad.exe", @"UniCMD.data\starttext.unicmd");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
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
                    return;
                }
                return;
            }
            Console.WriteLine("PromptText file already exists.");
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
                System.Diagnostics.Process.Start("notepad.exe", @"UniCMD.data\prompttext.unicmd");
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
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
        }

        public static string ApplyTextModules(string text)
        {
            System.Diagnostics.Process proc = System.Diagnostics.Process.GetCurrentProcess();

            text = text.Replace("::ver::", Program.Version)
                .Replace("::cdnm::", Program.Codename)
                .Replace("::osver::", Environment.OSVersion.ToString())
                .Replace("::ram::", proc.PrivateMemorySize64.ToString())
                .Replace("::time::", DateTime.Now.ToString("hh:mm tt"))
                .Replace("::user::", Environment.UserName)
                .Replace("::host::", Environment.MachineName)
                .Replace("::proc::", Environment.ProcessorCount.ToString())
                .Replace("::mmem::", Environment.WorkingSet.ToString())
                .Replace("::tick::", Environment.TickCount.ToString())
                .Replace("::sysp::", Environment.SystemPageSize.ToString())
                .Replace("::sysd::", Environment.SystemDirectory.ToString() + "\\")  
                .Replace("::appd::", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\") 
                .Replace("::desk::", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\")
                .Replace("::usrd::", Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\")
                .Replace("::unsc:input::", UserData.UnscUserArg);

            // dictionary
            foreach (var entry in UserData.StringDictionary)
            {
                string placeholder = $"::dict:\"{entry.Key}\"::";
                string value = entry.Value;
                text = text.Replace(placeholder, value);
            }

            if (Other.IsAdmin) { text = text.Replace("::root::", "(#)");  }
            else { text = text.Replace("::root::", "   "); }

            if (Program.CurrentDir == null) { text = text.Replace("::cdir::", "NULL"); }
            else { text = text.Replace("::cdir::", Program.CurrentDir); }

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
