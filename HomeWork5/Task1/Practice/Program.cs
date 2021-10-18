using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PracticeReplacer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Console.WriteLine("Enter path to input file\n");
            string path = Console.ReadLine();

            Console.WriteLine(Replacer.PrintStringArray(Replacer.ReplaceIn(path)));
        }
    }
}
