using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;
namespace hw4
{
    class Polynomial
    {
        private int _rank;
        private SortedDictionary<int, double> _coeficients;

        public int Rank
        {
            get => _rank;
            set
            {
                if (value >= 0)
                {
                    _rank = value;
                }
                else
                {
                    throw new ArgumentException("Rank of polinom must be a positive number");
                }
            }
        }

        public double this[int index]
        {
            get
            {
                if (index >= 0 && _coeficients.ContainsKey(index))
                {
                    return _coeficients[index];
                }
                else
                {
                    throw new IndexOutOfRangeException("Polynome doesn't contain element with this power");
                }
            }
            set
            {
                if (value != 0.0 && _coeficients.ContainsKey(index))
                {
                    _coeficients[index] = value;

                }
                else if (value != 0.0 && !_coeficients.ContainsKey(index))
                {
                    _coeficients.Add(index, value);
                }
                else if (value == 0.0 && _coeficients.ContainsKey(index))
                {
                    _coeficients.Remove(index);
                }
                if (_coeficients.Count != 0)
                {
                    Rank = _coeficients.Last().Key;
                }
            }
        }

        public Polynomial(Polynomial pn)
        {
            if (pn == null)
            {
                throw new ArgumentNullException("Input polinomial is empty");
            }
            this.Rank = pn.Rank;
            this._coeficients = new SortedDictionary<int, double>();
            foreach (var item in pn._coeficients)
            {
                this._coeficients.Add(item.Key, pn._coeficients[item.Key]);
            }

        }

        public Polynomial(int rank)
        {
            Rank = rank;
            _coeficients = new SortedDictionary<int, double>();
            for (int i = 0; i <= rank; ++i)
            {
                _coeficients.Add(i, i + 1);
            }
        }

        public Polynomial()
        {
            _coeficients = new SortedDictionary<int, double>();
        }

        public override string ToString()
        {
            if (this._coeficients.Count == 0)
            {
                return "0";
            }
            StringBuilder result = new StringBuilder();

            foreach (var item in _coeficients)
            {
                result.Append($"{(item.Value > 0 ? "+ " : "- ")}{item.Value}{(item.Key > 0 ? $"*x^{item.Key}" : "")} ");
            }
            if (result[0] == '+')
            {
                result.Remove(0, 2);
            }
            return result.ToString();
        }

        public Polynomial Add(Polynomial pn1)
        {
            int rank = this.Rank > pn1.Rank ? this.Rank : pn1.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    continue;
                }
                else if (!this._coeficients.ContainsKey(i) && pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = pn1[i];
                }
                else if (this._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = this[i];
                }
                else
                {
                    pnResult[i] = this[i] + pn1[i];
                }
            }
            return pnResult;
        }

        public Polynomial Subtract(Polynomial pn1)
        {
            int rank = this.Rank > pn1.Rank ? this.Rank : pn1.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    continue;
                }
                else if (!this._coeficients.ContainsKey(i) && pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = -(pn1[i]);
                }
                else if (this._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = -(this[i]);
                }
                else
                {
                    pnResult[i] = (this[i]) + (-pn1[i]);
                }
            }
            return pnResult;
        }

        public Polynomial Multiply(double number)
        {
            if (this._coeficients.Count != 0)
            {
                Polynomial pnResult = new Polynomial(this);
                for (int i = 0; i <= Rank; ++i)
                {
                    if (_coeficients.ContainsKey(i) == true)
                    {
                        pnResult[i] = this[i] * number;
                    }
                }
                return pnResult;
            }
            else
            {
                throw new ArgumentNullException("Polynome is empty");
            }
        }

        public static Polynomial Parse(string s)
        {
            string decimalSeparatorUA = new CultureInfo("uk-UA", false).NumberFormat.NumberDecimalSeparator;
            string decimalSeparatorEN = new CultureInfo("en-UK", false).NumberFormat.NumberDecimalSeparator;

            StringBuilder temp = new StringBuilder();
            foreach (char c in s)
            {
                if (c != ' ')
                {
                    if (c.ToString() == decimalSeparatorUA)
                    {
                        temp.Append(decimalSeparatorEN);
                        continue;
                    }
                    temp.Append(c);
                }


            }
            string[] additions = temp.ToString().Split('+', StringSplitOptions.RemoveEmptyEntries);
            string[][] numbers = new string[additions.GetLength(0)][];

            for (int i = 0; i < additions.Length; ++i)
            {
                numbers[i] = additions[i].Split("*x^");
            }
            Polynomial result = new Polynomial();
            int startIndex = 0;
            if (numbers[startIndex].Length == 1)
            {
                if (double.TryParse(numbers[startIndex][0], out double zeroCoef))
                {
                    result[startIndex] = zeroCoef;
                }
                else
                {
                    throw new ArgumentException("Unable to read polynome. Check your input");
                }
                ++startIndex;
            }
            else if (numbers[startIndex].Length > 2)
            {
                throw new ArgumentException("Unable to read polynome. Check your input");
            }

            for (int i = startIndex; i < numbers.Length; ++i)
            {
                if (numbers[i].Length != 2)
                {
                    throw new ArgumentException("Unable to read polynome. Check your input");
                }
                if (double.TryParse(numbers[i][0], out double currentVal) &&
                    int.TryParse(numbers[i][1], out int currentKey))
                {
                    result[currentKey] = currentVal;
                }
                else
                {
                    throw new ArgumentException("Unable to read polynome. Check your input");
                }
            }



            return result;
        }
    }
}
