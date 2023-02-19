﻿using System.Text;

namespace UniCMD
{
    internal class FileUtils
    {
        //dir
        public static void SetDirectory()
        {
            if (Program.currentdir == null)
            {
                Console.WriteLine("Setting current directory..");
                Console.WriteLine("all the file commands will operate in this directory");
                Console.WriteLine("to cancel leave this field empty and press enter");
                Console.WriteLine();
                Console.Write(" > ");
                string path = Console.ReadLine();

                if (path.EndsWith(@"\"))
                {
                    if (Directory.Exists(path))
                    {
                        Program.currentdir = path;
                        Console.WriteLine("Current dir set to : " + path);
                        Program.Prompt();
                    }
                    else
                    {
                        Console.WriteLine("\nentered path isnt a valid or accesible directory");
                        Program.Prompt();
                    }
                }
                else
                {
                    Console.WriteLine("\nentered path needs to end with a backslash for UniCMD to read it properly");
                    Program.Prompt();
                }
            }
            else
            {
                Console.WriteLine("Changing current directory..");
                Console.WriteLine("this command edits the current directory");
                Console.WriteLine("to clear set current execute 'clear set directory' in main prompt");
                Console.WriteLine("to cancel this action leave the field empty");
                Console.WriteLine("to go back one directory enter '..'\n");

                Console.Write("  " + Program.currentdir);
                string newdir = Console.ReadLine();
                Console.WriteLine();

                if (newdir == "..")
                {
                    string backonedir = Directory.GetParent(Program.currentdir).ToString();
                    string backtwodir = Directory.GetParent(backonedir).ToString();
                    Program.currentdir = backtwodir;
                    Console.WriteLine("Sucessfully edited current directory..");
                    Program.Prompt();
                }
                else
                {
                    string setnewdir = Path.Combine(Program.currentdir, newdir);

                    if (Directory.Exists(setnewdir))
                    {
                        if (setnewdir.EndsWith(@"\"))
                        {
                            Program.currentdir = setnewdir;
                            Console.WriteLine("Sucessfully edited directory..");
                            Program.Prompt();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nentered path isnt a valid or accesible directory");
                        Program.Prompt();
                    }
                }
            }
        }
        public static void ClearSetDirectory()
        {
            Program.currentdir = null;
            Console.WriteLine("Cleared set directory");
            Program.Prompt();
        }
        public static void ListDir()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            else
            {
                Console.WriteLine("  Contents of " + Program.currentdir);
                Console.WriteLine("----------------------------------------------");
                var directories = Directory.GetDirectories(Program.currentdir);
                foreach (var d in directories)
                {
                    Console.WriteLine(" " + d + @"\");
                }
                var files = Directory.GetFiles(Program.currentdir);
                foreach (var d in files)
                {
                    Console.WriteLine(" " + d);
                }
                Program.Prompt();
            }
        }
        public static void NoDirSet()
        {
            Console.WriteLine("in order to use this command set a directory first");
            Console.WriteLine("the command for it is 'sd'");
            Program.Prompt();
        }

        public static void CreateDir()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string dirname = Program.command.Replace("dir make ", "");
            try
            {
                if (Directory.Exists(Program.currentdir + dirname))
                {
                    Console.WriteLine("Directory already exists");
                    Program.Prompt();
                }
                Directory.CreateDirectory(Program.currentdir + dirname);
                Console.WriteLine("Directory created at current");
                Console.WriteLine(" " + Program.currentdir + dirname);
            }
            catch (Exception ex)
            {
                if (OtherUtils.runningAsAdmin == false)
                {
                    Console.WriteLine("Could not create directory, access denied");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
                Console.WriteLine("Could not create directory.");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void CreateDirPath()
        {
            string dirname = Program.command.Replace("dir make /p ", "");
            try
            {
                if (Directory.Exists(dirname))
                {
                    Console.WriteLine("Directory already exists");
                    Program.Prompt();
                }
                Directory.CreateDirectory(dirname);
                Console.WriteLine("Directory created from path");
                Console.WriteLine(" " + Program.currentdir + dirname);
            }
            catch (Exception ex)
            {
                if (OtherUtils.runningAsAdmin == false)
                {
                    Console.WriteLine("Could not create directory, access denied");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
                Console.WriteLine("Could not create directory.");
                OtherUtils.PrintException(ex);
            }
            Program.Prompt();
        }
        public static void DeleteDir()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string dirname = Program.command.Replace("dir del ", "");
            if (Directory.Exists(Program.currentdir + dirname))
            {
                try
                {
                    Directory.Delete(Program.currentdir + dirname, true);
                    Console.WriteLine("Deleted directory from current");
                    Console.WriteLine(" " + Program.currentdir + dirname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not delete directory, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot delete directory");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist");
                Program.Prompt();
            }
            Program.Prompt();
        }
        public static void DeleteDirPath()
        {
            string dirname = Program.command.Replace("dir del /p ", "");
            if (Directory.Exists(dirname))
            {
                try
                {
                    Directory.Delete(dirname, true);
                    Console.WriteLine("Deleted directory from path");
                    Console.WriteLine(" " + dirname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not delete directory, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot delete directory");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist");
                Program.Prompt();
            }
            Program.Prompt();
        }
        public static void CloneDir()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("dir cln ", "");
            if (Directory.Exists(Program.currentdir + filename))
            {
                string clone = Path.Combine(Program.currentdir, filename + "_(copy)");

                try
                {
                    if (File.Exists(clone))
                    {
                        Console.WriteLine(" Directory copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {

                        }
                        else
                        {
                            Program.Prompt();
                        }
                    }
                    else
                    {

                    }
                    Console.WriteLine("Cloning directory.. (might freeze with bigger directories)");
                    // why did microsoft make File.Copy but not Directory.Copy, anyways here's some wierd visual basic shit
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(Program.currentdir + filename, clone);
                    Console.WriteLine("Sucessfully cloned directory");
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not clone directory, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.WriteLine("Could not clone directory");
                        OtherUtils.PrintException(ex);
                    }
                }
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("Directory not found");
            }
            Program.Prompt();
        }
        public static void CloneDirPath()
        {
            string filename = Program.command.Replace("dir cln /p ", "");
            if (Directory.Exists(filename))
            {
                string clone = Path.Combine(filename + "_(copy)");

                try
                {
                    if (File.Exists(clone))
                    {
                        Console.WriteLine(" Directory copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {

                        }
                        else
                        {
                            Program.Prompt();
                        }
                    }
                    else
                    {

                    }
                    Console.WriteLine("Cloning directory.. (might freeze with bigger directories)");
                    // why tf did microsoft make File.Copy but not Directory.Copy, anyways here's some wierd visual basic shit
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(filename, clone);
                    Console.WriteLine("Sucessfully cloned directory");
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not clone directory, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.WriteLine("Could not clone directory");
                        OtherUtils.PrintException(ex);
                    }
                }
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("Directory not found");
            }
            Program.Prompt();
        }
        public static void RenameDir()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("dir rnm ", "");
            if (Directory.Exists(Program.currentdir + filename))
            {
                Console.WriteLine(" Enter new directory name");
                Console.WriteLine(" to cancel enter empty\n");
                Console.Write("   >");
                string newname = Console.ReadLine();
                Console.WriteLine();
                if (newname == null)
                {
                    Console.WriteLine("Returning to main prompt..");
                    Program.Prompt();
                }
                try
                {
                    Directory.Move(Program.currentdir + filename, Program.currentdir + newname);
                    Console.Write("Successfully renamed {0} to {1}", filename, newname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.Write("Could not rename directory, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.Write("Could not rename directory");
                        OtherUtils.PrintException(ex);
                    }
                }
            }
            else
            {
                Console.Write("Directory does not exist");
            }
            Program.Prompt();
        }
        public static void RenameDirPath()
        {
            string filename = Program.command.Replace("dir rnm /p ", "");
            if (Directory.Exists(filename))
            {
                Console.WriteLine(" Enter new directory name");
                Console.WriteLine(" to cancel enter empty\n");
                Console.Write("   >");
                string newname = Console.ReadLine();
                string pardir = Directory.GetParent(filename).FullName; 
                Console.WriteLine();
                if (newname == null)
                {
                    Console.WriteLine("Returning to main prompt..");
                    Program.Prompt();
                }
                try
                {
                    Directory.Move(filename, pardir + @"\" + newname);
                    Console.Write("Successfully renamed {0} to {1}", filename, newname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.Write("Could not rename directory, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.Write("Could not rename directory");
                        OtherUtils.PrintException(ex);
                    }
                }
            }
            else
            {
                Console.Write("Directory does not exist");
            }
            Program.Prompt();
        }

        //file
        public static void CreateFile()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            else
            {
                string filename = Program.command.Replace("file make ", "");
                try
                {
                    var myFile = File.Create(Program.currentdir + filename);
                    myFile.Close();
                    Console.WriteLine("Created file at current");
                    Console.WriteLine(" " + Program.currentdir + filename);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not create file, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot create file");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            Program.Prompt();
        }
        public static void CreateFilePath()
        {
            string filename = Program.command.Replace("file make /p ", "");
            if (File.Exists(filename))
            {
                Console.WriteLine("File already exists.");
                Program.Prompt();
            }
            else
            {
                try
                {
                    var myFile = File.Create(filename);
                    myFile.Close();
                    Console.WriteLine("Created file from path");
                    Console.WriteLine(" " + filename);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not create file, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot create file..");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            Program.Prompt();
        }
        public static void DeleteFilePath()
        {
            string filename = Program.command.Replace("file del /p ", "");
            if (File.Exists(filename))
            {
                try
                {
                    
                    Console.WriteLine("Deleted file from path");
                    Console.WriteLine(" " + filename);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not delete file, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot delete file");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
                Program.Prompt();
            }
            Program.Prompt();
        }
        public static void DeleteFile()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("file del ", "");
            if (File.Exists(Program.currentdir + filename))
            {
                try
                {
                    File.Delete(Program.currentdir + filename);
                    Console.WriteLine("Deleted file from current");
                    Console.WriteLine(" " + Program.currentdir + filename);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not delete file, access denied");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                    Console.WriteLine("Cannot delete file");
                    OtherUtils.PrintException(ex);
                    Program.Prompt();
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
                Program.Prompt();
            }
            Program.Prompt();
        }
        public static void ReadFile()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("file rd ", "");
            if (File.Exists(Program.currentdir + filename))
            {
                string file = File.ReadAllText(Program.currentdir + filename, Encoding.UTF8);

                Console.WriteLine("  Contents of " + filename);
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

            Program.Prompt();
        }
        public static void ReadFilePath()
        {
            string filename = Program.command.Replace("file rd /p ", "");

            if (File.Exists(filename))
            {
                string file = File.ReadAllText(filename, Encoding.UTF8);

                Console.WriteLine("  Contents of " + filename);
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("File does not exist");
            }

            Program.Prompt();
        }
        public static void WriteFile()
        {
            string filename = Program.command.Replace("file wrt ", "");
            if (File.Exists(Program.currentdir + filename))
            {
                Console.WriteLine("  Writing to " + Program.currentdir + filename);
                Console.WriteLine(" To stop writing enter '__!!stop'");
                Console.WriteLine("----------------------------------------------");
                writeline();
                void writeline()
                {
                    try
                    {
                        string line;
                        line = Console.ReadLine();
                        if (line == "__!!stop")
                        {
                            Console.WriteLine("\n    Stopping writing process..");
                            Program.Prompt();
                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(Program.currentdir + filename))
                            {
                                sw.WriteLine(line);
                            }
                            line = null;
                            writeline();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n    Cant write to file");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void WriteFilePath() // https://cdn.discordapp.com/attachments/992907489853054976/996056313526239242/nnfreuiownfrelw1231fds.mp4
        {
            string filename = Program.command.Replace("file wrt /p ", "");
            if (File.Exists(filename))
            {
                Console.WriteLine("  Writing to " + filename);
                Console.WriteLine(" To stop writing enter '__!!stop'");
                Console.WriteLine("----------------------------------------------");
                writeline();
                void writeline()
                {
                    try
                    {
                        string line;
                        line = Console.ReadLine();
                        if (line == "__!!stop")
                        {
                            Console.WriteLine("\n    Stopping writing process..");
                            Program.Prompt();
                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(filename))
                            {
                                sw.WriteLine(line);
                            }
                            line = null;
                            writeline();
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n    Cant write to file");
                        OtherUtils.PrintException(ex);
                        Program.Prompt();
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void ClearFile()
        {
            string filename = Program.command.Replace("file clr ", "");
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            if (File.Exists(Program.currentdir + filename))
            {
                FileInfo fileinfo = new FileInfo(Program.currentdir + filename);
                var bytes = fileinfo.Length;
                var inMb = (bytes / 1024 / 1024);
                if (inMb > 10)
                {
                    Console.WriteLine("  Larger file size disclaimer");
                    Console.WriteLine("----------------------------------------------\n");
                    Console.WriteLine("  The file selected {0}", filename);
                    Console.WriteLine("  has the size of above 10 MB");
                    Console.WriteLine("  Actual size : {0} MB\n", inMb);
                    Console.WriteLine("   Are you sure?\n");
                    Console.Write("      (Y)es / (N)o  ");
                    choice();
                    void choice()
                    {
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\b\b");
                        if (result.Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nWiping all data in {0}.. (might freeze on larger files)");
                            try
                            {
                                System.IO.File.WriteAllBytes(Program.currentdir + filename, new byte[0]);
                                Console.WriteLine("Sucessfully wiped all data from file..");
                                Program.Prompt();
                            }
                            catch (Exception ex)
                            {
                                if (OtherUtils.runningAsAdmin == true)
                                {
                                    Console.WriteLine("Cannot wipe");
                                    OtherUtils.PrintException(ex);
                                }
                                else
                                {
                                    Console.WriteLine("Cannot wipe (access denied)");
                                    OtherUtils.PrintException(ex);
                                }
                            }
                        }
                        if (result.Key == ConsoleKey.N)
                        {
                            Console.WriteLine("\nReturning to main prompt..");
                            Program.Prompt();
                        }
                        else
                        {
                            choice();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wiping all data in {0} .. (might freeze on larger files)", filename);
                    try
                    {
                        System.IO.File.WriteAllBytes(filename, new byte[0]);
                        Console.WriteLine("Sucessfully wiped all data from file..");
                        Program.Prompt();
                    }
                    catch (Exception ex)
                    {
                        if (OtherUtils.runningAsAdmin == true)
                        {
                            Console.WriteLine("Cannot wipe");
                            OtherUtils.PrintException(ex);
                        }
                        else
                        {
                            Console.WriteLine("Cannot wipe (access denied)");
                            OtherUtils.PrintException(ex);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            Program.Prompt();
        }
        public static void ClearFilePath()
        {
            string filename = Program.command.Replace("file clr /p ", "");
            if (File.Exists(filename))
            {
                FileInfo fileinfo = new FileInfo(filename);
                var bytes = fileinfo.Length;
                var inMb = (bytes / 1024 / 1024);
                if (inMb > 10)
                {
                    Console.WriteLine("  Larger file size disclaimer");
                    Console.WriteLine("----------------------------------------------\n");
                    Console.WriteLine("  The file selected {0}", filename);
                    Console.WriteLine("  has the size of above 10 MB");
                    Console.WriteLine("  Actual size : {0} MB\n", inMb);
                    Console.WriteLine("   Are you sure?\n");
                    Console.Write("      (Y)es / (N)o  ");
                    choice();
                    void choice()
                    {
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\b\b");
                        if (result.Key == ConsoleKey.Y)
                        {
                            Console.WriteLine("\nWiping all data in {0}.. (might freeze on larger files)");
                            try
                            {
                                System.IO.File.WriteAllBytes(filename, new byte[0]);
                                Console.WriteLine("Sucessfully wiped all data from file..");
                                Program.Prompt();
                            }
                            catch (Exception ex)
                            {
                                if (OtherUtils.runningAsAdmin == true)
                                {
                                    Console.WriteLine("Cannot wipe");
                                    OtherUtils.PrintException(ex);
                                }
                                else
                                {
                                    Console.WriteLine("Cannot wipe (access denied)");
                                    OtherUtils.PrintException(ex);
                                }
                            }
                        }
                        if (result.Key == ConsoleKey.N)
                        {
                            Console.WriteLine("\nReturning to main prompt..");
                            Program.Prompt();
                        }
                        else
                        {
                            choice();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Wiping all data in {0} .. (might freeze on larger files)", filename);
                    try
                    {
                        System.IO.File.WriteAllBytes(filename, new byte[0]);
                        Console.WriteLine("Sucessfully wiped all data from file..");
                        Program.Prompt();
                    }
                    catch (Exception ex)
                    {
                        if (OtherUtils.runningAsAdmin == true)
                        {
                            Console.WriteLine("Cannot wipe");
                            OtherUtils.PrintException(ex);
                        }
                        else
                        {
                            Console.WriteLine("Cannot wipe (access denied)");
                            OtherUtils.PrintException(ex);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            Program.Prompt();
        }
        public static void CloneFile()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("file cln ", "");
            if (File.Exists(Program.currentdir + filename))
            {
                string extension = Path.GetExtension(Program.currentdir + filename);
                string filenoex = filename.Replace(extension, "");
                string clone1 = Path.Combine(Program.currentdir, filenoex + "_(copy)" + extension); 
                 
                try
                {
                    if (File.Exists(clone1))
                    {
                        Console.WriteLine(" File copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {

                        }
                        else
                        {
                            Program.Prompt();
                        }
                    }
                    else
                    {

                    }
                    File.Copy(Program.currentdir + filename, clone1);
                    Console.WriteLine("Sucessfully cloned file");
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not clone file, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.WriteLine("Could not clone file");
                        OtherUtils.PrintException(ex);
                    }                  
                }
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("File not found");
            }
            Program.Prompt();
        }
        public static void CloneFilePath()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("file cln /p ", "");
            if (File.Exists(filename))
            {
                string extension = Path.GetExtension(filename);
                string filenoex = filename.Replace(extension, "");
                string clone1 = Path.Combine(filenoex + "_(copy)" + extension);

                try
                {
                    if (File.Exists(clone1))
                    {
                        Console.WriteLine(" File copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {

                        }
                        else
                        {
                            Program.Prompt();
                        }
                    }
                    else
                    {

                    }
                    File.Copy(filename, clone1);
                    Console.WriteLine("Sucessfully cloned file");
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.WriteLine("Could not clone file, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.WriteLine("Could not clone file");
                        OtherUtils.PrintException(ex);
                    }
                }
                Program.Prompt();
            }
            else
            {
                Console.WriteLine("File not found");
            }
            Program.Prompt();
        }
        public static void RenameFile()
        {
            if (Program.currentdir == null)
            {
                FileUtils.NoDirSet();
            }
            string filename = Program.command.Replace("file rnm ", "");
            if (File.Exists(Program.currentdir + filename))
            {
                Console.WriteLine(" Enter new file name (extension included)");
                Console.WriteLine(" to cancel enter empty\n");
                Console.Write("   >");
                string newname = Console.ReadLine();
                Console.WriteLine();
                if (newname == null)
                {
                    Console.WriteLine("Returning to main prompt..");
                    Program.Prompt();
                }
                try
                {
                    File.Move(Program.currentdir + filename, Program.currentdir + newname);
                    Console.Write("Successfully renamed {0} to {1}", filename, newname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.Write("Could not rename file, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.Write("Could not rename file");
                        OtherUtils.PrintException(ex);
                    }
                }
            }
            else
            {
                Console.Write("File does not exist");
            }
            Program.Prompt();
        }
        public static void RenameFilePath()
        {
            string filename = Program.command.Replace("file rnm /p ", "");
            if (File.Exists(filename))
            {
                string filedir1 = Path.GetDirectoryName(filename);
                string filedir = filedir1 + @"\";
                
                Console.WriteLine(" Enter new file name (extension included)");
                Console.WriteLine(" to cancel enter empty\n");
                Console.Write("   >");
                string newname = Console.ReadLine();
                Console.WriteLine();
                if (newname == null)
                {
                    Console.WriteLine("Returning to main prompt..");
                    Program.Prompt();
                }
                try
                {
                    File.Move(filename, filedir + newname);
                    Console.WriteLine("Successfully renamed {0} to {1}", filename, newname);
                }
                catch (Exception ex)
                {
                    if (OtherUtils.runningAsAdmin == false)
                    {
                        Console.Write("Could not rename file, access denied");
                        OtherUtils.PrintException(ex);
                    }
                    else
                    {
                        Console.Write("Could not rename file");
                        OtherUtils.PrintException(ex);
                    }
                }
            }
            else
            {
                Console.Write("File does not exist");
            }
            Program.Prompt();
        }
    }
}