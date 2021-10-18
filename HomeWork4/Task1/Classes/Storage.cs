using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace hw4_task1
{
    public class Storage
    {
        private List<Product> _assortment;

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
            _assortment = new List<Product>(productArray.Length);
            for (int i = 0; i < _assortment.Count; ++i)
            {
                _assortment[i] = productArray[i];
            }
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
                if (index < 0 || index > _assortment.Count)
                {
                    throw new ArgumentException("Index was out of dounds of array");
                }
                return _assortment[index];
            }
            set
            {
                if (index < 0 || index > _assortment.Count)
                {
                    throw new ArgumentException("Index was out of dounds of array");
                }
                _assortment[index] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (_assortment.Count == 0)
            {
                result.Append("Storage is empty");
                return result.ToString();
            }

            for (int i = 0; i < _assortment.Count; ++i)
            {
                result.Append(_assortment[i].ToString() + "\n");
            }
            return result.ToString();
        }

        public Storage SearchForMeat()
        {
            int counter = 0;
            foreach (var p in _assortment)
            {
                if (p.GetType() == typeof(Meat))
                {
                    ++counter;
                }
            }
            if (counter == 0)
            {
                return new Storage(0);
            }

            Storage temp = new Storage(counter);

            for (int i = 0, ind = 0; i < _assortment.Count; ++i)
            {
                if (_assortment[i].GetType() == typeof(Meat))
                {
                    temp[ind] = _assortment[i];
                    ++ind;
                }
            }
            return temp;
        }

        public void ChangePrice(double diff)
        {
            if ((diff.CompareTo(-1d) == -1) || (diff.CompareTo(1d) == 1))
            {
                throw new ArgumentException("Difference must be a number between -1 and 1");
            }
            for (int i = 0; i < _assortment.Count; ++i)
            {
                _assortment[i].ChangePrice(diff);
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
                int type = Int32.Parse(Console.ReadLine());

                string name;
                int expiration;
                double price, weight;
                DateTime madeDate;


                Console.WriteLine("Enter name of product: ");
                name = Console.ReadLine();
                Console.WriteLine("Enter price of product: ");
                price = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter weight of product: ");
                weight = Double.Parse(Console.ReadLine());
                Console.WriteLine("Enter term of expiration of product: ");
                expiration = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter date of production of product: ");
                madeDate = DateTime.Parse(Console.ReadLine());

                switch (type)
                {
                    case 2:
                        Console.WriteLine("Choose category 1)High 2)First 3)Second ");
                        MeatCategory mCategory = (MeatCategory)Enum.Parse(typeof(MeatCategory), Console.ReadLine());
                        Console.WriteLine("Choose meat sort: 1)Mutton 2)Beef 3)Pork 4)CHicken ");
                        MeatSort mSort = (MeatSort)Enum.Parse(typeof(MeatSort), Console.ReadLine());
                        _assortment[i] = new Meat(name, price, weight, expiration, madeDate, mCategory, mSort);
                        break;
                    case 3:
                        _assortment[i] = new DairyProducts(name, price, weight, expiration, madeDate);
                        break;
                    default:
                        _assortment[i] = new Product(name, price, weight, expiration, madeDate);
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

            using (StreamReader file = new StreamReader(path))
            {
                while (!file.EndOfStream)
                {
                    string[] fields = file.ReadLine().Split(";", StringSplitOptions.RemoveEmptyEntries);
                    if (fields.Length != 8 && fields.Length != 6)
                    {
                        throw new ArgumentException("Wrong number of values in line");
                    }

                    if (!Enum.TryParse(typeof(ProductType), fields[0], true, out var type))
                    {
                        throw new ArgumentException("Wrong type of product. Correct your file");
                    }
                    else
                    {
                        switch ((ProductType)type)
                        {
                            case ProductType.Product: _assortment.Add(new Product(String.Join(";", fields.Where((_, index) => 0 < index && index < fields.Length)))); break;
                            case ProductType.Dairy: _assortment.Add(new DairyProducts(String.Join(";", fields.Where((_, index) => 0 < index && index < fields.Length)))); break;
                            case ProductType.Meat: _assortment.Add(new Meat(String.Join(";", fields.Where((_, index) => 0 < index && index < fields.Length)))); break;
                        }
                    }
                }
                file.Close();
            }
        }

        public void RemoveExpiredDiary(string path)
        {
            var expired = _assortment.OfType<DairyProducts>().Where(item => item.IsValid == false).ToList();
            expired.ForEach(item => _assortment.Remove(item));

            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine("EXPIRED PRODUCTS");
                foreach (var item in expired)
                {
                    file.WriteLine(item.ToString());
                }
                file.Close();
            }

        }
    }
}
