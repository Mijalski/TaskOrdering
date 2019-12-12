using System;
using System.Threading.Tasks;
using TaskOrdering.Services;

namespace TaskOrdering
{
    class Program
    {
        public static int CORE_COUNT = 4;

        static async Task Main(string[] args)
        {
            await new InstanceProgram().Main(args);
        }
    }
}
