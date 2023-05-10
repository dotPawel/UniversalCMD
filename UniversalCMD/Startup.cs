using System.Diagnostics;

namespace UniCMD
{
    static internal class Startup // this thing is such a mess, may cause brain damage just by looking at it
    {
        public static bool showExceptions = false;
        public static bool doAutoexec = false;
        public static string[] config;
        public static void MainStartUp()
        {
            Console.WriteLine("   UniCMD Start-Up\n");
            CheckData();

            string[] config = System.IO.File.ReadAllLines(@"UniCMD.data/config.cfg");
            Console.WriteLine("config.cfg > to string");
            if (config.Contains("showExceptions = y"))
            {
                Console.WriteLine("showExceptions > ENABLED");
                showExceptions = true;
            }
            if (config.Contains("doAutoexec = y"))
            {
                Console.WriteLine("doAutoexec > ENABLED");
                UniScript.ExecuteAutoexec();
            }

            Console.WriteLine("\n    Start-Up finished");
            Console.Clear();
            // after this unicmd boots into the main prompt
        }
        public static void CheckData()
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
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            // autoexec file
            if (File.Exists(@"UniCMD.data/autoexec.cfg"))
            {
                Console.WriteLine("autoexec.cfg > OK");
            }
            else
            {
                Console.WriteLine("\nautoexec.cfg does not exist");
                Console.WriteLine("creating autoexec.cfg ..");
                try
                {
                    var myFile = File.Create(@"UniCMD.data\autoexec.cfg");
                    myFile.Close();
                    Console.WriteLine("Restarting Start-Up process");
                    MainStartUp();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue");
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
                    var myFile = File.Create(@"UniCMD.data\config.cfg");
                    myFile.Close();
                    Console.WriteLine("writing config template..");
                    WriteTemplate();
                    Console.WriteLine("Restarting Start-Up process");                    
                    MainStartUp();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create config.cfg");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }              
            }
            // macros folder
            if (Directory.Exists(@"UniCMD.data\Macros"))
            {
                Console.WriteLine("UniCMD/Macros > OK");
            }
            else
            {
                Console.WriteLine("\nUniCMD.data/Macros does not exist");
                Console.WriteLine("creating /UniPKG..");
                try
                {
                    Directory.CreateDirectory(@"UniCMD.data\Macros");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data/Macros");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }

            // unipkg folder
            if (Directory.Exists(@"UniCMD.data\UniPKG"))
            {
                Console.WriteLine("UniCMD/UniPKG > OK");
            }
            else
            {
                Console.WriteLine("\nUniCMD.data/UniPKG does not exist");
                Console.WriteLine("creating /UniPKG ..");
                try
                {
                    Directory.CreateDirectory(@"UniCMD.data\UniPKG");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data/UniPKG");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
            // unipkg\pkginfo folder
            if (Directory.Exists(@"UniCMD.data\UniPKG\pkginfo"))
            {
                Console.WriteLine("UniCMD/UniPKG/pkginfo > OK");
            }
            else
            {
                Console.WriteLine("\nUniCMD.data/UniPKG/pkginfo does not exist");
                Console.WriteLine("creating /UniPKG/pkginfo ..");
                try
                {
                    Directory.CreateDirectory(@"UniCMD.data\UniPKG\pkginfo");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nCould not create UniCMD.data/UniPKG/pkginfo");
                    Console.WriteLine("Exception :\n\n" + ex + "\n");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                }
            }
        }       
        public static void WriteTemplate()
        {
            using (StreamWriter sw = File.AppendText(@"UniCMD.data/config.cfg"))
            {
                sw.WriteLine("//UniveralCMD Config File");
                sw.WriteLine("//this is used when UniCMD is booting (start-up)");
                sw.WriteLine();
                sw.WriteLine("showExceptions = n");
                sw.WriteLine("doAutoexec = n");
                sw.Close();
            }
        }
    }
}
