using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskOrdering.Models;

namespace TaskOrdering.Services
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
