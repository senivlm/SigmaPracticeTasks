using System;

namespace hw2
{

    public class Program
    {// У Вас неправильна нумерація завдань. Залиштеся для індивідуальної перевірки
        static void Main(string[] args)
        {
            try
            {// об'єкти з маленької літери
                Product Processor = new("Intel I9-9900K", 449.90, 0.10);

                Meat Beef = new Meat("Beef meat", 20, 20, MeatCategory.First, MeatSort.Beef);

                Buy ProcessorPurchase = new(Processor, 20);

                DairyProducts Dairy = new DairyProducts("milk", 20, 1, 30);

                Storage Assortment = new Storage(Processor, Beef, Dairy);
                System.Console.WriteLine(Assortment.ToString());

                Storage SearchMeatResult = Assortment.SearchForMeat();
                System.Console.WriteLine(SearchMeatResult.ToString());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nError occured:\n" + ex.Message + "\nExiting programm");
                return;
            }
            
        }
    }
}
