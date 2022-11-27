using System.Diagnostics;
using System.Runtime.InteropServices;

namespace UniCMD
{
    static internal class Startup // this thing is such a mess, may cause brain damage just by looking at it
    {
        public static bool showExceptions = false;
        public static string[] config;
        public static void mainstartup()
        {
            Console.WriteLine("   UniCMD Start-Up\n");
            checkdata();

            string[] config = System.IO.File.ReadAllLines(@"UniCMD.data/config.cfg");
            Console.WriteLine("config.cfg > to string");
            if (config.Contains("showExceptions = y"))
            {
                Console.WriteLine("showExceptions > ENABLED");
                showExceptions = true;
            }
            settheme(config);

            Console.WriteLine("\n    Start-Up finished");
            Console.Clear();
            // after this unicmd boots into the main prompt
        }
        public static void checkdata()
        {
            // data folder
            if (Directory.Exists(@"UniCMD.data"))
            {
                Console.WriteLine("UniCMD > OK");
            }
            else
            {
                Console.WriteLine("\nUniCMD.data does not exist");
                Console.WriteLine("creating UniCMD.data ..");
                try
                {
                    Directory.CreateDirectory(@"UniCMD.data");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue to UniCMD");
                    Console.ReadKey();
                }
            }
            // config
            if (File.Exists(@"UniCMD.data/config.cfg"))
            {
                Console.WriteLine("config.cfg > OK");
            }
            else
            {
                Console.WriteLine("\nconfig.cfg does not exist");
                Console.WriteLine("creating config.cfg ..");
                try
                {
                    var myFile = File.Create(@"UniCMD.data/config.cfg");
                    myFile.Close();
                    Console.WriteLine("writing config template..");
                    writetemplate();
                    Console.WriteLine("UniCMD will restart now");
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        Process.Start(@"UniCMD");
                        Environment.Exit(0);
                    }
                    Process.Start(@"UniCMD.exe");
                    Environment.Exit(0);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue to UniCMD");
                    Console.ReadKey();
                }              
            }
        }
        static void settheme(string[] config)
        {
            foreach (string line in config)
            {
                if (line.StartsWith("textColor = "))
                {
                    string color = line.Replace("textColor = ", "");
                    try
                    {
                        ConsoleColor consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                        Console.ForegroundColor = consoleColor;
                        Console.WriteLine("config.cfg > textColor = " + consoleColor);
                    }
                    catch
                    {
                        Console.WriteLine("\nconfig.cfg > textColor > Error");
                        Console.WriteLine("could not parse and set text color");
                        Console.WriteLine("check your config file for any errors");
                        Console.ReadKey();
                    }
                }
                if (line.StartsWith("backgroundColor = "))
                {
                    string color = line.Replace("backgroundColor = ", "");
                    try
                    {
                        ConsoleColor consoleColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color, true);
                        Console.BackgroundColor = consoleColor;
                        Console.WriteLine("config.cfg > backgroundColor = " + consoleColor);
                    }
                    catch
                    {
                        Console.WriteLine("\nconfig.cfg > backgroundColor > Error");
                        Console.WriteLine("could not parse and set background color");
                        Console.WriteLine("check your config file for any errors");
                        Console.ReadKey();
                    }
                }
            }       
        }
        public static void writetemplate()
        {
            using (StreamWriter sw = File.AppendText(@"UniCMD.data/config.cfg"))
            {
                sw.WriteLine("//UniveralCMD Config File");
                sw.WriteLine("//this is used when UniCMD is booting (start-up)");
                sw.WriteLine();
                sw.WriteLine("showExceptions = n");
                sw.WriteLine();
                sw.WriteLine("textColor = White");
                sw.WriteLine("backgroundColor = Black");
                sw.Close();
            }
        }
    }
}
