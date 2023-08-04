using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UniCMD
{
    internal class Networking
    {
        public static void Ping()
        {
            string ip = Program.Command.Replace("net ping ", "");
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send(ip, 1000);
                if (reply != null)
                {
                    Console.WriteLine(" Status : " + reply.Status + " \n Time : " + reply.RoundtripTime.ToString() + " \n Address : " + reply.Address);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot ping ip");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void Download()
        {
            // my brain is no longer functional

            string ip = Program.Command.Replace("net dload ", "");
            string path = "";
            if (ip.Contains(" /p "))
            {
                path = ip.Split(" /p ")[1];
                ip = ip.Split(" /p ")[0];
            }
            else
            {
                // if no path provided
                if (Program.CurrentDir == null)
                {
                    FileUtils.NoDirSet();
                }
                else
                {
                    path = Program.CurrentDir + Path.GetFileName(ip);
                }
            }

            try
            {
                using (var client = new WebClient())
                {
                    Console.WriteLine("Downloading file");
                    Console.WriteLine(" Url : " + ip);
                    Console.WriteLine(" Location : " + path);
                    Console.WriteLine();
                    client.DownloadFile(ip, path);
                    Console.WriteLine("File successfully downloaded");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Downloading file failed");
                OtherUtils.PrintException(ex);
            }
        }
        public static void FetchContents()
        {
            string ip = Program.Command.Replace("net fc ", "");

            try
            {
                using (WebClient client = new WebClient())
                {
                    string Contents = client.DownloadString(ip);
                    Console.WriteLine("  Contents of " + ip);
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine(Contents);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fetching url contents failed");
                OtherUtils.PrintException(ex);
            }
        }
    }
}
