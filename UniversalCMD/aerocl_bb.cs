using System;
using System.Management;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

namespace UniCMD
{
    static internal class AeroCL_BB
    {
        public static string currentDir = "";
        public static string bbver = "0.6";
        internal static void acl_main()
        {
            Console.Clear();
            try
            {
                welcomeScreen();

                static void welcomeScreen()
                {
                    string currentDir;                
                    string userName = Environment.UserName;
                    ManagementObjectSearcher mos =
                    new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
                    Console.WriteLine("==================================================");
                    Console.WriteLine(" AeroCL 2.0 | " + userName);
                    Console.WriteLine(" (UniCMD Backbridge {0})", bbver);
                    Console.WriteLine("==================================================");
                    Console.WriteLine(" |OS; " + Environment.OSVersion);
                    Console.WriteLine(" |PC; " + Environment.MachineName);
                    foreach (ManagementObject mo in mos.Get())
                    {
                        Console.WriteLine(" |CPU; " + mo["Name"]);
                    }
                    Console.WriteLine("==================================================");
                    Console.WriteLine("'HELP' For command list");
                    Console.WriteLine("==================================================");
                    cmdvoid();              
                }
                static void cmdvoid()
                {
                    
                    Console.Write("@" + Environment.MachineName + ">");
                    String CMD = Console.ReadLine().ToUpper();

                    if (CMD == "HELP")
                    {
                        Console.WriteLine("");
                        Console.WriteLine("==================================================");
                        Console.WriteLine("Command List");
                        Console.WriteLine("==================================================");
                        Console.WriteLine(" Basics == ");
                        Console.WriteLine("   HELP - this command");
                        Console.WriteLine("   EXIT - exit AeroCL");
                        Console.WriteLine("   SHTDWN - mashine shutdown");
                        Console.WriteLine("   REBOOT - mashine reboot");
                        Console.WriteLine("   ECHO - returns given text");
                        Console.WriteLine("   FETCHINFO - welcome screen");
                        Console.WriteLine("   SETDIR - change current directory");
                        Console.WriteLine(" File Management == ");
                        Console.WriteLine("   CRDIR - create directory at current");
                        Console.WriteLine("   CRFILE - create file at current");
                        Console.WriteLine("   DLDIR - deletes directory at current");
                        Console.WriteLine("   DLFILE - deletes file at current");
                        Console.WriteLine("   LISTDIR - lists contents of current");
                        Console.WriteLine(" Drive utilities == ");
                        Console.WriteLine("   LISTDRIVE - list of logical drives");
                        Console.WriteLine("   LISTUSB - list of removeable usb devices");
                        Console.WriteLine(" Backbridge == ");
                        Console.WriteLine("   ACL_BB exit - return into UniCMD");
                        Console.WriteLine("   ACL_BB restart - restarts the AeroCL backbridge");
                        Console.WriteLine("==================================================");
                        cmdvoid();
                    }

                    //backbridge commands
                    if (CMD == "ACL_BB EXIT")
                    {
                        Console.WriteLine("Returning to UniCMD..");
                        Program.Prompt();
                    }
                    if (CMD == "ACL_BB RESTART")
                    {
                        Console.Clear();
                        currentDir = null;
                        CMD = null;
                        welcomeScreen();
                    }

                    //acl commands
                    if (CMD == "EXIT")
                    {
                        Environment.Exit(0);
                    }
                    if (CMD == "ECHO")
                    {
                        string echoText = Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine(echoText);
                        cmdvoid();
                    }
                    if (CMD == "BEEP")
                    {
                        Console.Beep();
                        Console.WriteLine("A sound has been sent");
                        cmdvoid();
                    }
                    if (CMD == "SHTDWN")
                    {
                        Process.Start("shutdown", "/s /t 0");
                    }
                    if (CMD == "REBOOT")
                    {
                        Process.Start("shutdown", "/r /t 0");
                    }
                    if (CMD == "FETCHINFO")
                    {
                        welcomeScreen();
                    }
                    if (CMD == "LISTDRIVE")
                    {
                        try
                        {
                            string[] drives = System.IO.Directory.GetLogicalDrives();

                            Console.WriteLine("=============================================================");
                            Console.WriteLine("Logical drives");
                            Console.WriteLine("=============================================================");

                            foreach (string str in drives)
                            {
                                System.Console.WriteLine("| " + str);
                            }
                            Console.WriteLine("=============================================================");
                            cmdvoid();
                        }
                        catch
                        {
                            Console.WriteLine("Unable to get drives");
                            cmdvoid();
                        }
                    }
                    if (CMD == "LISTUSB")
                    {
                        Console.WriteLine("=============================================================");
                        Console.WriteLine("Removeable drives");
                        Console.WriteLine("=============================================================");

                        foreach (DriveInfo drive in DriveInfo.GetDrives())
                        {
                            if (drive.DriveType == DriveType.Removable)
                            {
                                Console.WriteLine(string.Format("({0}) {1}", drive.Name.Replace("\\", ""), drive.VolumeLabel));
                            }
                            else
                            {
                                Console.WriteLine("No drives found");
                            }
                        }

                        Console.WriteLine("=============================================================");
                        cmdvoid();
                    }
                    if (CMD == "SETDIR")
                    {
                        Console.WriteLine();
                        Console.Write("Set current directory to? > ");

                        string setcurrentDir = Console.ReadLine();

                        if (setcurrentDir.EndsWith(@"\"))
                        {
                            if (Directory.Exists(setcurrentDir))
                            {
                                Console.WriteLine();
                                Console.WriteLine("The current directory is now set to > " + setcurrentDir);
                                currentDir = setcurrentDir;
                                cmdvoid();
                            }

                            if (File.Exists(setcurrentDir))
                            {
                                Console.WriteLine("Given path is a file not a accessable directory");
                                Console.WriteLine();
                                cmdvoid();
                            }

                        }

                        if (setcurrentDir.EndsWith(""))
                        {
                            Console.WriteLine("No backslash at the end of path");
                            Console.WriteLine();
                            cmdvoid();
                        }

                        else
                        {
                            Console.WriteLine("Cant access given directory");
                            Console.WriteLine();
                            cmdvoid();
                        }
                    }
                    if (CMD == "CRDIR")
                    {
                        if (currentDir == "")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Directory hasnt been set");
                            Console.WriteLine("Set it by executing SETDIR");
                            cmdvoid();
                        }


                        Console.WriteLine("Directory name?");
                        Console.WriteLine();

                        try
                        {
                            Console.Write(currentDir);
                            string crDir = Console.ReadLine();

                            Directory.CreateDirectory(currentDir + crDir);
                            Console.WriteLine();
                            Console.WriteLine("Directory created at ; " + currentDir + crDir);

                            cmdvoid();
                        }
                        catch
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cant create directory");
                            Console.WriteLine("invalid name or already exists");
                            cmdvoid();
                        }

                    }
                    if (CMD == "CRFILE")
                    {
                        if (currentDir == "")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Directory hasnt been set");
                            Console.WriteLine("Set it by executing SETDIR");
                            cmdvoid();
                        }

                        Console.WriteLine("File name? (file extension included)");
                        Console.WriteLine();

                        try
                        {
                            Console.Write(currentDir);
                            string crFile = Console.ReadLine();

                            File.Create(currentDir + crFile);
                            Console.WriteLine();
                            Console.WriteLine("File created at ; " + currentDir + crFile);

                            cmdvoid();
                        }
                        catch
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cant create file");
                            Console.WriteLine("invalid name or already exists");
                            cmdvoid();
                        }

                    }
                    if (CMD == "DLFILE")
                    {
                        if (currentDir == "")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Directory hasnt been set");
                            Console.WriteLine("Set it by executing SETDIR");
                            cmdvoid();
                        }

                        Console.WriteLine("File name? (file extension included)");
                        Console.WriteLine();

                        try
                        {
                            Console.Write(currentDir);
                            string delFile = Console.ReadLine();

                            File.Delete(currentDir + delFile);
                            Console.WriteLine();
                            Console.WriteLine("File deleted");

                            cmdvoid();
                        }
                        catch
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cant delete file");
                            Console.WriteLine("doesnt exist or file protected/critical");
                            cmdvoid();
                        }
                    }
                    if (CMD == "DLDIR")
                    {
                        if (currentDir == "")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Directory hasnt been set");
                            Console.WriteLine("Set it by executing SETDIR");
                            cmdvoid();
                        }

