using System;
using System.Linq;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Models;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;

namespace TaskOrdering.Solvers
{
    public class RandomSolver : ISolver
    {

        public async Task<Solution> Solve(Instance instanceToSolve)
        {
            Solution bestSolution = null;
            var tasksCount = instanceToSolve.TasksToSchedule.Count;
            
            var roundsCount = 800;
            if (tasksCount <= 100)
                roundsCount *= 8;
            if (tasksCount <= 200)
                roundsCount *= 5;
            else if (tasksCount <= 300)
                roundsCount *= 3;


            for (var i = 0; i < roundsCount; i++)
            {
                var random = new Random();
                var randomlyOrderedTasks = instanceToSolve
                    .TasksToSchedule
                    .Select(x => new
                    {
                        x.Id,
                        x.Deadline,
                        x.TimeToComplete,
                        x.ReadyTime,
                        Weight = x.Deadline * random.NextDouble()
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

                foreach (var taskToSchedule in randomlyOrderedTasks)
                {
                    var processor = solution.GetFirstFreeProcessor();
                    processor.AddTaskScheduled(taskToSchedule);

                    if (bestSolution == null || solution.GetPenalty() < bestSolution.GetPenalty())
                    {
                        bestSolution = solution;
                    }
                }
            }

            return bestSolution;
        }
    }
}