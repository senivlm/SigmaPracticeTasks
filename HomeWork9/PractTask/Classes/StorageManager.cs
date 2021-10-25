using System;
using System.Collections.Generic;
using System.Linq;

namespace Task.Classes
{
    public class StorageManager
    {
        public static Storage SearchCommon(Storage list1, Storage list2)
        {
            return new Storage(list1.Assortment
                .Where(item => list2.Assortment.Contains(item))
                .ToList());
        }

        public static Storage SearchUniqueForFirst(Storage list1, Storage list2)
        {
            return new Storage(list1.Assortment
                .Where(item => !list2.Assortment.Contains(item))
                .ToList());
        }

        public static Storage SearchNonCommon(Storage list1, Storage list2)
        {
            Storage listOfNonCommon = new Storage();

            listOfNonCommon.Assortment.AddRange(list1.Assortment.Where(item => !list2.Assortment.Contains(item)).ToList());
            listOfNonCommon.Assortment.AddRange(list2.Assortment.Where(item => !list1.Assortment.Contains(item)).ToList());

            return listOfNonCommon;
        }

        public static Storage SearchByName(string name, Storage storage)
        {
            return new Storage(storage.Assortment.Where(item => string.Compare(item.Name, name, ignoreCase: true) == 0).ToList());
        }

        public static Storage SearchByPrice(double lowerLimit, double upperLimit, Storage storage)
        {
            if (lowerLimit > upperLimit)
                throw new ArgumentException($"Lower price limit must be less than upper price limit", nameof(lowerLimit));

            return new Storage(storage.Assortment.Where(item => item.Price >= lowerLimit && item.Price <= upperLimit).ToList());
        }

        public static Storage SearchByWeight(double lowerLimit, double upperLimit, Storage storage)
        {
            if (lowerLimit > upperLimit)
                throw new ArgumentException($"Lower weight limit must be less than upper weight limit", nameof(lowerLimit));
            List<Product> searchResult = storage.Assortment.Where(item =>
                ((item.Weight >= lowerLimit) && (item.Weight <= upperLimit))).ToList();

           

            return new Storage(searchResult);
        }

        public static Storage SearchForMeat(Storage storage)
        {

            Storage temp = new Storage();

            foreach (var product in storage.Assortment)
            {
                if (product.GetType() == typeof(Meat))
                {
                    temp.AddProduct(product);
                }
            }

            return temp;
        }

    }
}