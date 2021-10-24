using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Task3
{
    class TextWorker
    {
        public List<string> Sentences { get; }

        public TextWorker()
        {
            Sentences = new List<string>();
        }
        public TextWorker(string path)
        {
            Sentences = new List<string>();
            GetSentencesFromFile(path);
        }

        public void GetSentencesFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("File doesn't exist");
            }

            if (new FileInfo(path).Extension != ".txt")
            {
                throw new ArgumentOutOfRangeException($"{nameof(path)}", path, "Only .txt files are allowed");
            }

            var text = File.ReadAllLines(path);

            var sentenceToAdd = "";

            foreach (var line in text)
            {
                string[] splitLine = line.Split('.', StringSplitOptions.RemoveEmptyEntries);
                for (var index = 0; index < splitLine.Length - 1; index++)
                {
                    sentenceToAdd += splitLine[index];
                    sentenceToAdd += ".";
                    Sentences.Add(sentenceToAdd);
                    sentenceToAdd = "";
                }

                sentenceToAdd = splitLine[^1];

            }
            Sentences.Add(sentenceToAdd);

        }

        public string GetSentenceDeepestNesting()
        {
            if (Sentences.Count == 0)
            {
                throw new InvalidOperationException(
                    "Cant find sentence with deepest nesting because current sentence list is empty");
            }

            int maxDepth = 0, maxDepthIndex = 0;
            for (int i = 1; i < Sentences.Count; ++i)
            {
                Stack<char> parenthesesStack = new Stack<char>();
                int currentDepth = 0, maxCurrentDepth = 0;
                foreach (var symbol in Sentences[i])
                {
                    if (symbol == '(')
                    {
                        parenthesesStack.Push('(');
                        ++currentDepth;
                    }
                    else if (symbol == ')')
                    {
                        if (parenthesesStack.Pop() != '(') continue;
                        if (maxCurrentDepth < currentDepth)
                        {
                            maxCurrentDepth = currentDepth;
                        }

                        --currentDepth;
                        if (currentDepth < 0)
                        {
                            break;
                        }
                    }
                }

                if (maxCurrentDepth <= maxDepth) continue;
                maxDepth = maxCurrentDepth;
                maxDepthIndex = i;
            }
            return Sentences[maxDepthIndex];
        }

        public override string ToString()
        {
            StringBuilder result = new();
            foreach (var sentence in Sentences)
            {
                result.Append(sentence + "\n");
            }
            return result.ToString();
        }

        public void SortSentencesByLength()
        {
            Sentences.Sort((string1, string2) => string1.Length - string2.Length);
        }
    }
}