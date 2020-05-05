using LinqConsoleApp;
using System;
using System.Linq;
using System.Reflection;

namespace Ćwiczenia9
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples samples = new LinqSamples();
            var methods = samples.GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(item => item.Name.StartsWith("Przyklad"));
            foreach(var method in methods)
            {
                method.Invoke(samples, new object[0]);
                Console.WriteLine();
            }
        }
    }
}
