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
        public static string Version = "1.1";
        // online
        public static void InstallOnlinePackage()
        {
            // were so back
            OtherUtils.RootCheck();
            string pkgname = Program.command.Replace("unipkg /inst ", "");
            if (File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + pkgname + ".pkginfo"))
            {
                Console.WriteLine("Package entry already exists");
                Program.Prompt();
            }
            try
            {
                using (var client = new WebClient())
                {
                    Console.WriteLine("Fetching package : https://unipkg.vercel.app/unipkg/" + pkgname + ".unipkg");
                    client.DownloadFile("https://unipkg.vercel.app/unipkg/" + pkgname + ".unipkg", @"UniCMD.data\UniPKG\" + pkgname + ".unipkg");
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
                System.IO.Compression.ZipFile.ExtractToDirectory(@"UniCMD.data\UniPKG\" + pkgname + ".unipkg", @"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Deleting package image..");
                File.Delete(@"UniCMD.data\UniPKG\" + pkgname + ".unipkg");

                Console.WriteLine("Fetching .pkginfo");

                CheckPackageInfo(pkgname);
                ReadInfo(pkgname, true);
                InstallFiles(pkgname);

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

            Program.Prompt();
        }
        public static void FetchOnlineInfo()
        {
            string pkgname = Program.command.Replace("unipkg /foinfo ", "");
            try
            {
                Console.WriteLine(@"Making TEMP ..");
                Directory.CreateDirectory(@"UniCMD.data\UniPKG\TEMP");

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://unipkg.vercel.app/unipkg/" + pkgname + ".pkginfo", @"UniCMD.data\UniPKG\TEMP\" + pkgname + ".pkginfo");
                }
                ReadInfo(pkgname, true);

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
            Program.Prompt();
        }

        // local
        public static void Depackage()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            OtherUtils.RootCheck();
            string file = Program.command.Replace("unipkg /dpkg ", "");
            if (!File.Exists(Program.currentdir + file))
            {
                Console.WriteLine("Selected file does not exist");
                Program.Prompt();
            }
            if (!file.EndsWith(".unipkg"))
            {
                Console.WriteLine("Selected file is not an UniPKG package");
                Program.Prompt();
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
                System.IO.Compression.ZipFile.ExtractToDirectory(Program.currentdir + file, @"UniCMD.data\UniPKG\TEMP");

                Console.WriteLine("Fetching .pkginfo");
                string PackageName = file.Replace(".unipkg", "");

                CheckPackageInfo(PackageName);
                ReadInfo(PackageName, true);
                InstallFiles(PackageName);

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
            Program.Prompt();
        }
        public static void Uninstall()
        {
            string package = Program.command.Replace("unipkg /uinst ", "");
            OtherUtils.RootCheck();
            if (!File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + package + ".uninst"))
            {
                Console.WriteLine("No .uninst file found for " + package);
                Program.Prompt();
            }

            try
            {
                Console.WriteLine("Uninstalling " + package);
                foreach (string line in File.ReadAllLines(@"UniCMD.data\UniPKG\pkginfo\" + package + ".uninst"))
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
                File.Delete(@"UniCMD.data\UniPKG\pkginfo\" + package + ".uninst");
                File.Delete(@"UniCMD.data\UniPKG\pkginfo\" + package + ".pkginfo");
                Console.WriteLine("Successfully uninstalled " + package);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Uninstalling failed.");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void FetchInfo()
        {
            string package = Program.command.Replace("unipkg /finfo ", "");
            if (!File.Exists(@"UniCMD.data\UniPKG\pkginfo\" + package + ".pkginfo"))
            {
                Console.WriteLine("No .pkginfo file found for " + package);
                Program.Prompt();
            }

            ReadInfo(package, false);
            Program.Prompt();
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
                Program.Prompt();
            }
            Program.Prompt();
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
                Program.Prompt();
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
                Program.Prompt();
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
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Reading info failed");
                OtherUtils.PrintException(ex);
                Program.Prompt();
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
                Program.Prompt();
            }
        }
    }
}