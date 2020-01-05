using System;
using System.Linq;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Models;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;

namespace TaskOrdering.Solvers
{
    public class DummySolver : ISolver
    {
        public async Task<Solution> Solve(Instance instanceToSolve)
        {
            var solution = new Solution();
            var totalTaskCount = instanceToSolve.TasksToSchedule.Count;
            var tasksPerProcessorCount = (int)
                Math.Ceiling((double) totalTaskCount / solution.Processors.Count);
            var processorId = 0;

            for(int i = 1; i <= totalTaskCount; i += tasksPerProcessorCount)
            {
                var processor = solution.Processors.Single(_ => _.Id == processorId);
                for(var j = 0; j < tasksPerProcessorCount; j++)
                {
                    var taskId = i + j;
                    var taskToSchedule = instanceToSolve
                        .TasksToSchedule.SingleOrDefault(x => x.Id == taskId);

                    if(taskToSchedule != null)
                        processor.AddTaskScheduled(taskToSchedule);
                }
                processorId++;
            }
            return solution;
        }
    }
}