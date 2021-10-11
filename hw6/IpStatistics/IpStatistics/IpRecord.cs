using System;
using System.Text;

namespace IpStatistics
{
    class IpRecord
    {
        private string _ipAddress;
        private TimeSpan _visitTime;
        private DayOfWeek _visitDay;
        private int _visitCount;

        public string IpAddress
        {
            get => _ipAddress;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("IpAddress string can't be empty");
                }
                _ipAddress = value;
            }
        }
        public TimeSpan VisitTime
        {
            get => _visitTime;
            private set => _visitTime = value;
        }
        public DayOfWeek VisitDay
        {
            get => _visitDay;
            private set => _visitDay = value;
        }
        public int VisitCount
        {
            get => _visitCount;
            set
            {
                if (value >= 1)
                {
                    _visitCount = value;
                }

                throw new ArgumentException("Visit Count must be a positive number");
            }
        }

        public IpRecord()
        {
            IpAddress = "1.1.1.1";
            VisitTime = new TimeSpan(0,0,0);
            VisitDay = DayOfWeek.Monday;
            VisitCount = 1;
        }
        public IpRecord(string ipAddress, TimeSpan visitTime, DayOfWeek visitDay, int visitCount)
        {
            IpAddress = ipAddress;
            VisitTime = visitTime;
            VisitDay = visitDay;
            VisitCount = visitCount;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.Append($"\nIP: {IpAddress}\nTime: {VisitTime}\nDay: {VisitDay}\n");
            return sb.ToString();
        }
    }
}