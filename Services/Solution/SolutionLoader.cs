using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;

namespace TaskOrdering.Services.Solution
{
    public class SolutionLoader
    {
        public async Task<Models.Solving.Solution> LoadSolutionAsync(string fileName)
        {
            var readPath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"{fileName}.txt");

            var solution = new Models.Solving.Solution();
            using (var file = new StreamReader(readPath, false))
            {
                string line;
                var counter = 0;
                while((line = await file.ReadLineAsync()) != null)
                {
                    var taskIdsForProcessor = line.Split(' ');
                    if (taskIdsForProcessor.Length == 1)
                    {
                        solution.ExpectedPenalty = long.Parse(taskIdsForProcessor.First());
                    }
                    else
                    {

                        foreach (var taskId in taskIdsForProcessor)
                        {
                            solution.Processors[counter].TasksScheduled.Add(
                                new TaskScheduled
                                {
                                    Id = int.Parse(taskId)
                                });
                        }

                        counter++;
                    }
                }

                return solution;
            }
        }
    }
}
