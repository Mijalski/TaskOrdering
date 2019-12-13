using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskOrdering.Services.Instance
{
    public class InstanceSaver
    {
        public async Task SaveInstanceAsync(string instance, int instanceSize)
        {
            var savePath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"in132285_{instanceSize}.txt");
            using (var outputFile = new StreamWriter(savePath, false))
            {
                await outputFile.WriteLineAsync(instance);
            }
        }
    }
}
