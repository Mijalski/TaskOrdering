using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using TaskOrdering.Interfaces;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;
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
        private readonly ISolver _listInstanceSolver;
        private readonly SolutionSaver _solutionSaver;

        public InstanceProgram()
        {
            _instanceValidator = new InstanceValidator();
            _instanceGenerator = new InstanceGenerator();
            _instanceLoader = new InstanceLoader();
            _dummyInstanceSolver = new DummySolver();
            _listInstanceSolver = new ListSolver();
            _solutionSaver = new SolutionSaver();
        }

        public async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                var arguments = new [] { "--list", "in132285" };
                await Main(arguments);
                arguments = new [] { "--list", "in133865" };
                await Main(arguments);
                arguments = new [] { "--list", "in109679" };
                await Main(arguments);
                arguments = new [] { "--list", "in134392" };
                await Main(arguments);
                arguments = new [] { "--list", "in116658" };
                await Main(arguments);
                arguments = new [] { "--list", "in109678" };
                await Main(arguments);
                arguments = new [] { "--list", "in117286" };
                await Main(arguments);
                arguments = new [] { "--list", "127211" };
                await Main(arguments);
                arguments = new [] { "--list", "in122470" };
                await Main(arguments);
                arguments = new [] { "--list", "in106571" };
                await Main(arguments);
                arguments = new [] { "--list", "in133863" };
                await Main(arguments);
                arguments = new [] { "--list", "in117306" };
                await Main(arguments);
                arguments = new [] { "--list", "in109678" };
                await Main(arguments);
                //arguments = new [] { "--list", "in128998" };
                //await Main(arguments);
                arguments = new [] { "--list", "in133865" };
                await Main(arguments);
                arguments = new [] { "--list", "in133861" };
                await Main(arguments);
                //arguments = new [] { "--list", "in100629" };
                //await Main(arguments);
            }

            if (args.Length > 0)
            {
                Instance instance;
                Solution solution;
                switch (args[0])
                {
                    case "--generate":
                        await _instanceGenerator.GenerateInstancesAsync();
                        Console.WriteLine("Generated new instances");
                        break;
                    case "--dummy":
                        for (var i = 50; i <= 500; i += 50)
                        {
                            var fileName = $"{args[1]}_{i}";
                            instance = await _instanceLoader.LoadInstanceAsync(fileName);
                            solution = await _dummyInstanceSolver.Solve(instance);
                            await _solutionSaver.SaveInstanceAsync(solution, $"out-dummy-{fileName}");
                        }
                        break;
                    case "--list":
                        for (var i = 50; i <= 500; i += 50)
                        {
                            Stopwatch sw = new Stopwatch();
                            var fileName = $"{args[1]}_{i}";
                            instance = await _instanceLoader.LoadInstanceAsync(fileName);
                            sw.Start();
                            solution = await _listInstanceSolver.Solve(instance);
                            sw.Stop();
                            await _solutionSaver.SaveInstanceAsync(solution, $"out-list-{fileName}", sw.ElapsedMicroSeconds());
                        }
                        break;
                    case "--validate":
                        for (var i = 50; i <= 500; i += 50)
                        {
                            var fileName = $"{args[1]}_{i}";
                            await _instanceValidator.ValidateInstanceAsync(args[1], args[2]);
                        }
                        break;
                }
            }
        }
    }
}