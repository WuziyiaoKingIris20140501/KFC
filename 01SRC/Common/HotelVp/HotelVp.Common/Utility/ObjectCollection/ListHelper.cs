using System;
using System.Collections.Generic;
using System.Text;

namespace HotelVp.Common.Utilities.ObjectCollection
{
    /// <summary>
    /// 提供对IList类型实例进行比较的方法集合
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// 对象比较方法的委托
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalItem">原始IList对象中包含的Item</param>
        /// <param name="newItem">变动后IList对象中包含的Item</param>
        /// <returns></returns>
        public delegate bool MatchDelegate<T>(T originalItem, T newItem);
        /// <summary>
        /// 根据提供的比较方法委托实例，查找变动后的IList对象被删除的Item
        /// <remarks>1.当接收原始集合不为null，变动后的集合为null时，将直接返回原始的集合;
        /// 2.当原始集合为null，那么直接返回null
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">原始IList对象</param>
        /// <param name="newList">变动后IList对象</param>
        /// <param name="matchCondition">对象比较方法的委托实例</param>
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
            //如果新的IList集合为null，就默认返回originalList
            if (newList == null && originalList !=null)
            {
                deleteList = originalList;
            }
            //当originalList为null，就返回null；
            if (originalList == null && (newList != null || newList == null))
            {
                deleteList = null;
            }
            return deleteList;
        }


        /// <summary>
        /// 根据提供的比较方法委托实例，查找变动后的IList对象中新增的Item
        /// <remarks>1.当接收原始集合为null，变动后的集合不为null时，将直接返回变动的集合;
        /// 2.当变动的集合为null，那么直接返回null
        /// </remarks>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">原始IList对象</param>
        /// <param name="newList">变动后IList对象</param>
        /// <param name="matchCondition">对象比较方法的委托实例</param>
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
            //如果原有IList集合为null，就默认返回修改后的IList
            if (originalList == null && newList != null)
            {
                addList = newList;
            }
            //当newList为null，就返回null；
            if (newList == null && (originalList != null || originalList == null))
            {
                addList = null;
            }
            return addList;
        }

        /// <summary>
        /// 根据提供的比较方法委托实例，查找变动后的IList对象有信息变动的Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList">原始IList对象</param>
        /// <param name="newList">变动后IList对象</param>
        /// <param name="matchCondition">对象比较方法的委托实例</param>
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
        /// 从指定的对象集合中，查找提供的Item是否存在
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
        /// 删除集合中重复的项
        /// </summary>
        /// <typeparam name="T">T必须继承至IList接口</typeparam>
        /// <param name="originalList">需要进行删除操作的集合</param>
        /// <param name="match">重复的规则</param>
        /// <param name="comparer">删除前需要对集合进行排序，用于指定排序规则, 如果传入值为Null, 则使用Comparer<T>.Default进行排序.</param>
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
        /// 删除集合中重复的项(删除前需要对集合进行排序, 使用Comparer<T>.Default进行排序.)
        /// </summary>
        /// <typeparam name="T">T必须继承至IList接口</typeparam>
        /// <param name="originalList">需要进行删除操作的集合</param>
        /// <param name="match">重复的规则</param>
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
