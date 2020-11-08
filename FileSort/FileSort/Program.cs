using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace FileSort
{
    class Program
    {
        static string newFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\SortedFile.txt";
        static string FilePath = AppDomain.CurrentDomain.BaseDirectory + @"\TempFile2.txt";
        static Char[] charsToReplace = { ' ' };
        static Char[] CharsToDelete = { '.', ',', '?', '!', '[', ']', '{', '}', '(', ')', '"', '\'', ' ' };
        static Stopwatch sp = new Stopwatch();
        /*static Dictionary<char, Dictionary<char, List<string>>> Dict = new Dictionary<char, Dictionary<char, List<string>>>{ 
                                                                        { 'a', new Dictionary<char, List<string>>
                                                                                    { 'a', new List<string>() }
                                                                        }
        };
*/
        //static Dictionary<char, List<string>> yo = new Dictionary<char, List<string>>{ 'a', new List<string>()};

    static void Main(string[] args)
        {
            sp.Start();
            using (var writer = new StreamWriter(File.CreateText(newFilePath).BaseStream))
            {
                using (var reader = new StreamReader(FilePath))
                {
                    Console.SetOut(writer);
                    Console.SetIn(reader);
                    string line;
                    string[] splitContent;
                    line = reader.ReadToEnd();

                    string content = new string(line.AsParallel().Select(c => charsToReplace.Contains(c) ? '\n' : CharsToDelete.Contains(c) ? '\0' : c ).ToArray()); // Remove or replace characters with \n when necessary

                    splitContent = content.Split('\n');
                    splitContent = splitContent.AsParallel().OrderBy(c => c).ToArray();
                    Console.WriteLine(String.Join("\n", splitContent));
                    sp.Stop();
                    Console.WriteLine(string.Format("Time to sort {0} ms", sp.ElapsedMilliseconds));
                }
            }
        }
    }
}
