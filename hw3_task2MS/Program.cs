using System;

namespace hw3b
{

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MagicSquare matr = new MagicSquare(3);
                System.Console.WriteLine(matr.ToString());

                MagicSquare matr2 = new MagicSquare(5);
                System.Console.WriteLine(matr2.IsSquare());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}
