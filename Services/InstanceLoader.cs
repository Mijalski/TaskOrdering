using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TaskOrdering.Models;

namespace TaskOrdering.Services
{
    public class InstanceLoader
    {
        public async Task<Instance> LoadSolution(string fileName)
        {
            var readPath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"{fileName}.txt");
            
            var taskToScheduleList = new List<TaskToSchedule>();
            using (var file = new StreamReader(readPath, false))
            {
                string line;
                while((line = await file.ReadLineAsync()) != null)
                {
                    var taskToScheduleParameters = line.Split(' ');

                    switch (taskToScheduleParameters.Length)
                    {
                        case 0:
                            continue;
                        case 3:
                        {
                            var taskToSchedule = new TaskToSchedule
                            {
                                TimeToComplete = Convert.ToInt64(taskToScheduleParameters[0]),
                                ReadyTime = Convert.ToInt64(taskToScheduleParameters[1]),
                                Deadline = Convert.ToInt64(taskToScheduleParameters[2])
                            };
                            taskToScheduleList.Add(taskToSchedule);
                            break;
                        }
                        default:
                            throw new InvalidOperationException("Invalid data format line: " + line);
                    }
                }

                return new Instance
                {
                    TasksToSchedule = taskToScheduleList
                };
            }
        }
    }
}
