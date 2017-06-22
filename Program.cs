//HUY (HENRY) NGUYEN | HMN8@ZIPS.UAKRON.EDU | STUDENT ID:2653922
//UNIX SHELL COMMAND PROMPT
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace winsh
{
    class Program
    {
        static void Main(string[] args)
        {
            var unixShell = new winsh();
            unixShell.Run();//Run starts application
        }
    }


    public class winsh
    {
        public void Run()
        {

            try
            {
                //Get the current directory
                string path = Directory.GetCurrentDirectory();
                Console.WriteLine(path);
                Console.WriteLine("Type '?' For Command List.\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed to open current directory", e.ToString() );
            }

            string input = null;

            do
            {
                Console.Write("winsh>");
                input = Console.ReadLine();
                Execute(input);
            } while (input != "exit");
        }


        public int Execute(string input)//excute input commands
        {

            if (input.Contains("man "))
            {
                StringSearch(input);//look and execute for 'man' command
                return 0;
            }

            else if(input.Contains("grep "))
            {
                FileSearch(input);//look and execute for 'grep' command
                return 0;
            }

            else if(input.Contains("cp "))
            {
                copyFile(input);//look and executer for "cp" command
                return 0;
            }

            else if(input.Contains("more "))
            {
                readTxt(input);
                return 0;
            }

            else if(input.Contains("rm "))
            {
                delFile(input);
                return 0;
            }

            else
            {

                switch (input)//determines user input to execute commands
                {
                    case "ls"://list directory
                        ProcessDirectory();
                        return 0;
                    case "?"://list command menu info
                        Help();
                        return 0;
                    case "mkdir"://make new directory/Folder
                        newDir();
                        return 0;
                    case "date"://get date and time
                        Time();
                        return 0;
                    default:
                        break;
                }
            }
        
                Console.WriteLine($"{input} Command Not Found 1");
                return 1;
            
          

        }

        public static void ProcessDirectory()//ls function
        {
            string path = Directory.GetCurrentDirectory();
            string[] Files = Directory.GetFiles(path);

            foreach (string fileName in Files)
            {
                string clean;
                clean = Path.GetFileName(fileName);//cleans up directory path display
                Console.WriteLine(clean);

            }

            string[] dir = Directory.GetDirectories(path);
            foreach (string fileDir in dir)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(fileDir));
            }

        }

            public static void StringSearch(string input)
        {
            string Search;

            //removes "man" from search
            Search = (input.Substring(4, (input.Length) - 4));

            //explains commands
            switch(Search)
            {
                case "ls":
                    Console.WriteLine("'ls' lists files in current directory");
                    break;
                case "exit":
                    Console.WriteLine("'exit' terminates program");
                    break;
                case "grep":
                    Console.WriteLine("'grep' searches directory for files associated with key word input");
                    break;
                case "?":
                    Console.WriteLine("'?' HELP for list of commands and explanation on usage");
                    break;
                case "cp":
                    Console.WriteLine("'cp' copies file into subdirectory [Copy Dir]");
                    break;
                case "mkdir":
                    Console.WriteLine("'mkdir' creates a New File");
                    break;
                case "more":
                    Console.WriteLine("'more' reads contents of a Text File (.txt)");
                    break;
                case "date":
                    Console.WriteLine("'date' displays current date and time");
                    break;
                case "rm":
                    Console.WriteLine("'rm' deletes file");
                    break;
                default:
                    Console.WriteLine("COMMAND DOES NOT EXIST");
                    break;


            }
        }

        public static void FileSearch(string input)
        {
            //open directory for search
            string path = Directory.GetCurrentDirectory();
            string[] Files = Directory.GetFiles(path);
            string[] Dir = Directory.GetDirectories(path);
            string SearchPattern;
            bool NotFound = true;

            //isolate "grep" from command keyword
            SearchPattern = (input.Substring(5, input.Length - 5));


            foreach(string found in Files)
            {
                string clean;
                clean = Path.GetFileName(found);

                if (System.Text.RegularExpressions.Regex.IsMatch(clean, SearchPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    System.Console.Write(clean);
                    System.Console.WriteLine("  (match for '{0}' found)", SearchPattern);
                    NotFound = false;
                }

            }

            foreach (string found in Dir)
            {
                string clean;
                clean = Path.GetFileNameWithoutExtension(found);

                if (System.Text.RegularExpressions.Regex.IsMatch(clean, SearchPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    System.Console.Write(clean);
                    System.Console.WriteLine("  (match for '{0}' found)", SearchPattern);
                    NotFound = false;
                }

            }

            if (NotFound == true)
            {
                System.Console.WriteLine("NO MATCHES FOUND");
            }
        }

        public static void Help()
        {
            Console.WriteLine("'ls' lists files in current directory");
            Console.WriteLine("'exit' terminates program");
            Console.WriteLine("'grep' searches directory for files associated with key word input. Example: grep [input word]");
            Console.WriteLine("'man' explains the function of the command. Example: man [function input]");
            Console.WriteLine("'?' HELP for list of commands");
            Console.WriteLine("'cp' copies file into current directory. Example: cp [input file name]");
            Console.WriteLine("'mkdir' creates new directory [New Folder]");
            Console.WriteLine("'more' reads contents of a Text File. Example: more [input Text File]");
            Console.WriteLine("'date' displays current date and time");
            Console.WriteLine("'rm' deletes file. Example: rm [input file name]");


        }

        public static void copyFile(string input)
        {
            string path = Directory.GetCurrentDirectory();
            string[] Files = Directory.GetFiles(path);
            bool success = false;


            //cleanup input
            string Search = (input.Substring(3, input.Length - 3));
            
            foreach (string fileName in Files)
            {
                string clean;
                clean = Path.GetFileName(fileName);//cleans up directory path display


                if (clean == Search)
                {

                    Console.WriteLine("If File Exists, Overwrite File? (Y/N)");

                    string OverWrite;
                    Directory.CreateDirectory("Copy Dir");//creat Folder to copy file to

                    string origin = (Path.Combine(path, clean));//origin folder
                    string des = (Path.Combine(path, "Copy Dir", clean));//destination folder

                    OverWrite = Console.ReadLine();

                    
                    if (OverWrite == "Y")
                    {
                        try
                        {
                            File.Copy(origin,des, true);
                            success = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("ERROR CANNOT COPY FILE");
                        }
                        success = true;
                    }
                    else if (OverWrite == "N")
                    { Console.WriteLine("Copy Canceled..."); }
                    else
                    { Console.WriteLine("Command Not Recognized. Copy Failed..."); }

    
                }
            }

            if (success == false)
            {
                Console.WriteLine("Cannot Copy Directory or File Not Found");
            }

        }
   
        

        public static void newDir()
        {
            Directory.CreateDirectory("New Folder");
        }

        public static void readTxt(string input)
        {
            string Search = (input.Substring(5, input.Length - 5));

            try
            {
                StreamReader file = new StreamReader(Search);
                string Line;

                Console.Write("--Start--");Console.WriteLine(Search);

                while ((Line = file.ReadLine()) != null)
                {
                    Console.Write("~"); Console.Write(Line); Console.WriteLine("");
                }

                Console.WriteLine("\n--END OF FILE");
            }
            catch (Exception)
            {
                Console.WriteLine("Error Openning File or File Does Not Exist...");
            }
        }

        public static void Time()
        {
            string Text = System.DateTime.Now.ToString("hh:mm tt  ");
            string Date = System.DateTime.Now.ToShortDateString();
            Console.Write(Text); Console.WriteLine(Date);
        }

        public static void delFile(string input)
        {
            string path = Directory.GetCurrentDirectory();
            string[] Files = Directory.GetFiles(path);

            bool success = false;

            string Search;

            //removes "rm" from search
            Search = (input.Substring(3, (input.Length) - 3));

            foreach(string fileName in Files)
            {
                string clean = fileName.Substring(path.Length + 1);

                if(clean == Search)
                {
                    try
                    {
                        File.Delete(fileName);
                        //success = true;
                        break;
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("ERROR Deleting File or File Does Not Exist...");
                    }
                }
            }

            if(success == false)
            {
                Console.WriteLine("Delete Failed. File Not Found...");
            }

        }



    }
}
