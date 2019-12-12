using System;
using System.IO;
using System.Threading.Tasks;

namespace TaskOrdering.Services
{
    public class InstanceValidator
    {
        private double totalSolutionPenalty;
        private readonly InstanceLoader _instanceLoader;

        public InstanceValidator()
        {
            _instanceLoader = new InstanceLoader();
        }

        public async Task ValidateInstanceAsync(string fileName)
        {
            var instanceToValidate = await _instanceLoader.LoadSolutionAsync(fileName);
            var maxTasks = (int) Math.Ceiling((double)instanceToValidate.TasksToSchedule.Count / Program.CORE_COUNT);
        }

        public async Task GenerateDummySolution()
        {

        }
    }
}