using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6673();
            Welcome0002();
            Console.ReadKey();

        }
        static partial void Welcome0002();
        private static void Welcome6673()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
