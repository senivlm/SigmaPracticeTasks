using System;
using System.Text;

namespace hw2
{
    public class Storage
    {
        private Product[] _assortment;

        public Storage(int size)
        {
            if (size < 0)
            {
                throw new ArgumentException("Size of array can't be a negative number");
            }
            _assortment = new Product[size];
        }

        public Storage(params Product[] productArray)
        {
            _assortment = new Product[productArray.Length];
            for (int i = 0; i < _assortment.Length; ++i)
            {
                _assortment[i] = productArray[i];
            }
        }

        public Product this[int index]
        {
            get
            {
                if (index < 0 || index > _assortment.Length)
                {
                    throw new ArgumentException("Index was out of dounds of array");
                }
                return _assortment[index];
            }
            set
            {
                if (index < 0 || index > _assortment.Length)
                {
                    throw new ArgumentException("Index was out of dounds of array");
                }
                _assortment[index] = value;
            }
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            if (_assortment.Length == 0)
            {
                result.Append("Storage is empty");
                return result.ToString();
            }

            for (int i = 0; i < _assortment.Length; ++i)
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

            for (int i = 0, ind = 0; i < _assortment.Length; ++i)
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
            for (int i = 0; i < _assortment.Length; ++i)
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
                        _assortment[i] = new Meat(name, price, weight, mCategory, mSort);
                        break;
                    case 3:
                        Console.WriteLine("Enter days before expiration: ");
                        int expiration = Int32.Parse(Console.ReadLine());
                        _assortment[i] = new DairyProducts(name, price, weight, expiration);
                        break;
                    default:
                        _assortment[i] = new Product(name, price, weight);
                        break;
                }
            }
        }
    }
}
