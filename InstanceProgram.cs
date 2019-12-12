using System.Threading.Tasks;
using TaskOrdering.Services;

namespace TaskOrdering
{
    public class InstanceProgram
    {
        private readonly InstanceValidator _instanceValidator;
        private readonly InstanceGenerator _instanceGenerator;
        private readonly InstanceLoader _instanceLoader;


        public InstanceProgram()
        {
            _instanceValidator = new InstanceValidator();
            _instanceGenerator = new InstanceGenerator();
            _instanceLoader = new InstanceLoader();
        }

        public async Task Main(string[] args)
        {
            if (args.Length > 0)
            {
                switch (args[0])
                {
                    case "--generate":
                        _instanceGenerator.GenerateInstances();
                        break;
                    case "--validate":
                        _instanceValidator.ValidateInstance(args[1]);
                        break;
                }
            }
            else
            {

            }
        }
    }
}