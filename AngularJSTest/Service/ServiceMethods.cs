

namespace AngularJSTest.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    /// <summary>
    /// The service methods.
    /// </summary>
    public class ServiceMethods
    {
        /// <summary>
        /// Get random numbers.
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        /// <param name="toCount">
        /// The to count.
        /// </param>
        /// <param name="numberOfElements">
        /// The number of elements.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<int> GetRandomNumbers(int from, int toCount, int numberOfElements)
        {
            Random random = new Random();
            return Enumerable.Range(from, toCount).OrderBy(n => random.Next()).Take(numberOfElements).ToList();
        }

        /// <summary>
        /// The list to string converter.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The string result <see cref="string"/>.
        /// </returns>
        public static string ListToString<T>(List<T> list)
        {
            StringBuilder itemsString = new StringBuilder();
            foreach (var item in list)
            {
                itemsString.Append(Environment.NewLine + item);
            }

            return itemsString.ToString();
        }

        /// <summary>
        /// The compare lists.
        /// </summary>
        /// <param name="list1">
        /// The list 1.
        /// </param>
        /// <param name="list2">
        /// The list 2.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// If lists are equal disregarding seqience<see cref="bool"/>.
        /// </returns>
        public static bool CompareLists<T>(List<T> list1, List<T> list2)
        {
            var hashSet1 = new HashSet<T>(list1);
            var hashSet2 = new HashSet<T>(list2);
            return hashSet1.SetEquals(hashSet2) && list1.Count.Equals(list2.Count);
        }
    }
}
