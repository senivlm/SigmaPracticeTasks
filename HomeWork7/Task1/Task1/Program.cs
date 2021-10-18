using System;
using System.ComponentModel.DataAnnotations;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            var testDict = ReplaceDictionary.GenerateDefaultDictionary();
            //var testDict = new ReplaceDictionary();
            string input = "I go to school. Girl runs to school.";
            var res = testDict.ReplaceWords(input);
            Console.WriteLine(res);
        }
    }
}
