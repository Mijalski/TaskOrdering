using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Services;
using TaskOrdering.Services.Instance;
using TaskOrdering.Services.Solution;
using TaskOrdering.Solvers;

namespace TaskOrdering
{
    public class InstanceProgram
    {
        private readonly InstanceValidator _instanceValidator;
        private readonly InstanceGenerator _instanceGenerator;
        private readonly InstanceLoader _instanceLoader;

        private readonly ISolver _dummyInstanceSolver;
        private readonly SolutionSaver _solutionSaver;

        public InstanceProgram()
        {
            _instanceValidator = new InstanceValidator();
            _instanceGenerator = new InstanceGenerator();
            _instanceLoader = new InstanceLoader();
            _dummyInstanceSolver = new DummySolver();
            _solutionSaver = new SolutionSaver();
        }

        public async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                //var instance = await _instanceLoader.LoadInstanceAsync("in132285_50");
                //var solution = await _dummyInstanceSolver.Solve(instance);
                //await _solutionSaver.SaveInstanceAsync(solution, "out-dummy");
                await _instanceValidator.ValidateInstanceAsync("in132285_50", "out-dummy");
            }

            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "--generate":
                        await _instanceGenerator.GenerateInstancesAsync();
                        Console.WriteLine("Generated new instances");
                        break;
                    case "--dummy":
                        var instance = await _instanceLoader.LoadInstanceAsync(args[1]);
                        var solution = await _dummyInstanceSolver.Solve(instance);
                        await _solutionSaver.SaveInstanceAsync(solution, "out-dummy");
                        Console.WriteLine($"total penalty: {solution.GetPenalty()}");
                        break;
                    case "--validate":
                        await _instanceValidator.ValidateInstanceAsync(args[1], args[2]);
                        break;
                }
            }
        }
    }
}