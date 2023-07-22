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
    }
}
