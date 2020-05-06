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

            var objects = samples
                .GetType()
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Where(item => item.Name.StartsWith("Przyklad"));

            foreach(var @object in objects)
            {
                @object.Invoke(samples, new object[0]);
            }
        }
    }
}
