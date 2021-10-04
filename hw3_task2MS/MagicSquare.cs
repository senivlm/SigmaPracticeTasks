using System;
using System.Linq;
using System.Text;

namespace hw3b
{
    class MagicSquare
    {
        private int[,] _matrix;
        private int _matrixSize;

        public int Size
        {
            get => _matrixSize; set
            {
                if ((value > 0) && (value % 2 == 1))
                {
                    _matrixSize = value;
                }
                else
                {
                    throw new ArgumentException("Size of matrix must be a positive odd number");
                }
            }
        }

        public int[,] Matrix
        {
            get => _matrix;

        }

        public MagicSquare(int size)
        {
            Size = size;
            _matrix = new int[Size, Size];
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    _matrix[i, j] = 0;
                }
            }
            BuildSquare();
        }

        private void BuildSquare()
        {
            int i = Size / 2;
            int j = Size - 1;
            for (int num = 1; num <= Size * Size;)
            {
                if (i == -1 && j == Size)
                {
                    j = Size - 2;
                    i = 0;
                }
                else
                {
                    if (j == Size)
                    {
                        j = 0;
                    }

                    if (i < 0)
                    {
                        i = Size - 1;
                    }
                }
                if (_matrix[i, j] != 0)
                {
                    j -= 2;
                    i++;
                    continue;
                }
                else
                {
                    _matrix[i, j] = num++;
                }

                j++;
                i--;
            }
            IsSquare();
        }

        public bool IsSquare()
        {
            int[] matrixSums = new int[Size + Size + 2];

            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    matrixSums[i] += _matrix[i, j];
                    matrixSums[matrixSums.Length / 2 - 1 + i] += _matrix[j, i];
                }
            }


            for (int i = 0; i < Size; ++i)
            {
                matrixSums[matrixSums.Length - 2] += _matrix[i, i];
                matrixSums[matrixSums.Length - 1] += _matrix[i, Size - i - 1];
            }

            bool result = matrixSums.Distinct().Count() == 1;
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    result.Append($"{Matrix[i, j],5} ");
                }
                result.Append("\n");
            }
            return result.ToString();
        }
    }
}
