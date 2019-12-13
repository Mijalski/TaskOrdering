using System.Collections.Generic;
using System.Linq;

namespace TaskOrdering.Models.Solving
{
    public class Solution
    {
        public List<SolutionProcessor> Processors = new List<SolutionProcessor>();
        public long ExpectedPenalty { get; set; }

        public Solution()
        {
            for (var i = 0; i < Program.CORE_COUNT; i++)
            {
                Processors.Add(new SolutionProcessor(i));
            }
        }

        public SolutionProcessor GetFirstFreeProcessor()
        {
            return Processors.OrderBy(_ => _.FinishTime).First();
        }

        public long GetPenalty()
        {
            long overallPenalty = 0;
            foreach (var processor in Processors)
            {
                overallPenalty += processor.TasksScheduled.Sum(_ => _.Penalty);
            }

            return overallPenalty;
        }

    }
}