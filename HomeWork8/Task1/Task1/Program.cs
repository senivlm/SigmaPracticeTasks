using System;
using System.Collections.Generic;

namespace Task1
{
    class Program
    {
        public static void DemonstrateSorting()
        {
            List<Product> productList = new()
            {
                new Product("Water", 25, 1),
                new Product("Milk", 40, 1),
                new Product("Potato", 15, 2),
                new Product("Bread", 12, 0.5)
            };
            Product[] productArray = productList.ToArray();

            SortDelegate.Sort(productArray, CompareByPrice);
            Console.WriteLine("Array sorted by price");
            foreach (var product in productArray)
            {
                Console.WriteLine(product);
            }

            Console.WriteLine("Array sorted by weight");
            SortDelegate.Sort(productArray, CompareByWeight);
            foreach (var product in productArray)
            {
                Console.WriteLine(product);
            }
        }

        private static int CompareByWeight(object obj1, object obj2)
        {
            var product1 = obj1 as Product;
            var product2 = obj2 as Product;

            return (int) (product1.Weight - product2.Weight);
        }

        private static int CompareByPrice(object obj1, object obj2)
        {
            var product1 = obj1 as Product;
            var product2 = obj2 as Product;

            return (int) (product1.Price - product2.Price);
        }

        static void Main(string[] args)
        {
           DemonstrateSorting();
        }
    }
}
