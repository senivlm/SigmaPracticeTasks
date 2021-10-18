using System;

namespace practicePath
{
    class PathWorker
    {
        private string _path;

        public string Path
        {
            get => _path;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _path = value;
                }
                else
                {
                    throw new ArgumentException("String is null or empty");
                }
            }
        }

        public string GetRootDir()
        {
            int pos = _path.IndexOf('\\');
            return _path.Substring(0, pos - 1);
        }

        public string GetFileBase()
        {
            int posSlash = _path.LastIndexOf('\\');
            int posDot = _path.LastIndexOf('.');

            return _path.Substring(posSlash + 1, posDot - posSlash - 1);
        }

        public PathWorker(string path)
        {
            _path = path;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PathWorker path = new("c: \\ WebServers \\ home \\ testsite \\ www \\ myfile.txt");
            PathWorker path2 =
                new("C:\\Users\\mykha\\Desktop\\Sigma_School\\practs\\05_10_2021\\practicePath\\practicePath\\Program.cs");
            Console.WriteLine(path.GetFileBase());
            Console.WriteLine(path.GetRootDir());
            Console.WriteLine(path2.GetFileBase());
            Console.WriteLine(path2.GetRootDir());
        }
    }
}
