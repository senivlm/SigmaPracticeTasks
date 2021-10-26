namespace Task1
{
    class SortDelegate
    {делегат краще було винести поза клас
        public delegate int CompareObjects(object obj1, object obj2);
        public static int Sort(object[] objArray, CompareObjects compareMethod)
        {
            for (int i = 0; i < objArray.Length; ++i)
            {
                for (int j = 0; j < objArray.Length - i - 1; ++j)
                {
                    if (compareMethod(objArray[j], objArray[j+1]) > 0)
                    {
                        (objArray[j], objArray[j + 1]) = (objArray[j + 1], objArray[j]);
                    }
                }
            }
            return 0;
        }
    }
}
