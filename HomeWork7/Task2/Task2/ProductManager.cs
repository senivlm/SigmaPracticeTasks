using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task2
{
    class ProductManager
    {
        private Dictionary<string, double> _menu;
        private Dictionary<string, double> _prices;

        public ProductManager()
        {
            _menu = new Dictionary<string, double>();
            _prices = new Dictionary<string, double>();
        }
        public ProductManager(string menuPath, string pricePath)
        {
            if (!File.Exists(menuPath))
            {
                throw new FileNotFoundException("File with Menu doesn't exist at specified location");
            }
            if (!File.Exists(pricePath))
            {
                throw new FileNotFoundException("File with Prices doesn't exist at specified location");
            }
            _menu = new Dictionary<string, double>();
            _prices = new Dictionary<string, double>();

            var text = File.ReadAllText(menuPath);
            var mMenuWords = Regex.Matches(text, @"\w+\s+\d+\.?\d?");
            List<string> menuIngredients = mMenuWords.Select(mWord => mWord.Value).ToList();

            text = File.ReadAllText(pricePath);
            var mPriceWords = Regex.Matches(text, @"\w+\s+\d+\.?\d?");
            List<string> priceIngredients = mPriceWords.Select(mWord => mWord.Value).ToList();

            foreach (string menuIngredient in menuIngredients)
            {
                string[] items = menuIngredient.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                double.TryParse(items[1], out var amount);
                if(!_menu.ContainsKey(items[0]))
                {
                    _menu.Add(items[0], amount);
                }
                else
                {
                    _menu[items[0]] += amount;
                }
            }

            foreach (string priceIngredient in priceIngredients)
            {
                string[] items = priceIngredient.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                double.TryParse(items[1], out var price);
                _prices.Add(items[0], price);
            }
        }

        public override string ToString()
        {
            StringBuilder result = new();
            foreach (var item in _menu)
            {
                double totalPrice = item.Value * _prices[item.Key];
                result.Append($"{item.Key} {item.Value:F2} {totalPrice:F2}\n");
            }
            return result.ToString();
        }
    }
}