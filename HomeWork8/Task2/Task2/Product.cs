using System;
using System.Text;

namespace Task2
{
    public class Product
    {
        private string _name;
        private double _price;
        private double _weight;

        public string Name
        {
            get => _name;
            set
            {
                if (String.Compare(value, "") != 0)
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

        public Product(string name, double price, double weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
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
            if ((obj == null) || (this.GetType() != obj.GetType()))
            {
                return false;
            }
            else
            {
                Product temp = obj as Product;
                return (this.Name.Equals(temp.Name) && (this.Price == temp.Price) && (this.Weight == temp.Weight));
            }

        }
    }
}
