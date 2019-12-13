using System.Threading.Tasks;
using TaskOrdering.Models;
using TaskOrdering.Models.Instance;
using TaskOrdering.Models.Solving;

namespace TaskOrdering.Interfaces
{
    public interface ISolver
    {
        Task<Solution> Solve(Instance instanceToSolve);
    }
}