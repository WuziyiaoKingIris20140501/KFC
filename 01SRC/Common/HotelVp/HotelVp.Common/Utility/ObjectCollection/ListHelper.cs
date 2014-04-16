using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.ObjectCollection
{
    /// <summary>
    /// �ṩ��IList����ʵ�����бȽϵķ�������
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// ����ȽϷ�����ί��
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalItem">ԭʼIList�����а�����Item</param>
        /// <param name="newItem">�䶯��IList�����а�����Item</param>
        /// <returns></returns>
        public delegate bool MatchDelegate<T>(T originalItem, T newItem);
        /// <summary>
        /// �����ṩ�ıȽϷ���ί��ʵ�������ұ䶯���IList����ɾ����Item
        /// <remarks>1.������ԭʼ���ϲ�Ϊnull���䶯��ļ���Ϊnullʱ����ֱ�ӷ���ԭʼ�ļ���;
        /// 2.��ԭʼ����Ϊnull����ôֱ�ӷ���null
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">ԭʼIList����</param>
        /// <param name="newList">�䶯��IList����</param>
        /// <param name="matchCondition">����ȽϷ�����ί��ʵ��</param>
        /// <returns></returns>
        public static IList<T> FindDelete<T>(IList<T> originalList, IList<T> newList, MatchDelegate<T> matchCondition)
        {
            IList<T> deleteList = new List<T>();
            
            if (originalList != null && newList != null)
            {
                foreach (T originalItem in originalList)
                {
                   
                    bool exists = Exists<T>(newList,delegate(T newItem)
                    {
                        return matchCondition(originalItem, newItem);
                    });

                    if (!exists)
                    {
                        deleteList.Add(originalItem);
                    }
                }
            }
            //����µ�IList����Ϊnull����Ĭ�Ϸ���originalList
            if (newList == null && originalList !=null)
            {
                deleteList = originalList;
            }
            //��originalListΪnull���ͷ���null��
            if (originalList == null && (newList != null || newList == null))
            {
                deleteList = null;
            }
            return deleteList;
        }


        /// <summary>
        /// �����ṩ�ıȽϷ���ί��ʵ�������ұ䶯���IList������������Item
        /// <remarks>1.������ԭʼ����Ϊnull���䶯��ļ��ϲ�Ϊnullʱ����ֱ�ӷ��ر䶯�ļ���;
        /// 2.���䶯�ļ���Ϊnull����ôֱ�ӷ���null
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">ԭʼIList����</param>
        /// <param name="newList">�䶯��IList����</param>
        /// <param name="matchCondition">����ȽϷ�����ί��ʵ��</param>
        /// <returns></returns>
        public static IList<T> FindAdd<T>(IList<T> originalList, IList<T> newList, MatchDelegate<T> matchCondition)
        {
            IList<T> addList = new List<T>();
            if (originalList != null && newList != null)
            {
                foreach (T newItem in newList)
                {
                    bool exists = Exists<T>(originalList, delegate(T originalItem)
                    {
                        return matchCondition(originalItem, newItem);
                    });

                    if (!exists)
                    {
                        addList.Add(newItem);
                    }
                }
                
            }
            //���ԭ��IList����Ϊnull����Ĭ�Ϸ����޸ĺ��IList
            if (originalList == null && newList != null)
            {
                addList = newList;
            }
            //��newListΪnull���ͷ���null��
            if (newList == null && (originalList != null || originalList == null))
            {
                addList = null;
            }
            return addList;
        }

        /// <summary>
        /// �����ṩ�ıȽϷ���ί��ʵ�������ұ䶯���IList��������Ϣ�䶯��Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">ԭʼIList����</param>
        /// <param name="newList">�䶯��IList����</param>
        /// <param name="matchCondition">����ȽϷ�����ί��ʵ��</param>
        /// <returns></returns>
        public static IList<T> FindChange<T>(IList<T> originalList, IList<T> newList, MatchDelegate<T> matchCondition)
        {
            IList<T> changeList = new List<T>();
            if (originalList != null && newList != null)
            {
                foreach (T newItem in newList)
                {
                    T findItem = Find<T>(originalList,delegate(T originalItem)
                    {
                        return matchCondition(originalItem, newItem);
                    });
                    if (findItem != null)
                    {
                        changeList.Add(findItem);
                    }
                    
                }
            }
            return changeList;
        }

        /// <summary>
        /// ��ָ���Ķ��󼯺��У������ṩ��Item�Ƿ����
        /// </summary>
        /// <example><![CDATA[Predicate<DepartmentModel> m = new Predicate<DepartmentModel>(delegate(DepartmentModel originalItem)
        /// {return originalItem.Number == t.Number;});
        /// DepartmentModel result = ListHelper.Find<DepartmentModel>(originalList, m);]]></example>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList"></param>
        /// <param name="match"></param>
        /// <returns></returns>
        public static T Find<T>(IList<T> originalList, Predicate<T> match)
        {
            T result = default(T);
            if (originalList != null)
            {
                int index = FindIndex<T>(0, originalList.Count, originalList, match);
                if (index > -1)
                {
                    result = originalList[index];
                }
            }

            return result;
        }

        /// <summary>
        /// ɾ���������ظ�����
        /// </summary>
        /// <typeparam name="T">T����̳���IList�ӿ�</typeparam>
        /// <param name="originalList">��Ҫ����ɾ�������ļ���</param>
        /// <param name="match">�ظ��Ĺ���</param>
        /// <param name="comparer">ɾ��ǰ��Ҫ�Լ��Ͻ�����������ָ���������, �������ֵΪNull, ��ʹ��Comparer<T>.Default��������.</param>
        public static void RemoveDuplicateItem<T>(IList<T> originalList, MatchDelegate<T> match, IComparer<T> comparer)
        {
            if (originalList == null)
            {
                throw new ArgumentNullException("originalList");
            }

            if (match == null)
            {
                throw new ArgumentNullException("match");
            }

            if (comparer != null)
            {
                SortHelper.QuickSort<T>(originalList, comparer);
            }
            else 
            {
                SortHelper.QuickSort<T>(originalList, Comparer<T>.Default);
            }
            
            for (int i = 1; i < originalList.Count; i++)
            {
                if (match(originalList[i], originalList[i - 1])) 
                {
                    originalList.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary>
        /// ɾ���������ظ�����(ɾ��ǰ��Ҫ�Լ��Ͻ�������, ʹ��Comparer<T>.Default��������.)
        /// </summary>
        /// <typeparam name="T">T����̳���IList�ӿ�</typeparam>
        /// <param name="originalList">��Ҫ����ɾ�������ļ���</param>
        /// <param name="match">�ظ��Ĺ���</param>
        public static void RemoveDuplicateItem<T>(IList<T> originalList, MatchDelegate<T> match) 
        {
            RemoveDuplicateItem<T>(originalList, match, Comparer<T>.Default);
        }

        private static bool Exists<T>(IList<T> originalList,Predicate<T> match)
        {
            int index = FindIndex<T>(0, originalList.Count, originalList, match);
            return (index != -1);
        }


        private static int FindIndex<T>(int startindex, int count,IList<T> originalList, Predicate<T> match)
        {
            int result = -1;
            int size = originalList.Count;
            if (startindex <= size)
            {
                if ((count >= 0) && (startindex <= (size - count)))
                {
                    if (match != null)
                    {
                        int num = startindex + count;
                        for (int i = startindex; i < num; i++)
                        {
                            if (match(originalList[i]))
                            {
                                result= i;
                                break;
                            }
                        }
                    }
                }
                
            }
            return result;

        }

    }
}
