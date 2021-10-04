using System;

namespace hw4_task1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputMeat = "Beef Steak;100;0.5;30;24.03.2021;High;Beef";
                string inputDairy = "Milk;40;0.5;25;24.04.2020";
                string inputProduct = "Rice;30;0.5;50;30.05.2021";
                
                Product p = new Product(inputProduct);
                Meat m = new Meat(inputMeat);
                DairyProducts d = new DairyProducts(inputDairy);

                Storage s = new Storage(@"productData.txt");
                s.RemoveExpiredDiary(@"expiredDairy.txt");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
