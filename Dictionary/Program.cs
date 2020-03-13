using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    class Program
    {
        static void Main()
        {
            var dictionary = new Dictionary<int, string>(5);
            dictionary.Add(1, "a");
            dictionary.Add(2, "b");
            dictionary.Add(10, "c");
            dictionary.Add(7, "d");
            dictionary.Add(12, "e");

  
            foreach (KeyValuePair<int, string> kvp in dictionary)
            {
                Console.WriteLine("Key = {0}, Value = {1}",
                    kvp.Key, kvp.Value);
            }

            var keys = dictionary.Keys;

            foreach (int s in keys)
            {
                Console.WriteLine("Key = {0}", s);
            }
        }
    }
}
