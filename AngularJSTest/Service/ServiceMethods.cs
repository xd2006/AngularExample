

namespace AngularJSTest.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ServiceMethods
    {
        public static int[] GetRandomNumbers(int from, int toCount, int numberOfElements)
        {
            Random random = new Random();
            return Enumerable.Range(from, toCount).OrderBy(n => random.Next()).Take(numberOfElements).ToArray();
        }

        public static string ListToString<T>(List<T> list)
        {
            StringBuilder itemsString = new StringBuilder();
            foreach (var item in list)
            {
                itemsString.Append(Environment.NewLine + item);
            }

            return itemsString.ToString();
        }
    }
}
