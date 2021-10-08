using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;


namespace PracticeMatrix
{
    class Figure
    {
        private int[,,] _array;

        public int Height => _array.GetLength(0);
        public int Width => _array.GetLength(1);
        public int Depth => _array.GetLength(2);

        public int this[int x, int y, int z]
        {

            get
            {
                if ((x >= 0 && x < Height) && (y >= 0 && y < Width) && (z >= 0 && z <= Depth))
                {
                    return _array[x, y, z];
                }
                else throw new IndexOutOfRangeException("Index was out od bounds of array");
            }
            set
            {
                if ((x >= 0 && x < Height) && (y >= 0 && y < Width) && (z >= 0 && z <= Depth))
                {
                    _array[x, y, z] = value;
                }
                else throw new IndexOutOfRangeException("Index was out od bounds of array");
            }
        }

        public Figure(int size)
        {
            _array = new int[size, size, size];
        }

        public Figure(int x, int y, int z)
        {
            _array = new int[x, y, z];
        }

        public Figure(int[,,] array)
        {
            _array = array;
        }

        //0 - front, 1 - left, 2 - top
        public int[,] GetProjection(int side)
        {
            int[,] projection;
            if (side == 0)
            {
                projection = new int[Height, Width];
                for (int d = 0; d < Depth; ++d)
                {
                    for (int i = 0; i < Height; ++i)
                    {
                        for (int j = 0; j < Width; ++j)
                        {
                            if (_array[i, j, d] == 1)
                            {
                                projection[i, j] = 1;
                                continue;
                            }
                        }
                    }
                }
            }
            else if (side == 1)
            {
                projection = new int[Height, Depth];
                for (int d = 0; d < Width; ++d)
                {
                    for (int i = 0; i < Height; ++i)
                    {
                        for (int j = 0; j < Depth; ++j)
                        {
                            if (_array[i, d, j] == 1)
                            {
                                projection[i, Depth - j - 1] = 1;
                                continue;
                            }
                        }
                    }
                }
            }
            else if (side == 2)
            {
                projection = new int[Depth, Width];
                for (int d = 0; d < Height; ++d)
                {
                    for (int i = 0; i < Depth; ++i)
                    {
                        for (int j = 0; j < Width; ++j)
                        {
                            if (_array[d, j, i] == 1)
                            {
                                projection[Depth - 1 - i, j] = 1;
                                continue;
                            }
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Wrong argument of projection");
            }
            return projection;
        }

        public string GetPrintedProjections()
        {
            var sb = new StringBuilder();
            sb.Append("\nFront view\n");
            sb.Append(ArrayHelper.Array2DToString(this.GetProjection(0)));
            sb.Append("\nLeft view\n");
            sb.Append(ArrayHelper.Array2DToString(this.GetProjection(1)));
            sb.Append("\nTop view\n");
            sb.Append(ArrayHelper.Array2DToString(this.GetProjection(2)));
            return sb.ToString();
        }

        public static Figure GenerateTestExample()
        {
            return new Figure(3, 3, 3)
            {
                [1, 0, 1] = 1,
                [1, 1, 1] = 1,
                [1, 2, 1] = 1,
                [1, 0, 2] = 1,
                [0, 1, 1] = 1,
                [1, 1, 0] = 1
            };
        }
    }

    class ArrayHelper
    {
        public static string Array2DToString(int[,] array)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    sb.Append($"{array[i, j]}");
                }

                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Figure testFigure = Figure.GenerateTestExample();
            
            Console.WriteLine(testFigure.GetPrintedProjections());
           
        }
    }
}
