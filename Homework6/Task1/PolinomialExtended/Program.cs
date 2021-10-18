using System;
using System.Threading.Channels;

namespace PolinomialExtended
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Polynomial pn = new Polynomial(7);
                pn[3] = 0;
                Polynomial pn2 = new Polynomial(7);
                Polynomial pnResMethod = pn.Add(pn2);
                Polynomial pnResOperator = pn + pn2;

                Console.WriteLine("First polynomial\n" + pn.ToString());
                Console.WriteLine("Second polynomial\n" + pn2.ToString());
                Console.WriteLine($"Method add\n {pnResMethod}\nOperator add\n {pnResOperator}");

                pnResMethod = pn2.Subtract(pn);
                pnResOperator = pn2 - pn;

                Console.WriteLine($"Method subtract\n {pnResMethod}\nOperator subtract\n {pnResOperator}");

                pnResMethod = pn2.Multiply(pn);
                pnResOperator = pn * pn2;

                Polynomial pnImplicit = 5.3;
                Console.WriteLine($"Implicit conversation\n {pnImplicit}");
               
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

        }
    }
}
