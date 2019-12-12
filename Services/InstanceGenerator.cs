using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TaskOrdering.Services
{
    public class InstanceGenerator
    {
        private readonly InstanceSaver _instanceSaver;
        public InstanceGenerator()
        {
            _instanceSaver = new InstanceSaver();
        }

        public async Task GenerateInstancesAsync()
        {
            for (var i = 50; i <= 500; i += 50)
            {
                var instance = GenerateSingleInstance(i);
                await _instanceSaver.SaveInstanceAsync(instance, i);
            }
        }

        private string GenerateSingleInstance(int size, int timeRange = 50, int cpuCount = 4)
        {
            var instanceSb = new StringBuilder();
            var buffer = Math.Floor(0.4 * timeRange);
            instanceSb.Append($"{size}\r\n");

            for (var i = 0; i < size; i++)
            {
                var p = Math.Floor(Random() * timeRange) + 1;
                var r = Math.Floor(Random() * timeRange / cpuCount + i / cpuCount);
                var d = r + p + Math.Floor(Random() * buffer) + 1;
                instanceSb.Append($"{p} {r} {d}\r\n");
            }

            return instanceSb.ToString();
        }

        private static double Random()  
        {  
            var random = new Random();  
            return random.NextDouble();  
        }  
    }
}