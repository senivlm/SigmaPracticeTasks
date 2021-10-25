using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Task.Enums;

namespace Task.Classes
{
    public class Storage
    {
        public delegate void WrongProductInputHandle(string errorInfo, Storage storage);
        public delegate void ExpiredProductsHandle(Storage storage, List<Product> expiredList);

        public event WrongProductInputHandle OnWrongInput;
        public event ExpiredProductsHandle OnExpiredSearch;
        private List<Product> _assortment;
        public List<Product> Assortment { get => _assortment; }

        public Storage()
        {
            _assortment = new();
        }

        public Storage(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size of array can't be a negative number");
            }
            _assortment = new List<Product>(size);
        }

        public Storage(params Product[] productArray)
        {
            _assortment = productArray.ToList();
        }

        public Storage(List<Product> productList)
        {
            _assortment = 
                productList;
        }

        public Storage(string path)
        {
            _assortment = new List<Product>();
            ReadFromFile(path);
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
            if (Assortment.Count == 0)
            {
                return "Storage is empty";
            }
            StringBuilder result = new StringBuilder();
            foreach (var product in Assortment)
            {
                result.Append(product.ToString() + "\n");
            }
            return result.ToString();
        }

        public void AddProduct(Product product)
        {
            Assortment.Add(product);
        }

        public void Remove(Product product)
        {
            Assortment.Remove(product);
        }

        public void RemoveByName(string name, bool removeAll)
        {
            if (!string.IsNullOrEmpty(name))
            {
                if (removeAll == true)
                    Assortment.RemoveAll(item => item.Name == name);
                else
                    Assortment.RemoveAt(Assortment.FindIndex(item => item.Name == name));
            }
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

        public void ReadFromConsole(int size)
        {
            if (size < 1)
            {
                throw new ArgumentException("Size of strorage must be 1 and greater");
            }

            Storage temp = new Storage(size);

            for (int i = 0; i < size; ++i)
            {
                Console.WriteLine("Choose type of products:\n 1) Product\n 2) Meat\n 3)Dairy Product\n");
                int type = 1;
                while (int.TryParse(Console.ReadLine(), out type))
                {
                    if (type > 0 & type < 4) break;
                    Console.WriteLine("Enter correct type of product from list!");
                }

                int expiration;
                double price, weight;
                DateTime madeDate;

                Console.WriteLine("Enter name of product: ");
                string name = Console.ReadLine();

                Console.WriteLine("Enter price of product: ");
                while (double.TryParse(Console.ReadLine(), out price) == false) Console.WriteLine("Enter correct price! (greater than 0)");

                Console.WriteLine("Enter weight of product: ");
                while (double.TryParse(Console.ReadLine(), out weight) == false) Console.WriteLine("Enter correct weight! (greater than 0)");

                Console.WriteLine("Enter term of expiration of product: ");
                while (int.TryParse(Console.ReadLine(), out expiration) == false) Console.WriteLine("Enter correct number of days before expiration! (greater than 0)");

                Console.WriteLine("Enter date of production of product: ");
                while (DateTime.TryParse(Console.ReadLine(), out madeDate) == false) Console.WriteLine("Enter correct date! (format dd.mm.yyyy before today)");

                switch (type)
                {
                    case 2:
                        Console.WriteLine("Choose category 1)High 2)First 3)Second ");
                        MeatCategory mCategory = 0;
                        while (Enum.TryParse(Console.ReadLine(), out mCategory) == false) Console.WriteLine("Enter correct position from list!");

                        MeatSort mSort = 0;
                        Console.WriteLine("Choose meat sort: 1)Mutton 2)Beef 3)Pork 4)Chicken ");
                        while (Enum.TryParse(Console.ReadLine(), out mSort) == false) Console.WriteLine("Enter correct position from list!");

                        AddProduct(new Meat(name, price, weight, expiration, madeDate, mCategory, mSort));
                        break;
                    case 3:
                        AddProduct(new DairyProducts(name, price, weight, expiration, madeDate));
                        break;
                    default:
                        AddProduct(new Product(name, price, weight, expiration, madeDate));
                        break;
                }
            }
        }

        public void ReadFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Wrong path to file");
            }

            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                List<string> mFields = Regex.Split(line, @"\s*;\s*").ToList();
                mFields.RemoveAll(item => Regex.IsMatch(item, @"\s*;\s*"));
                var fields = mFields.ToArray();
                string errorMessage = "";
                if (fields.Length != 8 && fields.Length != 6)
                {
                    //throw new FormatException("Wrong number of values in line");
                    errorMessage += "\nWrong number of args in line :\n " + line;
                    OnWrongInput?.Invoke(errorMessage, this);
                    continue;
                }

                if (!Enum.TryParse(typeof(ProductType), fields[0], true, out var type))
                {
                    //throw new ArgumentException("Wrong type of product. Correct your file");
                    errorMessage += "\nWrong enum type in line :\n" + line;
                    OnWrongInput?.Invoke(errorMessage, this);
                    continue;
                }

                try
                {
                    switch ((ProductType?)type)
                    {
                        case ProductType.Product:
                            Assortment.Add(new Product(string.Join(";",
                                fields.Where((_, index) => 0 < index && index < fields.Length))));
                            break;
                        case ProductType.Dairy:
                            Assortment.Add(new DairyProducts(string.Join(";",
                                fields.Where((_, index) => 0 < index && index < fields.Length))));
                            break;
                        case ProductType.Meat:
                            Assortment.Add(new Meat(string.Join(";",
                                fields.Where((_, index) => 0 < index && index < fields.Length))));
                            break;
                    }
                }
                catch (Exception e)
                {
                    errorMessage += "Input error in line:\n " + line + "\n" + e.Message;
                    OnWrongInput?.Invoke(errorMessage, this);
                }
            }
        }

        public void ShowProductsInConsole()
        {
            OnExpiredSearch?.Invoke(this, GetExpiredProducts());
            Console.WriteLine("Product list:");
            foreach (var product in Assortment)
            {
                Console.WriteLine(product);
            }
        }

        public List<Product> GetExpiredProducts()
        {
            var result = Assortment.Where(item => item.IsValid == false).ToList();
            return result;
        }

        public void RemoveExpiredDiary(string path)
        {
            var expired = Assortment.OfType<DairyProducts>().Where(item => item.IsValid == false).ToList();
            expired.ForEach(item => Assortment.Remove(item));

            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("EXPIRED PRODUCTS");
                foreach (var item in expired)
                {
                    file.WriteLine(item.ToString());
                }
            }


        }

    }
}