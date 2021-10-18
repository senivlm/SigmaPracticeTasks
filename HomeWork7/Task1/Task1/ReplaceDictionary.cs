using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Task1
{
    class ReplaceDictionary
    {
        private Dictionary<string, string> _replaceData;

        public ReplaceDictionary()
        {
            _replaceData = new Dictionary<string, string>();
        }

        public static ReplaceDictionary GenerateDefaultDictionary()
        {
            var dict = new ReplaceDictionary
            {
                ["I"] = "Boy",
                ["go"] = "run",
                ["to"] = "to",
                ["school"] = "cinema"
            };
            return dict;
        }
        public string this[string stringIndex]
        {
            get
            {
                if (_replaceData.ContainsKey(stringIndex))
                {
                    return _replaceData[stringIndex];
                }

                throw new IndexOutOfRangeException();
            }
            set
            {
                if (_replaceData.ContainsKey(stringIndex))
                {
                    throw new ArgumentException($"Dictionary already contains element {stringIndex}");
                }
                _replaceData.Add(stringIndex, value);
            }
        }

        public string ReplaceWords(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text), "Argument is empty or null string");
            }
            
            MatchCollection matchWords = Regex.Matches(text, @"\w+");
            List<string> words = matchWords.Select(m => m.Value).ToList();
            foreach (var word in words)
            {
                if (!_replaceData.ContainsKey(word))
                {
                    Console.WriteLine($"Enter replacement word for \" {word} \"");
                    string newRecord;
                    while ((newRecord = Console.ReadLine()).Length == 0) ;
                    _replaceData[word] = newRecord;

                }
                text = text.Replace(word, _replaceData[word]);
            }

            return text;
        }
    }
}