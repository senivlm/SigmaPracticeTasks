using System;
using Task.Classes;

namespace Task
{
    class Program
    {
        static void DemonstrateEvents()
        {
            try
            {
                Storage storage = new Storage();

                storage.OnWrongInput += StorageEvents.LogWrongInput;
                storage.OnWrongInput += StorageEvents.AskForCorrectInput;

                storage.OnExpiredSearch += StorageEvents.LogExpiredProducts;
                storage.OnExpiredSearch += StorageEvents.ActionExpiredProducts;

                storage.ReadFromFile(
                    @"C:\Users\mykha\Desktop\Sigma_School\practs\19_10_2021\PractTask\PractTask\productList.txt");
                storage.AddProduct(new Product("Carrot; 24 ; 1 ; 30 ; 24.10.2021"));

                Console.WriteLine(new string('-', 100) + "\nList from file after adding carrot\n" + storage);

                storage.RemoveByName("Rice", true);

                Console.WriteLine(new string('-', 100) + "\nList from file after deleting rice\n" + storage);

                Console.WriteLine(new string('-', 100) + "\nNew List of products with price 30 - 50\n" +
                                  StorageManager.SearchByPrice(30, 50, storage));
                storage.ShowProductsInConsole();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception was caught:\n" + e.Message);
            }
        }
        static void Main(string[] args)
        {
            DemonstrateEvents();
        }
    }
}
