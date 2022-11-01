using System;
using System.Collections.Generic;
using System.Linq;

namespace hw1
{//Чи не краще було одразу зробити методи для обчислення загальної вартості і загальної ваги?
    public class Buy
    {
        private List<Product> _purchaseList;
        private int _amount;
        private double _totalPrice;
        private double _totalWeight;

        public Buy(Product purchase, int amount)
        {
            _purchaseList = new List<Product>();
            _purchaseList.Add(purchase);
            Amount = amount;
            _totalPrice = _amount * this._purchaseList[0].Price;
            _totalWeight = _amount * this._purchaseList[0].Weight;
        }
        //вдале використання params. Молодець.
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
    }
}
