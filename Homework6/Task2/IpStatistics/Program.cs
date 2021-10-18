using System;

namespace IpStatistics
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IpStatistics ipStat = new IpStatistics(@"C:\Users\mykha\Desktop\Sigma_School\homework\hw6\IpStatistics\IpStatistics\ipStat.txt");
                Console.WriteLine(ipStat.GetIndividualStatistics());
                Console.WriteLine(ipStat.GetGeneralStatistics());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
