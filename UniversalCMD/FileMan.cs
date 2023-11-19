using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using static Community.CsharpSqlite.Sqlite3;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UniCMD
{
    internal class FileMan
    {
        //dir
        public static void SetDirectory()
        {
            string dir = Program.UserCommand.Replace("sd ", "");
            if (Program.CurrentDir != null && dir == "..")
            {
                try
                {
                    string backonedir = Directory.GetParent(Program.CurrentDir).ToString();
                    string backtwodir = Directory.GetParent(backonedir).ToString();
                    Program.CurrentDir = backtwodir;
                    if (!Program.CurrentDir.EndsWith(@"\"))
                        Program.CurrentDir += @"\";
                    Console.WriteLine("Successfully edited directory");
                    Console.WriteLine(" " + Program.CurrentDir);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot edit directory");
                    Other.PrintException(ex);
                }
                return;
            }

            if (!dir.EndsWith(@"\"))
            {
                dir += @"\";
            }

            if (Program.CurrentDir == null && Directory.Exists(dir))
            {
                Program.CurrentDir = dir;

                Console.WriteLine("Successfully set directory");
                Console.WriteLine(" " + Program.CurrentDir);
                return;
            }
            if (Program.CurrentDir != null && Directory.Exists(Program.CurrentDir + dir))
            {
                Program.CurrentDir = Program.CurrentDir + dir;
                Console.WriteLine("Successfully edited directory");
                Console.WriteLine(" " + Program.CurrentDir);
                return;
            }
            else
            {
                Console.WriteLine("Cannot access directory.");
            }

            /* Old version
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
            */
        }
        public static void ClearSetDirectory()
        {
            Program.CurrentDir = null;
            Console.WriteLine("Cleared set directory");
        }
        public static void ListDir()
        {
            if (Program.CurrentDir == null)
            {
                FileMan.NoDirSet();
            }
            else
            {
                Console.WriteLine("  Contents of " + Program.CurrentDir);
                Console.WriteLine("----------------------------------------------");
                var directories = Directory.GetDirectories(Program.CurrentDir);
                foreach (var d in directories)
                {
                    Console.WriteLine(" " + d + @"\");
                }
                var files = Directory.GetFiles(Program.CurrentDir);
                foreach (var d in files)
                {
                    Console.WriteLine(" " + d);
                }
            }
        }
        public static void NoDirSet()
        {
            Console.WriteLine("In order to use this command set a directory first");
            Console.WriteLine("Using the 'sd' command");
            Program.Prompt();
        }
        public static void CreateDir()
        {
            // this is how we ball
            var dirname = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            
            if (dirname.StartsWith("/p "))
            {
                dirname = dirname.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                dirname = Program.CurrentDir + dirname;
            }

            try
            {
                if (Directory.Exists(dirname))
                {
                    Console.WriteLine("Directory already exists");
                    return;
                }
                Directory.CreateDirectory(dirname);
                Console.WriteLine("Directory created from path");
                Console.WriteLine(" " + dirname);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not create directory.");
                Other.PrintException(ex);
            }
        }
        public static void DeleteDir()
        {
            var dirname = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));

            if (dirname.StartsWith("/p "))
            {
                dirname = dirname.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                dirname = Program.CurrentDir + dirname;
            }

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
                    Console.WriteLine("Cannot delete directory");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("Directory does not exist");
            }
        }
        public static void CloneDir()
        {
            var dirname = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            bool overwrite = false;
            if (dirname.StartsWith("/p "))
            {
                dirname = dirname.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                dirname = Program.CurrentDir + dirname;
            }

            if (dirname.Contains(" /frc"))
            {
                dirname = dirname.Replace(" /frc", "");
                overwrite = true;
            }

            if (Directory.Exists(dirname))
            {
                try
                {
                    string clone = dirname + "_clone";
                    if (Directory.Exists(clone) && !overwrite)
                    {
                        Console.WriteLine(" Directory copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {
                            overwrite = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(dirname, clone, overwrite);
                    Console.WriteLine("Sucessfully cloned directory");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not clone directory");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("Directory not found");
            }
        }
        public static void RenameDir()
        {
            var dirname = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (dirname.StartsWith("/p "))
            {
                dirname = dirname.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                dirname = Program.CurrentDir + dirname;
            }

            if (dirname.Contains(" /name "))
            {
                dirname = dirname.Split(" /name ")[0];
            }
            string[] newname = Program.UserCommand.Split(" /name ");

            if (Directory.Exists(dirname))
            {
                if (newname.Length > 1)
                {
                    try
                    {
                        Directory.Move(dirname, Path.GetDirectoryName(dirname) + "\\" + newname[1]);
                        Console.Write("Successfully renamed {0} to {1}", dirname, newname[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Could not rename directory");
                        Other.PrintException(ex);
                    }
                }
                else
                {
                    Console.WriteLine("No new name provided (/name argument)");
                    return;
                }
            }
            else
            {
                Console.Write("Directory does not exist");
            }
        }

        //file
        public static void CreateFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }

            if (File.Exists(filename))
            {
                Console.WriteLine("File already exists.");
            }
            else
            {
                try
                {
                    File.Create(filename).Close();
                    Console.WriteLine("Created file from path");
                    Console.WriteLine(" " + filename);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot create file..");
                    Other.PrintException(ex);
                }
            }
        }
        public static void DeleteFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                    Console.WriteLine("Deleted file from current");
                    Console.WriteLine(" " + filename);
                }
                catch (Exception ex)
                {
                    if (Other.IsAdmin == false) // lololoololoolol this is so retarded why did i even write this, anyway its staying like that
                    {
                        Console.WriteLine("Could not delete file, access denied");
                        Other.PrintException(ex);
                        return;
                    }
                    Console.WriteLine("Cannot delete file");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void ReadFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }

            if (File.Exists(filename))
            {
                string file = File.ReadAllText(filename, Encoding.UTF8);

                Console.WriteLine("  Contents of " + filename);
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine(file);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void WriteFile()
        {
            // use file wrtln instead of this for uniscript
            
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }

            if (File.Exists(filename))
            {
                Console.WriteLine("  Writing to " + filename);
                Console.WriteLine(" To stop writing enter '__!!stop'");
                Console.WriteLine("----------------------------------------------");
                foreach (string fileline in File.ReadAllLines(filename))
                {
                    Console.WriteLine(fileline);
                }
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
                            return;
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
                        Other.PrintException(ex);
                    }
                }
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void WriteLineFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }

            if (filename.Contains(" /s "))
            {
                filename = filename.Split(" /s ")[0];
            }
            else
            {
                Console.WriteLine("No input string provided (/s)");
                return;
            }
            string[] userstring = Program.UserCommand.Split(" /s "); // userstring[1] is the user input

            if (File.Exists(filename))
            {
                using (StreamWriter sw = File.AppendText(filename)) 
                {
                    sw.WriteLine(userstring[1]);
                    sw.Close();
                }
                Console.WriteLine("Wrote '{0}' to '{1}'", userstring[1], filename);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void ClearFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            bool skipask = false;
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }
            if (filename.Contains(" /frc"))
            {
                filename = filename.Replace(" /frc", "");
                skipask = true;
            }
            if (File.Exists(filename))
            {
                FileInfo fileinfo = new FileInfo(filename);
                var bytes = fileinfo.Length;
                var inMb = (bytes / 1024 / 1024);
                if (inMb > 10  && !skipask)
                {
                    Console.WriteLine("  Larger file size disclaimer");
                    Console.WriteLine("----------------------------------------------\n");
                    Console.WriteLine("  The file selected {0}", filename);
                    Console.WriteLine("  has the size of above 10 MB");
                    Console.WriteLine("  Actual size : {0} MB\n", inMb);
                    Console.WriteLine("   Are you sure?\n");
                    Console.Write("      (Y)es / (N)o  ");
                    ConsoleKeyInfo result = Console.ReadKey();
                    Console.WriteLine("\n");
                    if (result.Key == ConsoleKey.Y)
                    {
                        
                    }
                    else
                    {
                        return;
                    }
                }

                Console.WriteLine("Wiping all data in {0} ..", filename);
                try
                {
                    System.IO.File.WriteAllBytes(filename, new byte[0]);
                    Console.WriteLine("Sucessfully wiped all data from file..");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Cannot wipe");
                    Other.PrintException(ex);
                }

            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        public static void CloneFile()
        {
            var filename = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            bool overwrite = false;
            if (filename.StartsWith("/p "))
            {
                filename = filename.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                filename = Program.CurrentDir + filename;
            }

            if (filename.Contains(" /frc"))
            {
                filename = filename.Replace(" /frc", "");
                overwrite = true;
            }

            if (File.Exists(filename))
            {            
                try
                {
                    string clone = Path.GetFileNameWithoutExtension(filename) + "_clone" + Path.GetExtension(filename);

                    if (File.Exists(Path.GetDirectoryName(filename) + "\\" + clone) && !overwrite)
                    {
                        Console.WriteLine(" File copy already exists, overwrite?\n");
                        Console.Write("  (Y)es / (N)o ");
                        ConsoleKeyInfo result = Console.ReadKey();
                        Console.WriteLine("\n");
                        if (result.Key == ConsoleKey.Y)
                        {
                            overwrite = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                    File.Copy(filename, Path.GetDirectoryName(filename) + "\\" + clone, overwrite);
                    Console.WriteLine("Sucessfully cloned file");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Could not clone file");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("File not found");
            }
        }
        public static void RenameFile()
        {
            var file = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (file.StartsWith("/p "))
            {
                file = file.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                file = Program.CurrentDir + file;
            }

            if (file.Contains(" /name "))
            {
                file = file.Split(" /name ")[0];
            }
            string[] newname = Program.UserCommand.Split(" /name ");

            if (File.Exists(file))
            {
                if (newname.Length > 1) 
                {
                    try
                    {
                        File.Move(file, Path.GetDirectoryName(file) + "\\" + newname[1]);
                        Console.Write("Successfully renamed {0} to {1}", file, newname[1]);
                    }
                    catch (Exception ex)
                    {
                        Console.Write("Could not rename file");
                        Other.PrintException(ex);
                    }
                }
                else
                {
                    Console.WriteLine("No new name provided (/name argument)");
                    return;
                }
                Console.WriteLine();
            }
            else
            {
                Console.Write("File does not exist");
            }
        }
        public static void ZipFile()
        {
            var dirname = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (dirname.StartsWith("/p "))
            {
                dirname = dirname.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                dirname = Program.CurrentDir + dirname;
            }

            try
            {
                System.IO.Compression.ZipFile.CreateFromDirectory(dirname, dirname + ".zip");
                Console.WriteLine("Created zip archive at current");
                Console.WriteLine(" " + dirname + ".zip");
            }
            catch(Exception ex)
            {
                Console.Write("Could not create zip archive");
                Other.PrintException(ex);
            }
        }
        public static void UnzipFile()
        {
            var file = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (file.StartsWith("/p "))
            {
                file = file.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                file = Program.CurrentDir + file;
            }

            if (File.Exists(file))
            {
                try
                {
                    Directory.CreateDirectory(file + "_extracted");
                    System.IO.Compression.ZipFile.ExtractToDirectory(file, file + "_extracted");

                    Console.WriteLine("Successfully unzipped file to");
                    Console.WriteLine(" " + file + "_extracted");
                }
                catch (Exception ex)
                {
                    Directory.Delete(file + "_extracted");
                    Console.Write("Could not unzip archive");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.Write("File does not exist");
            }
        }
    }
}
