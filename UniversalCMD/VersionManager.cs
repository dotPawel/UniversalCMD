using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Community.CsharpSqlite.Sqlite3;

namespace UniCMD
{
    internal class VersionManager
    {
        public static string Version = "1.0";
        public static void CompareVersionToLatest(bool showerror)
        {
            try
            {
                if (Program.Version.EndsWith("d") || Program.Version.EndsWith("rc"))
                {
                    Console.WriteLine("Updater disabled due to nightly version");
                    return;
                }

                var client = new GitHubClient(new ProductHeaderValue("UniversalCMD"));
                var latest = client.Repository.Release.GetLatest("dotPawel", "UniversalCMD").Result;

                if (latest.TagName != Program.Version)
                {
                    Console.WriteLine(" New version of UniversalCMD is available");
                    Console.WriteLine("  {0} -> {1}", Program.Version, latest.TagName);
                    Console.WriteLine(" Download via 'vm pull latest'");
                }

            }
            catch (Exception ex)
            {
                if (showerror)
                {
                    Console.WriteLine("Checking for update failed");
                    Other.PrintException(ex);
                }
            }
        }
        public static void ListAllReleases()
        {
            try
            {
                var client = new GitHubClient(new ProductHeaderValue("UniversalCMD"));
                var latest = client.Repository.Release.GetAll("dotPawel", "UniversalCMD").Result;

                Console.WriteLine("  Release index [Tag / ID / Date]");
                Console.WriteLine("----------------------------------------------");
                foreach (var release in latest)
                {
                    Console.WriteLine(" {0} / {1} / {2}", release.TagName, release.Id, release.PublishedAt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fetching releases failed");
                Other.PrintException(ex);
            }
            
        }

        public static async void PullRelease()
        {
            var release = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));

            try
            {
                var client = new GitHubClient(new ProductHeaderValue("UniversalCMD"));
                var latest = client.Repository.Release.GetLatest("dotPawel", "UniversalCMD");
                int id = 0;

                if (release.ToLower() == "latest")
                {
                    id = latest.Result.Id;
                }
                else
                {
                    bool isInt = Int32.TryParse(release, out int x);
                    if (isInt)
                    {
                        id = x;
                    }
                    else
                    {
                        Console.WriteLine("Invalid release id");
                        return;
                    }
                    
                }
                var release_assets = client.Repository.Release.GetAllAssets("dotPawel", "UniversalCMD", id);
                var release_name = client.Repository.Release.Get("dotPawel", "UniversalCMD", id).Result.TagName;
                foreach (var asset in release_assets.Result)
                {
                    Console.WriteLine("Pulling release : {0}", release_name);
                    using (var web_client = new WebClient())
                    {
                        web_client.DownloadFile(asset.BrowserDownloadUrl, "UniCMD.data\\" + release_name + ".zip");
                    }
                }

                Console.WriteLine("Extracting release zip");
                System.IO.Compression.ZipFile.ExtractToDirectory("UniCMD.data\\" + release_name + ".zip", "UniCMD.data\\instance_" + release_name, true);

                File.Delete("UniCMD.data\\" + release_name + ".zip");
                Console.WriteLine("Pulling release successful\n");

                Console.WriteLine(" Open in explorer?");
                Console.Write("      (Y)es / (N)o  ");
                ConsoleKeyInfo result = Console.ReadKey();
                Console.WriteLine();
                if (result.Key == ConsoleKey.Y)
                {
                    System.Diagnostics.Process.Start("explorer.exe", "UniCMD.data\\instance_" + release_name);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Pulling release failed");
                Other.PrintException(ex);
            }
        }
    }
}
