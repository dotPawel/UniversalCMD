using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class UniPKG
    {
        public static string Version = "1.5";
        // online
        public static async void InstallOnlinePackage()
        {
            // were so back
            OtherUtils.RootCheck();
            string[] pkgname = Program.UserCommand.Split(" ");
            if (File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".pkginfo"))
            {
                Console.WriteLine("Package entry already exists");
                return;
            }
            try
            {
                using (var client = new WebClient())
                {
                    Console.WriteLine("Fetching package : https://unipkg.vercel.app/unipkg/" + pkgname[2] + ".unipkg");
                    client.DownloadFile("https://unipkg.vercel.app/unipkg/" + pkgname[2] + ".unipkg", @"UniCMD.data\UniPKG\" + pkgname[2] + ".unipkg");
                }
                Console.WriteLine("Package downloaded successfully");

                try
                {
                    Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
                }
                catch { }
                Console.WriteLine("Making TEMP directory..");
                Directory.CreateDirectory(@"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Extracting package to TEMP..");
                System.IO.Compression.ZipFile.ExtractToDirectory(@"UniCMD.data\UniPKG\" + pkgname[2] + ".unipkg", @"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Deleting package image..");
                File.Delete(@"UniCMD.data\UniPKG\" + pkgname[2] + ".unipkg");

                Console.WriteLine("Fetching .pkginfo");

                CheckPackageInfo(pkgname[2]);
                ReadInfo(pkgname[2], true);
                ConfirmInstall();
                InstallFiles(pkgname[2]);
                await ExecutePostInst(pkgname[2]);

                DeleteTemp();

                Console.WriteLine("Finished installing");
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found.")
                {
                    Console.WriteLine("Package not found in server (404)");
                    return;
                }
                else
                {
                    Console.WriteLine("Installation failed");

                    OtherUtils.PrintException(ex);
                }

                Console.WriteLine("Deleting package image..");
                File.Delete(@"UniCMD.data\UniPKG\" + pkgname + ".unipkg");
                DeleteTemp();
            }
        }
        public static void FetchOnlineInfo()
        {
            string[] pkgname = Program.UserCommand.Split(" ");
            try
            {
                Console.WriteLine(@"Making TEMP ..");
                Directory.CreateDirectory(@"UniCMD.data\UniPKG\TEMP");

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://unipkg.vercel.app/unipkg/" + pkgname[2] + ".pkginfo", @"UniCMD.data\UniPKG\TEMP\" + pkgname[2] + ".pkginfo");
                }
                ReadInfo(pkgname[2], true);

                Console.WriteLine(@"Deleting TEMP ..");
                Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found.")
                {
                    Console.WriteLine("No package info found (404)");
                    return;
                }
                else
                {
                    Console.WriteLine("Fetching info failed");

                    OtherUtils.PrintException(ex);
                }

                Console.WriteLine(@"Deleting TEMP ..");
                Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
            }
        }

        // local
        public static async void Depackage()
        {
            if (Program.CurrentDir == null)
            {
                FileUtils.NoDirSet();
            }
            OtherUtils.RootCheck();
            string[] pkgname = Program.UserCommand.Split(" ");
            if (!File.Exists(Program.CurrentDir + pkgname[2]))
            {
                Console.WriteLine("Selected file does not exist");
                return;
            }
            if (!pkgname[2].EndsWith(".unipkg"))
            {
                Console.WriteLine("Selected file is not an UniPKG package");
                return;
            }
            try
            {
                try
                {
                    Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
                }
                catch {}
                Console.WriteLine("Making TEMP directory..");
                Directory.CreateDirectory(@"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Extracting package to TEMP..");
                System.IO.Compression.ZipFile.ExtractToDirectory(Program.CurrentDir + pkgname[2], @"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Fetching .pkginfo");
                string PackageName = pkgname[2].Replace(".unipkg", "");

                CheckPackageInfo(PackageName);
                ReadInfo(PackageName, true);
                InstallFiles(PackageName);
                await ExecutePostInst(PackageName);

                DeleteTemp();
                
                Console.WriteLine("Finished installing");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nFailed to depackage UniPKG package");
                Console.WriteLine("Deleting TEMP..");
                Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
                OtherUtils.PrintException(ex);
            }
        }
        public static void Uninstall()
        {
            string[] pkgname = Program.UserCommand.Split(" ");
            OtherUtils.RootCheck();
            if (!File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".uninst"))
            {
                Console.WriteLine("No .uninst file found for " + pkgname[2]);
                return;
            }

            try
            {
                Console.WriteLine("Uninstalling " + pkgname[2]);
                foreach (string line in File.ReadAllLines(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".uninst"))
                {
                    if (line.EndsWith(@"\"))
                    {
                        Directory.Delete(line, true);
                        Console.WriteLine(" [DEL] " + line);
                    }
                    else
                    {
                        File.Delete(line);
                        Console.WriteLine(" [DEL] " + line);
                    }
                }
                File.Delete(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".uninst");
                File.Delete(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".pkginfo");
                Console.WriteLine("Successfully uninstalled " + pkgname[2]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Uninstalling failed.");
                OtherUtils.PrintException(ex);
            }
        }
        public static void FetchInfo()
        {
            string[] pkgname = Program.UserCommand.Split(" ");
            if (!File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + pkgname[2] + ".pkginfo"))
            {
                Console.WriteLine("No .pkginfo file found for " + pkgname[2]);
                return;
            }

            ReadInfo(pkgname[2], false);
        }
        public static void ListInstalledPackages()
        {
            try
            {
                var files = Directory.GetFiles(@"UniCMD.data\UniPKG\pkginfo\");
                Console.WriteLine("  Installed packages");
                Console.WriteLine("----------------------------------------------");

                foreach (var d in files)
                {
                    if (d.EndsWith(".pkginfo"))
                    {
                        string pkgname = Path.GetFileName(d).Replace(".pkginfo", "");
                        Console.WriteLine(" " + pkgname);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Listing packages failed");
                OtherUtils.PrintException(ex);
                return;
            }
        }

        // Installation modules
        static void InstallFiles(string PackageName)
        {
            try
            {
                Console.WriteLine("Installing files from .inst");
                File.Move(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".uninst", @"UniCMD.data\UniPKG\pkginfo\" + PackageName + ".uninst");
                Console.WriteLine(" [INFO] .uninst file installed");

                File.Move(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".pkginfo", @"UniCMD.data\UniPKG\pkginfo\" + PackageName + ".pkginfo");
                Console.WriteLine(" [INFO] .pkginfo file installed");

                string[] InstLines = File.ReadAllLines(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".inst");
                foreach (string line in InstLines)
                {
                    if (line.Contains(" [->] "))
                    {
                        string[] path = line.Split(" [->] ");
                        File.Move(path[0], path[1]);
                        Console.WriteLine(" {0} [->] {1}", path[0], path[1]);
                    }
                    if (line.Contains(" [=>] "))
                    {
                        string[] path = line.Split(" [=>] ");
                        Directory.Move(path[0], path[1]);
                        Console.WriteLine(" {0} [=>] {1}", path[0], path[1]);
                    }
                    if (line.EndsWith(" [CR]"))
                    {
                        string path = line.Replace(" [CR]", "");
                        Directory.CreateDirectory(path);
                        Console.WriteLine(" [CR] " + path);
                    }
                }
                Console.WriteLine("Installing from .inst finished");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Installing files failed");
                OtherUtils.PrintException(ex);
                return;
            }
        }
        static void CheckPackageInfo(string PackageName)
        {
            try
            {
                if (!File.Exists(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".pkginfo"))
                {
                    Console.WriteLine("File does not have .pkginfo file, halting");
                    DeleteTemp();
                    Program.Prompt();
                }
                if (!File.Exists(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".inst"))
                {
                    Console.WriteLine("File does not have .inst file, halting");
                    DeleteTemp();
                    Program.Prompt();
                }
                if (!File.Exists(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".uninst"))
                {
                    Console.WriteLine("File does not have .uninst file, halting");
                    DeleteTemp();
                    Program.Prompt();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Checking package info failed");
                OtherUtils.PrintException(ex);
                return;
            }

            Console.WriteLine("All package files OK");
        }
        static void ReadInfo(string PackageName, bool InstallMode)
        {
            try
            {
                string[] PkgInfoLines;
                if (InstallMode)
                {
                    PkgInfoLines = File.ReadAllLines(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".pkginfo");
                }
                else
                {
                    PkgInfoLines = File.ReadAllLines(@"UniCMD.data\UniPKG\pkginfo\" + PackageName + ".pkginfo");
                }
                Console.WriteLine("\nDefined package information :");
                foreach (string line in PkgInfoLines)
                {
                    switch (line)
                    {
                        case string s when s.Contains("Name="):
                            Console.WriteLine(" Name : " + line.Replace("Name=", ""));
                            break;
                        case string s when s.Contains("PackageVersion="):
                            Console.WriteLine(" Version : " + line.Replace("PackageVersion=", ""));
                            break;
                        case string s when s.Contains("Author="):
                            Console.WriteLine(" Author : " + line.Replace("Author=", ""));
                            break;
                        case string s when s.Contains("UniCMDVer="):
                            Console.WriteLine(" For version : " + line.Replace("UniCMDVer=", ""));
                            break;
                        case string s when s.Contains("Description="):
                            Console.WriteLine(" Description : " + line.Replace("Description=", "").Replace(@"\n", "\n"));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reading info failed");
                OtherUtils.PrintException(ex);
                return;
            }
            Console.WriteLine();
        }
        static void DeleteTemp()
        {
            try
            {
                Console.WriteLine("Deleting TEMP..");
                Directory.Delete(@"UniCMD.data\UniPKG\TEMP", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Deleting TEMP failed");
                OtherUtils.PrintException(ex);
            }
        }
        static void ConfirmInstall()
        {
            Console.WriteLine("Package ready to install, proceed?\n");
            Console.Write("  (Y)es / (N)o ");
            ConsoleKeyInfo result = Console.ReadKey();

            if (result.Key == ConsoleKey.Y)
            {
                Console.WriteLine("\n");
                return;
            }
            else
            {
                Console.WriteLine();
                DeleteTemp();
                Console.WriteLine("Returning to main prompt..");
                Program.Prompt();
            }
        }
        async static Task ExecutePostInst(string PackageName)
        {
            if (File.Exists(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".postinst"))
            {
                Console.WriteLine("Found .postinst script, executing..\n");
                UniScript.UniScriptExecuting = true;
                foreach (string line in File.ReadAllLines(@"UniCMD.data\UniPKG\TEMP\" + PackageName + ".postinst"))
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
                Console.WriteLine();
            }
            UniScript.UniScriptExecuting = false;
        }
    }
}