using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MatrixForEach
{
    class Matrix : IEnumerator, IEnumerable
    {
        private double[,] _data;
        private int _rowSize;
        private int _colSize;
        private (int,int) _position;

        public int Rows { get => _rowSize; }
        public int Columns { get => _colSize; }

        public bool MoveNext()
        {
            if (_position.Item1 < 0)
            {
                return false;
            }
            --_position.Item2;

            if (_position.Item2 >= 0)
            {
                if (_position.Item1 >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                _position.Item2 = _colSize - 1;
                return --_position.Item1 >= 0;
                
            }

            
        }

        public void Reset()
        {
            _position = (_rowSize, _colSize);
        }

        public object Current => _data[_position.Item1, _position.Item2];

        public double this[int x, int y]
        {
            get
            {
                if (x >= 0 && x < _rowSize && y >= 0 && y < _colSize)
                {
                    return _data[x, y];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (x >= 0 && x < _rowSize && y >= 0 && y < _colSize)
                {
                    _data[x, y] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }

        public Matrix()
        {
            _data = new double[1, 1];
            _rowSize = _data.GetLength(0);
            _rowSize = _data.GetLength(1);
        }

        public Matrix(int rows, int cols)
        {
            Random random = new Random();
            _data = new double[rows, cols];
            _rowSize = rows;
            _colSize = cols;
            for (int i = 0; i < rows; ++i)
            {
                for (int j = 0; j < cols; ++j)
                {
                    _data[i, j] = random.Next(0,50);
                }
            }
            _position = (rows-1, cols);
        }

        public Matrix(double[,] data)
        {
            _data = data;
            _rowSize = data.GetLength(0);
            _colSize = data.GetLength(1);
        }

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)
                this;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for (int i = 0; i < Rows; ++i)
            {
                for (int j = 0; j < Columns; ++j)
                {
                    sb.Append($"{_data[i, j],-4}");
                }

                sb.Append('\n');
            }
            return sb.ToString();
        }

        public string ToStringForeach()
        {
            int counter = 0;
            StringBuilder sb = new();
            foreach (double element in this)
            {
                sb.Append($"{element,-4}");
                ++counter;
                if (counter == Columns)
                {
                    sb.Append('\n');
                    counter = 0;
                }
            }

            return sb.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Custom foreach test for Matrix class");

            Matrix matrix = new Matrix(8, 5);
           
            Console.WriteLine($"Matrix printed with standard method\n{matrix}\nMatrix printed using modified foreach\n{matrix.ToStringForeach()}");

        }
    }
}
