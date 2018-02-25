

namespace AngularJSTest.Service
{
    using System;
    using System.Linq;

    public class ServiceMethods
    {
        public static int[] GetRandomNumbers(int from, int toCount, int numberOfElements)
        {
            Random random = new Random();
            return Enumerable.Range(from, toCount).OrderBy(n => random.Next()).Take(numberOfElements).ToArray();
        }
    }
}
