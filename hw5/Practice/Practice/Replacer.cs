using System;
using System.IO;
using System.Text;

namespace PracticeReplacer
{
    class Replacer
    {
        public static string[] ReplaceIn(string path)
        {
            string[] text = File.ReadAllLines(path);
            int gratesCounter = 0;

            foreach (var s in text)
            {
                if (string.IsNullOrEmpty(s))
                {
                    throw new ArgumentException("One of strings is empty or null");
                }
                gratesCounter += GetNumberOf(s, "#");
            }

            if (gratesCounter % 2 == 1)
            {
                throw new ArgumentException("Odd number of #. Unable to place < and > correctly");
            }

            int midVal = gratesCounter / 2;
            var result = text;

            int iterationCounter = 0;
            foreach (var s in result)
            {
                int pos = s.IndexOf("#");
                StringBuilder sb = new (s);
                while ( pos != -1 )
                {
                    sb[pos] = (gratesCounter > midVal) ? '<' : '>';
                    result[iterationCounter] = sb.ToString();
                    gratesCounter--;
                    pos = sb.ToString().IndexOf("#");
                }
                iterationCounter++;
            }
            return result;
        }

        private static int GetNumberOf(string s, string sub)
        {
            string[] words = s.Split(sub);
            return words.Length - 1;
        }

        public static string PrintStringArray(string[] text)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in text)
            {
                sb.Append(s);
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}