using System;

namespace hw4
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
                Polynomial pnRes = pn.Add(pn2);
                pnRes = pn.Subtract(pn2);
        
                Console.WriteLine("Construtor test\n" + pn.ToString());
                Console.WriteLine("Constructor test 2\n" + pn2.ToString());
                Console.WriteLine("Subtract test\n" + pnRes.ToString());

                Polynomial pnParseTest = new Polynomial();
                string s = "2*x^ 1 + 2*x^2 + 4*x^3 + -5,5*x^4 + 6.6*x^5 + 7*x^6 + 8*x^7";
                pnParseTest = Polynomial.Parse(s);
                System.Console.WriteLine($"Parse test:\n Input: {s}\nOutput:\n" + pnParseTest.ToString());
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
