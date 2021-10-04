using System;
using System.IO;

namespace hw3
{
    class FileWorker
    {
        string _path;

        public string Path
        {
            get => _path; set
            {
                if (String.Compare(value, "") != 0)
                {
                    _path = value;
                }
                else
                {
                    throw new ArgumentException("Path can't be empty");
                }
            }
        }

        public FileWorker(string path)
        {
            Path = path;
        }

        public int GetNumberFlat()
        {
            using (StreamReader file = new StreamReader(Path))
            {
                string data = file.ReadLine();
                string[] info = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(info[0], out int result);
                if (result < 1)
                {
                    throw new ArgumentException("Number of flats can't be negative number. Please, check your file");
                }
                return result;
            }

        }

        public int GetQuarter()
        {
            using (StreamReader file = new StreamReader(Path))
            {
                string data = file.ReadLine();
                string[] info = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(info[1], out int result);
                if (result < 1 || result > 4)
                {
                    throw new ArgumentException("Quarter number isn't correct. Allowed values: 1, 2, 3, 4\nPlease, check your file");
                }
                return result;
            }
        }

        public string[] GetListOfCounters()
        {
            using (StreamReader file = new StreamReader(Path))
            {
                string data = file.ReadLine();
                string[] info = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(info[0], out int flatNumber);
                string[] result = new string[flatNumber];
                for (int i = 0; i < flatNumber; ++i)
                {
                    result[i] = file.ReadLine();
                }
                return result;
            }
        }
    }
}
