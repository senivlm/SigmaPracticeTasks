using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Task2
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
    }
    public class Storage
    {
        public List<Product> Assortment { get; set; }

        public Storage()
        {
            Assortment = new List<Product>();
        }

        public Storage(List<Product> productList)
        {
            Assortment = new List<Product>(productList);
        }

        public Storage(params Product[] productArray)
        {
            Assortment = new List<Product>(productArray.ToList());
        }

        public Product this[int index]
        {
            get
            {
                if (index < 0 || index > Assortment.Count)
                {
                    throw new ArgumentException("Index was out of bounds of array");
                }
                return Assortment[index];
            }
            set
            {
                if (index < 0 || index > Assortment.Count)
                {
                    throw new ArgumentException("Index was out of bounds of array");
                }
                Assortment[index] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new();
            if (Assortment.Count == 0)
            {
                result.Append("Storage is empty");
                return result.ToString();
            }

            foreach (var product in Assortment)
            {
                result.Append(product.ToString() + "\n");
            }
            return result.ToString();
        }

        public Storage SearchForMeat()
        {
            Storage temp = new Storage();

            foreach (var product in Assortment)
            {
                if (product.GetType() != typeof(Meat)) continue;
                temp.AddProduct(product);
            }
           
            return temp;
        }

        public void ChangePrice(double diff)
        {
            if ((diff.CompareTo(-1d) == -1) || (diff.CompareTo(1d) == 1))
            {
                throw new ArgumentException("Difference must be a number between -1 and 1");
            }

            foreach (var product in Assortment)
            {
                product.ChangePrice(diff);
            }
        }

        public void AddProduct(Product product)
        {
            Assortment.Add(product);
        }

        public void ReadFromFile(string path)
        {
            Assortment = new List<Product>();
            using (StreamReader file = new(path))
            {
                string currentLine = "";
                while ((currentLine = file.ReadLine()) != null)
                {
                    var words = currentLine.Split(" ");
                    if (!double.TryParse(words[1], out var price))
                    {
                        continue;
                    }
                    if (!double.TryParse(words[2], out var weight))
                    {
                        continue;
                    }
                    Assortment.Add(new Product(words[0], price, weight));
                }
            }
        }

       

        /*public void ReadFromConsole(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size of strorage must be 1 and greater");
            }

            Storage temp = new Storage(size);

            for (int i = 0; i < size; ++i)
            {
                Console.WriteLine("Choose type of products:\n 1) Product\n 2) Meat\n 3)Dairy Product\n");
                int type = Int32.Parse(Console.ReadLine());

                string name;
                double price, weight;

                Console.WriteLine("Enter name of product: ");
                name = Console.ReadLine();
                Console.WriteLine("Enter price of product: ");
                price = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter weight of product: ");
                weight = Double.Parse(Console.ReadLine());

                switch (type)
                {
                    case 2:
                        Console.WriteLine("Choose category 1)High 2)First 3)Second ");
                        MeatCategory mCategory = (MeatCategory)Enum.Parse(typeof(MeatCategory), Console.ReadLine());
                        Console.WriteLine("Choose meat sort: 1)Mutton 2)Beef 3)Pork 4)CHicken ");
                        MeatSort mSort = (MeatSort)Enum.Parse(typeof(MeatSort), Console.ReadLine());
                        Assortment[i] = new Meat(name, price, weight, mCategory, mSort);
                        break;
                    case 3:
                        Console.WriteLine("Enter days before expiration: ");
                        int expiration = Int32.Parse(Console.ReadLine());
                        Assortment[i] = new DairyProducts(name, price, weight, expiration);
                        break;
                    default:
                        Assortment[i] = new Product(name, price, weight);
                        break;
                }
            }
        }*/
    }
}
