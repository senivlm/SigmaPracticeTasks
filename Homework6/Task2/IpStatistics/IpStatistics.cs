using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IpStatistics
{
    class IpStatistics
    {
        private Dictionary<string, (List<TimeSpan>, List<DayOfWeek>)> _ipAddressDictionary;

        public (List<TimeSpan>, List<DayOfWeek>) this[string ip]
        {
            get
            {
                if (_ipAddressDictionary.ContainsKey(ip))
                {
                    return _ipAddressDictionary[ip];
                }

                throw new IndexOutOfRangeException("There is no record with this IP");
            }
            set
            {
                if (_ipAddressDictionary.ContainsKey(ip))
                {
                    _ipAddressDictionary[ip].Item1.AddRange(value.Item1);
                    _ipAddressDictionary[ip].Item2.AddRange(value.Item2);
                }
                else
                {
                    _ipAddressDictionary.Add(ip, value);
                }
            }
        }

        public IpStatistics()
        {
            _ipAddressDictionary = new Dictionary<string, (List<TimeSpan>, List<DayOfWeek>)>();
        }
        public IpStatistics(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            _ipAddressDictionary = new Dictionary<string, (List<TimeSpan>, List<DayOfWeek>)>();
            using StreamReader file = new StreamReader(path);
            string tempLine;
            while ((tempLine = file.ReadLine()) != null)
            {
                string[] words = tempLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (Regex.IsMatch(words[0], @"\d*\.\d*\.\d*\.\d*")
                    && TimeSpan.TryParse(words[1], out TimeSpan tempTime) == true
                    && Enum.TryParse(words[2], true, out DayOfWeek tempDay) == true)
                {
                    this.AddRecord(words[0], tempTime, tempDay);
                }
            }
        }

        public void AddRecord(string ip, TimeSpan time, DayOfWeek day)
        {
            List<TimeSpan> timeList = new List<TimeSpan>();
            List<DayOfWeek> dayList = new List<DayOfWeek>();

            timeList.Add(time);
            dayList.Add(day);

            this[ip] = (timeList, dayList);
        }
        public string GetIndividualStatistics()
        {
            StringBuilder sb = new("Statistics\n");

            foreach (var (key, value) in _ipAddressDictionary)
            {
                var popularTime =
                   value.Item1
                       .GroupBy(i => i.Hours)
                       .OrderByDescending(grp => grp.Count())
                       .First().Key;
                sb.Append($" IP: {key}\n Visits: {value.Item1.Count}\n Popular visit time: {popularTime}:00-{popularTime + 1}:00\n\n");

            }

            return sb.ToString();
        }
        public string GetGeneralStatistics()
        {
            List<TimeSpan> timeList = new List<TimeSpan>();

            foreach (var (key, value) in _ipAddressDictionary)
            {
                timeList.AddRange(value.Item1);
            }
            var popularTime =
                timeList.GroupBy(i => i.Hours)
                    .OrderByDescending(grp => grp.Count())
                    .First().Key;
            string result = new($"Total Statistics\n Total visits: {timeList.Count}\n Popular visit time: {popularTime}:00-{popularTime + 1}:00\n");
            return result;
        }

    }
}