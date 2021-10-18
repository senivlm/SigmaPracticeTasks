using System;

namespace hw1
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Product CPU = new("Intel I9-9900K", 449.90, 0.10);
                Product GPU = new("nVidia GeForce GTX 3090 Ti", 500, 1);
                Product RAM = new("Kingston 3200 16 GB", 200, 0.10);
                Product SSD = new("Samsung EVO 980 m.2 1TB", 600, 0.1);

                Console.WriteLine(Check.PrintProduct(CPU));
                Console.WriteLine(Check.PrintProduct(SSD));

                Buy PCPurchase = new(CPU, GPU, RAM, SSD);
                Buy ShopPurchase = new(CPU, -10);

                Console.WriteLine("\nPC purchase:\r\b" + Check.PrintBuy(PCPurchase));
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nError occured:\n" + ex.Message + "\nExiting programm");
                return;
            }
        }
    }
}
