using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task.Classes
{
    public class Product
    {
        private string _name = "Unnamed";
        private double _price = 0;
        private double _weight = 0;
        private int _expiration = 0;
        private DateTime _madeDate = DateTime.Today;

        public bool IsValid => MadeDate.AddDays(Expiration) > DateTime.Today;

        public string Name
        {
            get => _name;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _name = value;
                }
                else
                {
                    throw new ArgumentException("Name can't be empty");
                }
            }
        }
        public double Price
        {
            get => _price;
            set
            {
                if (value >= 0)
                {
                    _price = value;
                }
                else
                {
                    throw new ArgumentException("Price must be a positive number");
                }
            }
        }
        public double Weight
        {
            get => _weight;
            set
            {
                if (value > 0)
                {
                    _weight = value;
                }
                else
                {
                    throw new ArgumentException("Weight must be a positive number");
                }
            }
        }
        public int Expiration
        {
            get => _expiration;
            set
            {
                if (value > 0)
                {
                    _expiration = value;
                }
                else
                {
                    throw new ArgumentException("Days to expire value must be a positive number");
                }
            }
        }
        public DateTime MadeDate
        {
            get => _madeDate;
            set
            {
                _madeDate = value;
            }
        }

        public Product()
        {

        }
        public Product(string name, double price, double weight, int expiration, DateTime madeDate)
        {
            Name = name;
            Price = price;
            Weight = weight;
            Expiration = expiration;
            MadeDate = madeDate;
        }
        public Product(string info)
        {
            Parse(info);
        }
        public virtual void ChangePrice(double diff)
        {
            if ((diff.CompareTo(-1d) == -1) || (diff.CompareTo(1d) == 1))
            {
                throw new ArgumentException("Difference must be a number between -1 and 1");
            }
            Price += Price * diff;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.Append("Name: " + _name + "\n");
            result.Append("Price: " + _price.ToString() + "\n");
            result.Append("Weight: " + _weight.ToString() + "\n");
            return result.ToString();
        }
        public override int GetHashCode()
        {
            return (Convert.ToInt32(_price) << 2) ^ Convert.ToInt32(_weight);
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(this.GetType() == obj.GetType()))
            {
                return false;
            }
            else
            {
                Product temp = obj as Product;
                return (this.Name.Equals(temp.Name) && (this.Price == temp.Price) && (this.Weight == temp.Weight));
            }

        }

        protected virtual void Parse(string s)
        {
            List<string> mFieldds = Regex.Split(s, @"\s*;\s*").ToList();
            mFieldds.RemoveAll(item => Regex.IsMatch(item, @"\s*;\s*"));
            string[] fields = mFieldds.ToArray();

            if (fields.Length != 5)
            {
                throw new ArgumentException($"{nameof(s)} has wrong format. String must contain 5 values separated by \";\" symbol whithout spaces");
            }

            string prodName = fields[0];

            if (!double.TryParse(fields[1], out double price))
            {
                
                throw new FormatException("Wrong price format (field 2)");
            }
            if (!double.TryParse(fields[2], out double weight))
            {
                throw new ArgumentException("Wrong weight format (field 3)");
            }
            if (!int.TryParse(fields[3], out int expiration))
            {
                throw new ArgumentException("Wrong expiration days format (field 4)");
            }
            if (!DateTime.TryParseExact(fields[4], "dd.MM.yyyy", null, DateTimeStyles.None, out DateTime madeDate))
            {
                throw new ArgumentException("Wrong production date format (field 5)\nCorrect is dd.mm.yyyy (03.02.2021)");
            }

            this.Name = prodName;
            this.Price = price;
            this.Weight = weight;
            this.Expiration = expiration;
            this.MadeDate = madeDate;
        }

    }
}
