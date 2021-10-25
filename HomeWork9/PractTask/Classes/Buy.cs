using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task.Classes
{
    public class Buy
    {
        private List<Product> _purchaseList;
        private int _amount;
        private double _totalPrice;
        private double _totalWeight;

        public Buy()
        {
            _purchaseList = new List<Product>();
            _purchaseList.Add(new Product("Default", 1, 1, 1, DateTime.Today));
            Amount = 1;
            _totalPrice = _amount * this._purchaseList[0].Price;
            _totalWeight = _amount * this._purchaseList[0].Weight;
        }

        public Buy(Product purchase, int amount)
        {
            _purchaseList = new List<Product>();
            _purchaseList.Add(purchase);
            Amount = amount;
            _totalPrice = _amount * this._purchaseList[0].Price;
            _totalWeight = _amount * this._purchaseList[0].Weight;
        }
        public Buy(params Product[] products)
        {
            _purchaseList = new List<Product>();
            Amount = products.Length;
            for (int i = 0; i < products.Length; ++i)
            {
                if (products[i] == null)
                {
                    throw new ArgumentException("Invalid agument");
                }
                else
                {
                    _purchaseList.Add(products[i]);
                }

            }
            _totalPrice = _purchaseList.Sum(product => product.Price);
            _totalWeight = _purchaseList.Sum(product => product.Weight);
        }
        public List<Product> PurchaseList
        {
            get => _purchaseList;
        }
        public int Amount
        {
            get => _amount;
            set
            {
                if (value > 0)
                {
                    _amount = value;
                }
                else
                {
                    throw new ArgumentException("Amount must be a positive number");
                }
            }
        }
        public double TotalPrice { get => _totalPrice; }
        public double TotalWeight { get => _totalWeight; }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < PurchaseList.Count; ++i)
            {
                result.Append("\n" + PurchaseList[i].ToString());
            }

            result.Append($"\n\nAmount: {Amount.ToString() } pcs");
            result.Append($"\nTotal Price: {TotalPrice.ToString("$0.00")}");
            result.Append($"\nTotal Weight: {TotalWeight.ToString("0.00 kg")}\n");

            return result.ToString();
        }
        public override int GetHashCode()
        {
            return ((Convert.ToInt32(_totalPrice + _totalWeight) << 2) ^ _amount);
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !(this.GetType().Equals(obj.GetType())))
            {
                return false;
            }
            else
            {
                Buy temp = obj as Buy;
                bool bTemp = true;
                for (int i = 0; i < PurchaseList.Count; ++i)
                {
                    bTemp = bTemp && this._purchaseList[i].Equals(temp._purchaseList[i]);
                }
                return (bTemp &&
                        (this.Amount == temp.Amount) &&
                        (this.TotalPrice == temp.TotalPrice) &&
                        (this.TotalWeight == temp.TotalWeight));
            }
        }
    }
}

