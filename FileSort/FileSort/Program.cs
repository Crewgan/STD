using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace FileSort
{
    class Program
    {
        static string newFilePath = AppDomain.CurrentDomain.BaseDirectory + @"\SortedFile.txt";
        static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\allo.txt"; // Modifier lorem5.txt
        static Char[] charsToReplace = { ' ' };
        static string[] alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", };
        static Stopwatch sp = new Stopwatch();
        static ConcurrentDictionary<string, ConcurrentDictionary<string, int>> dict = new ConcurrentDictionary<string, ConcurrentDictionary<string, int>>();
        static void Main(string[] args)
        {
            dict.TryAdd("a", new ConcurrentDictionary<string, int>());
            dict.TryAdd("b", new ConcurrentDictionary<string, int>());
            dict.TryAdd("c", new ConcurrentDictionary<string, int>());
            dict.TryAdd("d", new ConcurrentDictionary<string, int>());
            dict.TryAdd("e", new ConcurrentDictionary<string, int>());
            dict.TryAdd("f", new ConcurrentDictionary<string, int>());
            dict.TryAdd("g", new ConcurrentDictionary<string, int>());
            dict.TryAdd("h", new ConcurrentDictionary<string, int>());
            dict.TryAdd("i", new ConcurrentDictionary<string, int>());
            dict.TryAdd("j", new ConcurrentDictionary<string, int>());
            dict.TryAdd("k", new ConcurrentDictionary<string, int>());
            dict.TryAdd("l", new ConcurrentDictionary<string, int>());
            dict.TryAdd("m", new ConcurrentDictionary<string, int>());
            dict.TryAdd("n", new ConcurrentDictionary<string, int>());
            dict.TryAdd("o", new ConcurrentDictionary<string, int>());
            dict.TryAdd("p", new ConcurrentDictionary<string, int>());
            dict.TryAdd("q", new ConcurrentDictionary<string, int>());
            dict.TryAdd("r", new ConcurrentDictionary<string, int>());
            dict.TryAdd("s", new ConcurrentDictionary<string, int>());
            dict.TryAdd("t", new ConcurrentDictionary<string, int>());
            dict.TryAdd("u", new ConcurrentDictionary<string, int>());
            dict.TryAdd("v", new ConcurrentDictionary<string, int>());
            dict.TryAdd("w", new ConcurrentDictionary<string, int>());
            dict.TryAdd("x", new ConcurrentDictionary<string, int>());
            dict.TryAdd("y", new ConcurrentDictionary<string, int>());
            dict.TryAdd("z", new ConcurrentDictionary<string, int>());

            
            using (var writer = new StreamWriter(File.CreateText(newFilePath).BaseStream))
            {
                using (var reader = new StreamReader(filePath))
                {
                    string[] splitContent;

                    sp.Start();
                    Console.SetIn(reader);
                    // Lecture + découpage
                    Parallel.For(0, Int32.MaxValue, (i, state) => {
                        string line;
                        if ((line = Console.ReadLine()) != null)
                        {
                            line = new string(line.Select(c => charsToReplace.Contains(c) ? '\n' : Char.IsPunctuation(c) ? '\0' : c).ToArray());
                            splitContent = line.Split('\n');
                            Parallel.ForEach(splitContent, content =>
                            {
                                if (content != "")
                                {
                                    string firstChar = content.Substring(0, 1).ToLower();
                                    if (dict.ContainsKey(firstChar))
                                    {

                                        dict[firstChar].AddOrUpdate(content, 1, (key, oldValue) => oldValue + 1);
                                    }
                                }
                            });
                        }
                        else
                            state.Break();
                    });

                    sp.Stop();
                    Console.WriteLine(string.Format("Time to Read {0} ms", sp.ElapsedMilliseconds));
                    sp.Restart();

                    // Trie + Écriture
                    Console.SetOut(writer);
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        foreach (KeyValuePair<string, int> letter in dict[alphabet[i]].OrderBy(letter => letter.Key))
                        {
                            Parallel.For(0, letter.Value, index => {
                                Console.WriteLine(letter.Key);
                            });
                        }
                        dict[alphabet[i]] = null;
                    }

                    sp.Stop();
                    StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                    standardOutput.AutoFlush = true;
                    Console.SetOut(standardOutput);
                    Console.WriteLine(string.Format("Time to Write / Sort {0} ms", sp.ElapsedMilliseconds));
                }
            }
        }
    }
}
