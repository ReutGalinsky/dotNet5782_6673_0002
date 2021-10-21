using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            int x = 3;
            string str = "jnjn";
//            Console.WriteLine(x.GetType().GetInterface("IEnumerable") != null);//   IEnumerable<int>);
            Console.WriteLine(str is IEnumerable);//   IEnumerable<int>);
        }
    }
}
