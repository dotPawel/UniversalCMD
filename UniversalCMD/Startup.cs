namespace UniCMD
{
    static internal class Startup // https://cdn.discordapp.com/attachments/754607880359116824/1135948757280768000/gems.mp4
    {
        public static Dictionary<String, bool> ConfigDict = new Dictionary<String, bool>()
        {
            {"showExceptions", false },
            {"doAutoexec", false },
            {"printMacroInIndex", false },
            {"checkForUpdates", false },
        };
        public static string[] config;
        public static void MainStartUp()
        {
            Console.WriteLine("  UniCMD Start-Up\n");

            // Set working directory to where the UniCMD executeable is located
            // Helps avoiding situations where startup creates files and folders outside the main app directory
            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";

            CheckData();

            string[] config = System.IO.File.ReadAllLines(@"UniCMD.data/config.cfg");
            Console.WriteLine("config.cfg > to string");
            if (config.Contains("showExceptions = y"))
            {
                Console.WriteLine("showExceptions > ENABLED");
                ConfigDict["showExceptions"] = true;
            }
            if (config.Contains("printMacroInIndex = y"))
            {
                Console.WriteLine("printMacroInIndex > ENABLED");
                ConfigDict["printMacroInIndex"] = true;
            }
            if (config.Contains("checkForUpdates = y"))
            {
                Console.WriteLine("checkForUpdates > ENABLED");
                ConfigDict["checkForUpdates"] = true;
            }
            if (config.Contains("doAutoexec = y"))
            {
                Console.WriteLine("doAutoexec > ENABLED");
                ConfigDict["doAutoexec"] = true;
                UniScript.ExecuteAutoexec();
            }

            Console.WriteLine("  Start-Up finished");
            Console.Clear();
            // after this unicmd boots into the main prompt
        }
        public static void CheckData()
        {
            string[] ToCheckDir = 
            {
                "UniCMD.data", "UniCMD.data/Macros", "UniCMD.data/UniPKG", "UniCMD.data/UniPKG/pkginfo"
            };
            string[] ToCheckFile = 
            { 
                "UniCMD.data/config.cfg", "UniCMD.data/autoexec.cfg"
            };

            foreach (string dir in ToCheckDir)
            {
                if (Directory.Exists(dir))
                {
                    Console.WriteLine("OK -> " + dir);
                }
                else
                {
                    try
                    {
                        Directory.CreateDirectory(dir);
                        Console.WriteLine("MAKE -> " + dir);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERR -> " + dir);

                        Console.WriteLine("\n Attempt to create " + dir + " failed");
                        Console.WriteLine(" Exception : " + ex.Message);
                        Console.WriteLine(" ----- Press any key to exit -----");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }

            foreach (string file in ToCheckFile)
            {
                if (File.Exists(file))
                {
                    Console.WriteLine("OK -> " + file);
                }
                else
                {
                    try
                    {
                        File.Create(file).Close();
                        Console.WriteLine("MAKE -> " + file);

                        if (file == "UniCMD.data/config.cfg")
                        {
                            WriteTemplate();
                            Console.WriteLine("Restarting Start-Up");
                            Console.Clear();
                            MainStartUp();
                        }
                        Console.WriteLine(file + " -> TEMPLATE");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERR -> " + file);

                        Console.WriteLine("\n Attempt to create " + file + " failed");
                        Console.WriteLine(" Exception : " + ex.Message);
                        Console.WriteLine(" ----- Press any key to exit -----");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                }
            }
        }       
        public static void WriteTemplate()
        {
            using (StreamWriter sw = File.AppendText(@"UniCMD.data/config.cfg"))
            {
                sw.WriteLine("// UniveralCMD Config File");
                sw.WriteLine("// this is used when UniCMD is booting (start-up)");
                sw.WriteLine();
                sw.WriteLine("showExceptions = n");
                sw.WriteLine("doAutoexec = n");
                sw.WriteLine("printMacroInIndex = y");
                sw.WriteLine("checkForUpdates = y");
                sw.Close();
            }
        }
    }
}
