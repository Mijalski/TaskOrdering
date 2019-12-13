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
            var tasksOrderedByReadyTime = instanceToSolve
                .TasksToSchedule
                .OrderBy(_ => _.ReadyTime)
                .ToList();

            var solution = new Solution();

            foreach (var taskToSchedule in tasksOrderedByReadyTime)
            {
                var processor = solution.GetFirstFreeProcessor();
                processor.AddTaskScheduled(taskToSchedule);
            }

            return solution;
        }
    }
}