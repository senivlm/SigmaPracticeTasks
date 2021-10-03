using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace hw3
{
    public enum Quarter { First = 1, Second, Third, Fourth };
    public enum Months
    {
        January = 1, February, March, April, May, June,
        July, August, September, October, November, December
    }

    class EnergyInfo
    {
        private int _flatNumber;
        private List<FlatInfo> _flatsInfo;
        private double _price;
        private Quarter _quarter;

        public int FlatNumber
        {
            get => _flatNumber;
            set
            {
                if (value > 0)
                {
                    _flatNumber = value;
                }
                else
                {
                    throw new ArgumentException("Number of flats must be > 0");
                }
            }
        }

        public List<FlatInfo> FlatsInfo { get => _flatsInfo; }

        public double Price
        {
            get => _price;
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Price must be positive number");
                }
            }
        }

        public Quarter Quarter
        {
            get => _quarter;
            set
            {
                if (Enum.IsDefined(typeof(Quarter), value))
                {
                    _quarter = value;
                }
                else
                {
                    throw new ArgumentException("This value isn't a quarter of a year");
                }
            }
        }

        public EnergyInfo(int flatCount, string[] flatsInfo, double price, Quarter quarter)
        {
            FlatNumber = flatCount;
            _flatsInfo = new List<FlatInfo>(flatCount);
            for (int i = 0; i < flatCount; ++i)
            {
                string[] data = flatsInfo[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int.TryParse(data[0], out int flatNumber);
                string flatOwner = data[1];
                (int, int)[] flatCounter = new (int, int)[3];
                for (int j = 2, k = 0; j < data.Length - 1; j += 2, ++k)
                {

                    int.TryParse(data[j], out flatCounter[k].Item1);
                    int.TryParse(data[j + 1], out flatCounter[k].Item2);
                }
                FlatsInfo.Add(new FlatInfo(flatNumber, flatOwner, flatCounter, price));
            }
            Price = price;
            Quarter = quarter;
        }
        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(String.Format("INFORMATION ABOUT ALL FLATS\n Total number of flats: {0}\n", FlatNumber));
            result.Append(String.Format("Number of quarter: {0}\n", Quarter));

            for (int i = 0; i < FlatNumber; ++i)
            {
                result.Append(FlatsInfo[i].ToString());
                for (int j = 0; j < FlatsInfo[i].CounterInfo.Length; ++j)
                {
                    result.Append(String.Format(" {0}:\n  Begin: {1} kW\n  End: {2} kW\n  Total used: {3} kW\n",
                                                (Months)(((int)Quarter - 1) * 3 + j + 1),
                                                FlatsInfo[i].CounterInfo[j].Item1,
                                                FlatsInfo[i].CounterInfo[j].Item2,
                                                FlatsInfo[i].CounterInfoTotal[j]));
                }
                result.Append(String.Format(" To pay: {0} UAH for {1}kW\n\n", FlatsInfo[i].Bill, FlatsInfo[i].CounterInfoTotal.Sum(item => item)));
            }
            return result.ToString();
        }

        public string FindBiggestBill()
        {
            StringBuilder result = new StringBuilder();

            FlatInfo maxBillFlat = FlatsInfo.OrderByDescending(item => item.Bill).First();

            result.Append(String.Format("Biggest bill:\n {0}", maxBillFlat));

            return result.ToString();
        }

        public string FindFlatWithoutUsing()
        {
            StringBuilder result = new StringBuilder();

            FlatInfo maxBillFlat = FlatsInfo.OrderBy(item => item.CounterInfoTotal.Sum(item => item)).First();
            if (maxBillFlat.Bill != 0)
            {
                return "There is no flat without using electricity";
            }
            result.Append(String.Format("Flat without using electicity:\n {0}", maxBillFlat));

            return result.ToString();
        }

        public string ToString(int flatNumber)
        {
            StringBuilder result = new StringBuilder();
            foreach (FlatInfo item in FlatsInfo)
            {
                if (flatNumber == item.Number)
                {
                    result.Append(String.Format("INFORMATION ABOUT SPECIFIC FLAT\nNumber of quarter: {0}\n", Quarter));
                    result.Append(item.ToString());
                    for (int i = 0; i < item.CounterInfo.Length; ++i)
                    {
                        result.Append(String.Format(" {0}:\n  Begin: {1} kW\n  End: {2} kW\n  Total used: {3} kW\n",
                                                    (Months)(((int)Quarter - 1) * 3 + i + 1),
                                                    item.CounterInfo[i].Item1,
                                                    item.CounterInfo[i].Item2,
                                                    item.CounterInfoTotal[i]));
                    }
                    result.Append(String.Format(" To pay: {0} UAH for {1}kW\n\n", item.Bill, item.CounterInfoTotal.Sum(item => item)));
                    return result.ToString();
                }

            }

            return "There is no flat with this number\n";
        }
    }
}