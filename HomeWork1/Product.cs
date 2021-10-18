using System;

namespace hw1
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
                    throw new ArgumentException("Name of product can't be empty");
                }
            }
        }

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
    }
}
