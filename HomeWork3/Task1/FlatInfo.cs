using System;
using System.Linq;
using System.Text;
namespace hw3
{
    class FlatInfo
    {
        private int _number;
        private string _owner;
        private (int, int)[] _counterInfoBeginEnd;

        private int[] _countersInfoTotal;
        private double _price;
        private double _bill;

        public int Number
        {
            get => _number;
            set
            {
                if (value > 0)
                {
                    _number = value;
                }
                else
                {
                    throw new ArgumentException("Number of flats must be positive number");
                }
            }
        }

        public string Owner
        {
            get => _owner;
            set
            {
                if (String.Compare(value, "") != 0)
                {
                    _owner = value;
                }
                else
                {
                    throw new ArgumentException("Owner's name can't be empty");
                }
            }
        }

        public (int, int)[] CounterInfo
        {
            get => _counterInfoBeginEnd;
            set
            {
                _counterInfoBeginEnd = new (int, int)[value.Length];
                for (int i = 0; i < value.Length; ++i)
                {
                    _counterInfoBeginEnd[i].Item1 = value[i].Item1;
                    _counterInfoBeginEnd[i].Item2 = value[i].Item2;
                }
            }
        }

        public int[] CounterInfoTotal { get => _countersInfoTotal; }

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
                    throw new ArgumentException("Price must be a positive number");
                }
            }
        }

        public double Bill
        {
            get => _bill;
            private set
            {
                if (value >= 0)
                {
                    _bill = value;
                }
                else
                {
                    throw new ArgumentException("Bill must be a positive number.\nCheck your price\\counters data");
                }
            }
        }

        public FlatInfo(int number, string owner, (int, int)[] counterData, double price)
        {
            Number = number;
            Owner = owner;
            CounterInfo = counterData;
            Price = price;
            _countersInfoTotal = new int[counterData.Length];
            for (int i = 0; i < counterData.Length; ++i)
            {
                _countersInfoTotal[i] = counterData[i].Item2 - counterData[i].Item1;
            }
            Bill = _countersInfoTotal.Sum(item => item * Price);
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append(String.Format("Flat #{0} Owner: {1}\n", Number, Owner));
            return result.ToString();
        }
    }
}