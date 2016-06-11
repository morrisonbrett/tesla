using System;
using tesla;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"The answer is {new Thing().Get(42)}.");
        }
    }
}
