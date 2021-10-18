using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace PolinomialExtended
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
                    throw new ArgumentException("Rank of polynomial must be a positive number");
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
                    throw new IndexOutOfRangeException("Polynomial doesn't contain element with this power");
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
            result.AppendJoin(" + ", _coeficients.Select(item => item.Key == 0 ? $"{item.Value}" : $"{item.Value}*x^{item.Key}"));

            return result.ToString();
        }

        public Polynomial Add(Polynomial pn)
        {
            if (pn._coeficients.Count == 0)
            {
                return this;
            }

            int rank = this.Rank > pn.Rank ? this.Rank : pn.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coeficients.ContainsKey(i) && !pn._coeficients.ContainsKey(i))
                {
                    continue;
                }
                else if (!this._coeficients.ContainsKey(i) && pn._coeficients.ContainsKey(i))
                {
                    pnResult[i] = pn[i];
                }
                else if (this._coeficients.ContainsKey(i) && !pn._coeficients.ContainsKey(i))
                {
                    pnResult[i] = this[i];
                }
                else
                {
                    pnResult[i] = this[i] + pn[i];
                }
            }
            return pnResult;
        }

        public Polynomial Subtract(Polynomial pn)
        {
            if (pn._coeficients.Count == 0)
            {
                return this;
            }
            int rank = this.Rank > pn.Rank ? this.Rank : pn.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                if (!this._coeficients.ContainsKey(i) && !pn._coeficients.ContainsKey(i))
                {
                    continue;
                }
                else if (!this._coeficients.ContainsKey(i) && pn._coeficients.ContainsKey(i))
                {
                    pnResult[i] = -(pn[i]);
                }
                else if (this._coeficients.ContainsKey(i) && !pn._coeficients.ContainsKey(i))
                {
                    pnResult[i] = -(this[i]);
                }
                else
                {
                    pnResult[i] = this[i] - pn[i];
                }
            }
            return pnResult;
        }

        public Polynomial Multiply(double number)
        {
            if (this._coeficients.Count != 0)
            {
                Polynomial pnResult = new(this);

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
                return this;
            }
        }

        public Polynomial Multiply(Polynomial pn)
        {
            Polynomial result = new Polynomial();
            for (int i = 0; i < this.Rank; ++i)
            {
                for (int j = 0; j < pn.Rank; ++j)
                {
                    result[i + j] = this[i] * pn[j];
                }
            }
            return result;
        }

        public static Polynomial Parse(string s)
        {
            string currentDS = new CultureInfo(CultureInfo.CurrentCulture.Name, false).NumberFormat.NumberDecimalSeparator;

            string[] additions = s
                .Replace(" ", "")
                .Replace(".", currentDS)
                .Replace(",", currentDS)
                .Split("+", StringSplitOptions.RemoveEmptyEntries);

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
                    throw new ArgumentException("Unable to read polynomial. Incorrect addition. Check your input");
                }
                ++startIndex;
            }
            else if (numbers[startIndex].Length > 2)
            {
                throw new ArgumentException("Unable to read polynomial. Incorrect addition. Check your input");
            }

            for (int i = startIndex; i < numbers.Length; ++i)
            {
                if (numbers[i].Length != 2)
                {
                    throw new ArgumentException("Unable to read polynomial. Incorrect addition. Check your input");
                }

                if (double.TryParse(numbers[i][0], out double currentVal) &&
                    int.TryParse(numbers[i][1], out int currentKey))
                {
                    if (result._coeficients.ContainsKey(currentKey))
                    {
                        throw new ArgumentException("Unable to read polynome. There are two additions with same power. Check your input");
                    }
                    result[currentKey] = currentVal;
                }
                else
                {
                    throw new ArgumentException("Unable to read polynomial. Incorrect addition. Check your input");
                }
            }
            return result;
        }

        public double CalculateFor(double arg) => _coeficients.Sum(item => item.Value * Math.Pow(arg, item.Key));

        public static Polynomial operator+(Polynomial pn1, Polynomial pn2)
        {
            if (pn1._coeficients.Count == 0)
            {
                return pn2;
            }

            if (pn2._coeficients.Count == 0)
            {
                return pn1;
            }

            int rank = pn2.Rank > pn1.Rank ? pn2.Rank : pn1.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                if (!pn2._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    continue;
                }
                else if (!pn2._coeficients.ContainsKey(i) && pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = pn1[i];
                }
                else if (pn2._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                {
                    pnResult[i] = pn2[i];
                }
                else
                {
                    pnResult[i] = pn2[i] + pn1[i];
                }
            }
            return pnResult;
        }

        public static Polynomial operator-(Polynomial pn1, Polynomial pn2)
        {
            if (pn1._coeficients.Count == 0)
            {
                return pn2;
            }
            if (pn2._coeficients.Count == 0)
            {
                return pn1;
            }
            int rank = pn2.Rank > pn1.Rank ? pn2.Rank : pn1.Rank;
            var pnResult = new Polynomial();
            for (int i = 0; i <= rank; ++i)
            {
                switch (pn2._coeficients.ContainsKey(i))
                {
                    case false when !pn1._coeficients.ContainsKey(i):
                        continue;
                    case false when pn1._coeficients.ContainsKey(i):
                        pnResult[i] = -(pn1[i]);
                        break;
                    default:
                        {
                            if (pn2._coeficients.ContainsKey(i) && !pn1._coeficients.ContainsKey(i))
                            {
                                pnResult[i] = -(pn2[i]);
                            }
                            else
                            {
                                pnResult[i] = (pn1[i]) + (-pn2[i]);
                            }

                            break;
                        }
                }
            }
            return pnResult;
        }

        public static Polynomial operator*(Polynomial pn1, Polynomial pn2)
        {
            Polynomial result = new Polynomial();
            for (int i = 0; i < pn1.Rank; ++i)
            {
                for (int j = 0; j < pn2.Rank; ++j)
                {
                    result[i + j] = pn1[i] * pn2[j];
                }
            }
            return result;
        }

        public static implicit operator Polynomial(double value)
        {
            Polynomial result = new Polynomial();
            result._coeficients.Add(0, value);
            return result;
        }
    }
}