using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rolcore.Collections
{
    /// <summary>
    /// Provides extension methods related to lists.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Randomizes the order of items in the specified list.
        /// </summary>
        /// <param name="list">Specifies the list to shuffle.</param>
        public static void Shuffle(this IList list)
        {
            if (list == null || list.Count == 0)
                throw new ArgumentException("list is null or empty.", "list");
            
            int maxRandom = list.Count;
            for (int i = 0; i < list.Count; i++)
            {
                int randomIndex = ThreadSafeRandom.Next(i, maxRandom);
                object randomItem = list[randomIndex];
                object currentItem = list[i];

                list[i] = randomItem;
                list[randomIndex] = currentItem;
            }
        }

        /// <summary>
        /// Randomizes the order of items in the specified list.
        /// </summary>
        /// <param name="list">Specifies the list to shuffle.</param>
        public static void Shuffle(this object[] list)
        {
            if (list == null || list.Length == 0)
                throw new ArgumentException("list is null or empty.", "list");
            
            int maxRandom = list.Length;
            for (int i = 0; i < list.Length; i++)
            {
                int randomIndex = ThreadSafeRandom.Next(i, maxRandom);
                object randomItem = list[randomIndex];
                object currentItem = list[i];

                list[i] = randomItem;
                list[randomIndex] = currentItem;
            }
        }

        //public static void Shuffle(this System.Array list)
        //{
        //    Shuffle(list as IList);
        //}

        public static IEnumerable<T> Duplicates<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
        {
            if (list1 == null)
                throw new ArgumentNullException("list1", "list1 is null.");
            if (list2 == null)
                throw new ArgumentNullException("list2", "list2 is null.");
            
            List<T> result = new List<T>(list1.Where(l1Item => list2.Contains(l1Item)));
            result.AddRange(list2.Where(l2Item => (list1.Contains(l2Item) && !result.Contains(l2Item))));

            return result;
        }

        public static IEnumerable<T> Uniques<T>(this IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list", "list is null.");

            List<T> result = new List<T>();
            foreach (T item in list)
            {
                if (!result.Contains(item))
                    result.Add(item);
            }
            return result;
        }
    }
}
