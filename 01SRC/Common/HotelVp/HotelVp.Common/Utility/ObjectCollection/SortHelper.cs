using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.ObjectCollection
{
    /// <summary>
    /// ��IList�����а�����Item,��������
    /// ������Collection����
    /// </summary>
    public static class SortHelper
    {
        /// <summary>
        /// ����ָ�����ϵ�����������ָ�������е�Item��λ��
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
        /// ����2����ͬ����
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
            Swap<T>(list, pivotIndex, end); //�������Ƶ����
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
        /// ��������(�ڼ����еĶ�����Ҫ�̳���IComparable�ӿ�)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void QuickSort<T>(IList<T> list, IComparer<T> comparer)
        {
            QuickSort<T>(list, 0, list.Count - 1, comparer);
        }

        /// <summary>
        /// ��������(�ڼ����еĶ�����Ҫ�̳���IComparable�ӿ�)
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
        /// ��������
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
                //�������������һ�Ƚ�, ����tmpʱ, �����ƺ�
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
        /// ��������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="comparer"></param>
        public static void InsertionSort<T>(IList<T> list, IComparer<T> comparer)
        {
            InsertionSort<T>(list, 0, list.Count - 1, comparer);
        }

        /// <summary>
        /// ��������
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
