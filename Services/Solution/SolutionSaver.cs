using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskOrdering.Services.Solution
{
    public class SolutionSaver
    {
        public SolutionSaver()
        {
            var penaltiesSavePath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"penalties.txt");
            using (var outputFile = new StreamWriter(penaltiesSavePath, false))
            {
                outputFile.WriteLine(string.Empty);
            }    
        }

        public async Task SaveInstanceAsync(Models.Solving.Solution solution, string solutionName, long? timeElapsed = null)
        {
            var savePath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"{solutionName}.txt");
            Console.WriteLine($"Penalty: {solution.GetPenalty()} ({solutionName})");
            using (var outputFile = new StreamWriter(savePath, false))
            {
                await outputFile.WriteLineAsync($"{solution.GetPenalty()}");

                foreach (var processor in solution.Processors)
                {
                    var processorTasksString = string.Join(" ", processor.TasksScheduled.Select(_ => _.Id));
                    await outputFile.WriteLineAsync(processorTasksString);
                }
            }

            var penaltiesSavePath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"penalties.txt");
            using (var outputFile = new StreamWriter(penaltiesSavePath, true))
            {
                await outputFile.WriteLineAsync($"{solutionName};{solution.GetPenalty()}" +
                                                $"{(timeElapsed == null ? string.Empty : $";{timeElapsed}")}");
            }
        }
    }
}