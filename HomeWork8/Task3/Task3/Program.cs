using System;
using System.Text.RegularExpressions;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string delimiter = new('-', 100);
                TextWorker tw =
                    new(@"C:\Users\mykha\Desktop\Sigma_School\homework\HomeWork8\Task3\Task3\text.txt");

                Console.WriteLine("Sentences from file:\n" + tw);
                Console.WriteLine(delimiter + "\nSentence with deepest nesting:\n" + tw.GetSentenceDeepestNesting());
                tw.SortSentencesByLength();
                Console.WriteLine(delimiter + "\nSentences sorted by length:\n" + tw);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
