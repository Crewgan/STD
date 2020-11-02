using System;
using System.Collections.Generic;

namespace DictionnaryGenerator
{
    class Program
    {

        static int nbDictionaries = 3;
        static char[] alphabet = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        static void Main(string[] args)
        {
            Console.WriteLine(CreateDictionaires(nbDictionaries, "Dict", ""));
        }

        public static string CreateDictionaires(int nb, string dictName, string tab)
        {
            //static Dictionary<char, Dictionary> " + 
            string result = "";
            string thisDictName;
            for (int j = 0; j < alphabet.Length; j++)
            {
                thisDictName = tab + dictName + alphabet[j];
                result += Environment.NewLine + thisDictName + " = {";
                nb--;
                for (int i = 0; i < nb; i++)
                {
                    if (nb > 0)
                    {
                        tab += "\t";
                        result += tab + CreateDictionaires(nb, thisDictName, tab) + " };";
                    }
                    else
                    {
                        result += "finish";
                    }
                }
            }
            return result;
        }
    }
}
