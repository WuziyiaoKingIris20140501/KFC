using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.ObjectCollection
{
    /// <summary>
    /// 对IList对象中包含的Item,进行排序
    /// 适用于Collection类型
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// 根据指定集合的索引，交换指定索引中的Item的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public static void Swap<T>(IList<T> list, int i, int j)
        {
            T tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }

        /// <summary>
        /// 交换2个相同对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T temp = lhs;
            lhs = rhs;
            rhs = temp;
        }


        #region Quick Sort


        private static int Partition<T>(IList<T> list, int begin, int end, IComparer<T> comparer)
        {
            int pivotIndex = (begin + end) >> 1;
            T pivot = list[pivotIndex];
            Swap<T>(list, pivotIndex, end); //将枢轴移到最后
            int i = begin - 1;
            for (int j = begin; j < end; j++)
            {
                if (comparer.Compare(list[j], pivot) < 0)
                {
                    i++;
                    Swap<T>(list, i, j);
                }
            }
            Swap<T>(list, i + 1, end);
            return i + 1;
        }

        private static void QuickSort<T>(IList<T> list, int begin, int end, IComparer<T> comparer)
        {
            if (end - begin <= 10)
            {
                InsertionSort<T>(list, begin, end, comparer);
            }
            else
            {
                if (begin < end)
                {
                    int idx = Partition<T>(list, begin, end, comparer);
                    QuickSort<T>(list, begin, idx - 1, comparer);
                    QuickSort<T>(list, idx + 1, end, comparer);
                }
            }
        }

        /// <summary>
        /// 快速排序(在集合中的对象需要继承于IComparable接口)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void QuickSort<T>(IList<T> list, IComparer<T> comparer)
        {
            QuickSort<T>(list, 0, list.Count - 1, comparer);
        }

        /// <summary>
        /// 快速排序(在集合中的对象需要继承于IComparable接口)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void QuickSort<T>(IList<T> list)
        {
            QuickSort<T>(list, Comparer<T>.Default);
        }


        #endregion


        #region InsertionSort

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="comparer"></param>
        public static void InsertionSort<T>(IList<T> list, int begin, int end, IComparer<T> comparer)
        {
            int i, j;
            T tmp;
            for (i = begin + 1; i <= end; i++)
            {
                tmp = list[i];
                j = i - 1;
                //与已排序的数逐一比较, 大于tmp时, 该数移后
                while (j >= begin
                    && comparer.Compare(list[j], tmp) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = tmp;
            }
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void InsertionSort<T>(IList<T> list, IComparer<T> comparer)
        {
            InsertionSort<T>(list, 0, list.Count - 1, comparer);
        }

        /// <summary>
        /// 插入排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void InsertionSort<T>(IList<T> list)
        {
            InsertionSort<T>(list, Comparer<T>.Default);
        }

        #endregion
    }
}
