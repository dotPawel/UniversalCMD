using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UniCMD
{
    internal class UserData
    {
        public static string UnscUserArg;
        public static Dictionary<string, string> StringDictionary = new Dictionary<string, string>() {
            {"test_string", "foo"}
        
        };

        // user input utils
        public static void ClearUserInput()
        {
            UserData.UnscUserArg = null;
            Console.WriteLine("[usrin] clear");
        }
        public static void SetUserInput()
        {
            var usrinp = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            UserData.UnscUserArg = usrinp;
            Console.WriteLine("[usrin] set user argument to : " + usrinp);
        }
        public static void ReplaceUserInput()
        {
            var usrinp = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));

            if (usrinp.Contains(" /in "))
            {
                usrinp = usrinp.Split(" /in ")[0];
            }
            string[] newname = Program.UserCommand.Split(" /in "); // newname[1] is za0-sdjfzaoidfsjgoidmfxoigmodsxfmgoim[lo,

            UserData.UnscUserArg = usrinp.Replace(usrinp, newname[1]);
            Console.WriteLine("[usrin] replaced {0} with {1}", usrinp, newname[1]);
        }
        public static void ToLowerUserInput()
        {
            UserData.UnscUserArg = UserData.UnscUserArg.ToLower();
            Console.WriteLine("[usrin] ToLower : " + UserData.UnscUserArg);
        }
        public static void ToUpperUserInput()
        {
            UserData.UnscUserArg = UserData.UnscUserArg.ToUpper();
            Console.WriteLine("[usrin] ToUpper : " + UserData.UnscUserArg);
        }
        public static void ReadUserInput()
        {
            Console.Write(">");
            UserData.UnscUserArg = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("[usrin] set user argument to : " + UserData.UnscUserArg);
        }
        public static void ReadFileUserInput()
        {
            var path = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (path.StartsWith("/p "))
            {
                path = path.Replace("/p ", "");
            }
            else
            {
                if (Program.CurrentDir == null)
                {
                    FileMan.NoDirSet();
                }
                path = Program.CurrentDir + path;
            }

            if (File.Exists(path))
            {
                try
                {
                    UserData.UnscUserArg = File.ReadAllText(path);
                    Console.WriteLine("[usrin] set user argument to contents of : " + path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[usrin] cannot read file");
                    Other.PrintException(ex);
                }
            }
            else
            {
                Console.WriteLine("[usrin] file does not exist");
            }

        }

        // string dictionary
        public static void PrintDictionary()
        {
            Console.WriteLine("  String dictionary data");
            Console.WriteLine("----------------------------------------------");
            foreach (var key in StringDictionary.Keys)
            {
                Console.WriteLine(key + " -> " + StringDictionary[key]);
            }
        }
        public static void AddDictionaryKey()
        {
            var name = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (name.Contains(" /in "))
            {
                name = name.Split(" /in ")[0];
            }
            else
            {
                Console.WriteLine("[dict] no key value provided (/in argument)");
                return;
            }

            try
            {
                string value = Program.UserCommand.Split(" /in ")[1];

                StringDictionary.Add(name, value);
                Console.WriteLine("[dict] added key, name : {0}, value : {1}", name, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[dict] cannot create key");
                Other.PrintException(ex);
            }
        }
        public static void SetDictionaryKey()
        {
            var key = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            if (key.Contains(" /in "))
            {
                key = key.Split(" /in ")[0];
            }
            else
            {
                Console.WriteLine("[dict] no key value provided (/in argument)");
                return;
            }

            try
            {
                string[] newvalue = Program.UserCommand.Split(" /in ");
                StringDictionary[key] = newvalue[1];
                Console.WriteLine("[dict] set new key value, key : " + key + ", value : " + newvalue[1]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[dict] cannot set key");
                Other.PrintException(ex);
            }
        }
        public static void RemoveDictionaryKey()
        {
            var name = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));

            if (StringDictionary.ContainsKey(name))
            {
                StringDictionary.Remove(name);
                Console.WriteLine("[dict] removed key : " + name);
            }
            else
            {
                Console.WriteLine("[dict] key not found : " + name);
            }
        }
        public static void ClearDictionary()
        {
            StringDictionary.Clear();
            Console.WriteLine("[dict] dictionary cleared");
        }
    }
}
