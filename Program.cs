using System;
using System.Threading.Tasks;
using TaskOrdering.Services;

namespace TaskOrdering
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await new InstanceProgram().Main(args);
        }
    }
}
