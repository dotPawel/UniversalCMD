using System.Diagnostics;

namespace UniCMD
{
    static internal class Startup // this thing is such a mess, may cause brain damage just by looking at it
    {
        public static bool showExceptions = false;
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
                    WriteTemplate();
                    Console.WriteLine("UniCMD will restart now");                    
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
        public static void WriteTemplate()
        {
            using (StreamWriter sw = File.AppendText(@"UniCMD.data/config.cfg"))
            {
                sw.WriteLine("//UniveralCMD Config File");
                sw.WriteLine("//this is used when UniCMD is booting (start-up)");
                sw.WriteLine();
                sw.WriteLine("showExceptions = n");
                sw.Close();
            }
        }
    }
}
