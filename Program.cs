using System;
using System.Threading.Tasks;
using TaskOrdering.Services;

namespace TaskOrdering
{
    class Program
    {
        public static int CORE_COUNT = 4;

        static void Main(string[] args)
        {
            new InstanceProgram().Main(args).GetAwaiter().GetResult();

            Console.WriteLine("Finished");
            Console.ReadKey();
        }
    }
}
