using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskOrdering.Services.Solution
{
    public class SolutionSaver
    {
        public async Task SaveInstanceAsync(Models.Solving.Solution solution, string solutionName)
        {
            var savePath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"{solutionName}.txt");
            using (var outputFile = new StreamWriter(savePath, false))
            {
                foreach (var processor in solution.Processors)
                {
                    var processorTasksString = string.Join(" ", processor.TasksScheduled.Select(_ => _.Id));
                    await outputFile.WriteLineAsync(processorTasksString);
                }

                await outputFile.WriteLineAsync($"{solution.GetPenalty()}");
            }
        }
    }
}