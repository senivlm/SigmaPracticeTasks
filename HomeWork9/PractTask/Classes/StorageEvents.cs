using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Task.Classes
{
    public class StorageEvents
    {
        public static void AskForCorrectInput(string errorInfo, Storage storage)
        {
            Console.WriteLine("\nError occurred in following line\n" + errorInfo);
            Console.WriteLine("Do you want to enter correct data?\n y - enter new record\n n - cancel");
            string result = "";
            while (!Regex.IsMatch(result = Console.ReadLine(), @"y|n")) Console.WriteLine("Wrong input. Enter y or n");

            if (result == "y")
            {
                storage.ReadFromConsole(1);
            }

        }
        public static void LogWrongInput(string errorInfo, Storage storage)
        {

            using (StreamWriter file = new StreamWriter(@"C:\Users\mykha\Desktop\Sigma_School\practs\19_10_2021\PractTask\PractTask\log.txt", true))
            {
                file.WriteLine(new string('-', 100));
                file.WriteLine(DateTime.Now);
                file.WriteLine(errorInfo);
                file.WriteLine(new string('-', 100));

            }
        }

        public static void LogExpiredProducts(Storage storage, List<Product> expiredList)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\mykha\Desktop\Sigma_School\practs\19_10_2021\PractTask\PractTask\log.txt", true))
            {
                file.WriteLine(new string('-', 100));
                file.WriteLine(DateTime.Now);

                if (!expiredList.Any())
                {
                    file.WriteLine("There are no expired products in storage");
                }
                else
                {
                    file.WriteLine("Expired products:\n");
                    var sb = new StringBuilder();
                    foreach (var product in expiredList)
                    {
                        sb.Append(product.ToString() + "\n");
                    }

                    file.WriteLine(sb.ToString());
                }

                file.WriteLine(new string('-', 100));
            }
        }
        public static void ActionExpiredProducts(Storage storage, List<Product> expiredList)
        {
            if (!expiredList.Any())
            {
                return;
            }
            Console.WriteLine("Do you want to delete expired products from storage?\n y - yes, delete\n n - no, don't delete");
            string result = "";
            while (!Regex.IsMatch(result = Console.ReadLine(), @"y|n"))
            {
                Console.WriteLine("Wrong input. Enter y or n");
            }

            
            if (result == "y")
            {
                foreach (var product in expiredList)
                {
                    storage.Remove(product);
                }

            }
          
            using (StreamWriter file = new StreamWriter(@"C:\Users\mykha\Desktop\Sigma_School\practs\19_10_2021\PractTask\PractTask\log.txt", true))
            {
                file.WriteLine(new string('-', 100));
                file.WriteLine(DateTime.Now);

                file.WriteLine($"Expired products were{(result == "n" ? "n't" : "")} removed ");
                file.WriteLine(new string('-', 100));
            }
        }
    }
}