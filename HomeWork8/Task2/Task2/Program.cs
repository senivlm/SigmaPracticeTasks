using System;
using System.Collections.Generic;
using System.Linq;
namespace Task2
{
    class Program
    {
        public static void DemonstrateSearch()
        {
            Storage storage1 = new Storage(new List<Product>()
            {
                new Product("Potato", 15, 2),
                new Product("Water", 25, 1),
                new Product("Bread", 12, 0.5),
                new Product("Banana", 30, 1),
                new Product("Apple", 10, 1 ),
                new Product("Butter", 40, 0.3)
            });

            Storage storage2 = new Storage(new List<Product>()
            {
                new Product("Potato", 15, 2),
                new Product("Water", 25, 1),
                new Product("Chocolate", 12, 0.5),
                new Product("Grape", 30, 1),
                new Product("Apple", 10, 1 ),
                new Product("Butter", 40, 0.3)
            });

            var common = StorageManager.SearchCommon(storage1, storage2);
            Console.WriteLine("Common products: \n" + common);

            var uniqueForFirst = StorageManager.SearchUniqueForFirst(storage1, storage2);
            Console.WriteLine("Unique for first storage:\n" + uniqueForFirst);

            var nonCommon = StorageManager.SearchNonCommon(storage1, storage2);
            Console.WriteLine("Non common for both: \n" + nonCommon);
        }
        static void Main(string[] args)
        {
            DemonstrateSearch();
        }
    }
}