                        Console.WriteLine("Directory name?");
                        Console.WriteLine();

                        try
                        {
                            Console.Write(currentDir);
                            string delDir = Console.ReadLine();

                            Directory.Delete(currentDir + delDir);
                            Console.WriteLine();
                            Console.WriteLine("Directory deleted");

                            cmdvoid();
                        }
                        catch
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cant delete directory");
                            Console.WriteLine("directory protected or critical");
                            cmdvoid();
                        }
                    }
                    if (CMD == "LISTDIR")
                    {
                        if (currentDir == "")
                        {
                            Console.WriteLine();
                            Console.WriteLine("Directory hasnt been set");
                            Console.WriteLine("Set it by executing SETDIR");
                            cmdvoid();
                        }
                        string rootdir = currentDir;
                        Console.WriteLine();
                        Console.WriteLine("=============================================================");
                        Console.WriteLine("Contents of ; " + currentDir);
                        Console.WriteLine("=============================================================");
                        string[] files = Directory.GetFiles(rootdir);
                        Console.WriteLine(String.Join(Environment.NewLine, files));
                        string[] dirs = Directory.GetDirectories(rootdir);
                        Console.WriteLine(String.Join(Environment.NewLine, dirs));
                        Console.WriteLine("=============================================================");
                        cmdvoid();
                    }
                    else
                    {
                        Console.WriteLine("'" + CMD + "' is not a executeable command");
                        cmdvoid();
                    }
                }
            }
            catch
            {
                Console.WriteLine();
                Console.WriteLine("==================================================");
                Console.WriteLine(" AeroCL | CRASH HANDLER");
                Console.WriteLine("==================================================");
                Console.WriteLine("AeroCL has catched a unknown exception");
                Console.WriteLine("press any key to return into UniCMD");
                Console.WriteLine("==================================================");
                Console.ReadKey();
                Console.Clear();
                Program.Prompt();
            }
        }
    }
}
