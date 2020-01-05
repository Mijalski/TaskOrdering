using System;
using System.Linq;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Models.Instance;
using TaskOrdering.Services.Solution;
using TaskOrdering.Solvers;

namespace TaskOrdering.Services.Instance
{
    public class InstanceValidator
    {
        private readonly InstanceLoader _instanceLoader;
        private readonly ISolver _dummySolver;
        private readonly SolutionLoader _solutionLoader;
        private readonly SolutionSaver _solutionSaver;

        public InstanceValidator()
        {
            _instanceLoader = new InstanceLoader();
            _dummySolver = new DummySolver();
            _solutionLoader = new SolutionLoader();
            _solutionSaver = new SolutionSaver();
        }

        public async Task ValidateInstanceAsync(string instanceFileName, string solutionFileName)
        {
            var instanceToValidate = await _instanceLoader.LoadInstanceAsync(instanceFileName);
            var solutionToValidate = await _solutionLoader.LoadSolutionAsync(solutionFileName);
            var mySolution = new Models.Solving.Solution();

            foreach (var processor in solutionToValidate.Processors)
            {
                foreach (var task in processor.TasksScheduled)
                {
                    var taskToAdd = instanceToValidate
                        .TasksToSchedule
                        .Single(_ => _.Id == task.Id);

                    mySolution
                        .Processors
                        .Single(_ => _.Id == processor.Id)
                        .AddTaskScheduled(taskToAdd);
                }
            }

            await _solutionSaver.SaveInstanceAsync(mySolution, $"validate-{instanceFileName}-{solutionFileName}");
        }
    }
}