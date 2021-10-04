using System;

namespace hw3
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = @"energy.txt";
                FileWorker fw = new FileWorker(path);
                EnergyInfo energyInfo = new EnergyInfo(fw.GetNumberFlat(), fw.GetListOfCounters(), 2, (Quarter)fw.GetQuarter());
                System.Console.WriteLine(energyInfo.ToString());
                System.Console.WriteLine(energyInfo.ToString(23));
                System.Console.WriteLine(energyInfo.FindBiggestBill());
                System.Console.WriteLine(energyInfo.FindFlatWithoutUsing());
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

        }
    }
}
