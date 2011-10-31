using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(list != null, "list");

            int maxRandom = list.Count - 1;
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
            Contract.Requires<ArgumentNullException>(list != null, "list");

            int maxRandom = list.Length - 1;
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
            Contract.Requires<ArgumentNullException>(list1 != null, "list1");
            Contract.Requires<ArgumentNullException>(list2 != null, "list2");

            List<T> result = new List<T>(list1.Where(l1Item => list2.Contains(l1Item)));
            result.AddRange(list2.Where(l2Item => (list1.Contains(l2Item) && !result.Contains(l2Item))));

            return result;
        }

        public static IEnumerable<T> Uniques<T>(this IEnumerable<T> list)
        {
            Contract.Requires<ArgumentNullException>(list != null, "list");

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
