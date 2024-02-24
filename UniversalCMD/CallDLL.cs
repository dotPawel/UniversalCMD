using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Ionic.BZip2;
using IronPython.Runtime;
using IronPython.Runtime.Operations;
using Microsoft.VisualBasic;

namespace UniCMD
{
    static internal class CallDLL
    {
        public static string Version = "1.0";
        public static string LoadedDLLPath;

        public static void LoadDLL(){
            var file = string.Join(" ", Program.UserCommand.Split(' ').Skip(2)); // kendo thugssssssssssssssssssssssssssssssssssssssssssssss

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

            if (File.Exists(file)){
                LoadedDLLPath = file;
                Console.WriteLine("[cdll] DLL loaded");
            }
            else{
                Console.WriteLine("[cdll] file does not exist");
            }
        }
        public static void UnloadDLL(){
            LoadedDLLPath = null;
            Console.WriteLine("[cdll] dll unloaded");
        }
        public static void InvokeDLL(){
            
            if (LoadedDLLPath == null){
                Console.WriteLine("[cdll] no dll loaded");
                return;
            }

            bool UseUserArg = false;
            var input = string.Join(" ", Program.UserCommand.Split(' ').Skip(2));
            string[] parts = input.Split(' ');

            if (parts.Length >= 3)
            {
                string nspace = parts[0];
                string classname = parts[1];
                string voidname = parts[2];

                if (parts.Contains("/usrin")){
                    UseUserArg = true;
                    Console.WriteLine("[cdll] using user argument");
                }

                try{
                    Console.WriteLine("[cdll] loading assembly types");
                    Assembly assembly = Assembly.LoadFrom(LoadedDLLPath);

                    Type type = assembly.GetType($"{nspace}.{classname}");

                    if (type != null)
                    {
                        object instance = Activator.CreateInstance(type);
                        MethodInfo method = type.GetMethod(voidname);

                        if (method != null)
                        {
                            ParameterInfo[] parameters = method.GetParameters();

                            if (parameters.Length == 1)
                            {
                                if (UseUserArg){
                                    Console.WriteLine($"[cdll] invoking {classname}.{voidname} with '" + UserData.UnscUserArg + "' as parameter");
                                    method.Invoke(instance, new object[] { UserData.UnscUserArg });
                                } else{
                                    Console.WriteLine($"[cdll] method '{voidname}' requires a parameter");
                                    return;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"[cdll] invoking {classname}.{voidname}");
                                method.Invoke(instance, null);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[cdll] method '{voidname}' not found in class '{classname}'");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[cdll] '{classname}' not found in namespace '{nspace}'");
                    }
                }
                catch (Exception ex){
                    Console.WriteLine("[cdll] invoking dll failed");
                    Other.PrintException(ex);
                }
                
            }
            else
            {
                Console.WriteLine("[cdll] invalid input, not all arguments provided");
            }  
        }
    }
}