using System;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string menuPath = $@"menu.txt";
                string pricePath = $@"prices.txt";

                ProductManager pm = new ProductManager(menuPath, pricePath);

                Console.WriteLine(pm.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return;
        }
    }
}
