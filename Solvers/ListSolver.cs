using System;
using System.Linq;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Models;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;

namespace TaskOrdering.Solvers
{
    public class ListSolver : ISolver
    {
        public async Task<Solution> Solve(Instance instanceToSolve)
        {
            var tasksOrderedByReadyTime = instanceToSolve
                .TasksToSchedule
                .Select(x => new
                {
                    x.Id,
                    x.Deadline,
                    x.TimeToComplete,
                    x.ReadyTime,
                    Weight = x.Deadline - x.TimeToComplete + x.ReadyTime
                })
                .OrderBy(_ => _.Weight)
                .Select(x => new TaskToSchedule
                {
                    Id = x.Id,
                    Deadline = x.Deadline,
                    ReadyTime = x.ReadyTime,
                    TimeToComplete = x.TimeToComplete
                });

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