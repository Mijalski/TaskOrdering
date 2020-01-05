using System.Collections.Generic;
using System.Linq;
using TaskOrdering.Models.Instance;

namespace TaskOrdering.Models.Solving
{
    public class SolutionProcessor
    {
        public List<TaskScheduled> TasksScheduled { get; set; } = new List<TaskScheduled>();
        public int Id { get; set; }
        public long FinishTime { get; private set; }

        public SolutionProcessor(int id)
        {
            Id = id;
        }

        public void AddTaskScheduled(TaskToSchedule task)
        {
            var lastTask = TasksScheduled.OrderByDescending(_ => _.StartTime).FirstOrDefault();
            var firstPossibleTimeToAdd = lastTask != null ? lastTask.StartTime + lastTask.TimeToComplete : 0;
            var timeStartingTask = firstPossibleTimeToAdd > task.ReadyTime ? firstPossibleTimeToAdd : task.ReadyTime;
            var possiblePenalty = timeStartingTask + task.TimeToComplete - task.Deadline;

            var newTask = new TaskScheduled
            {
                Id = task.Id,
                StartTime = timeStartingTask,
                Penalty = possiblePenalty > 0 
                ? possiblePenalty
                : 0,
                TimeToComplete = task.TimeToComplete
            };

            TasksScheduled.Add(newTask);
            FinishTime = newTask.StartTime + newTask.TimeToComplete;
        }
    }
}