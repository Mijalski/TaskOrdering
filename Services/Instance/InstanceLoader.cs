﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskOrdering.Models.Instance;

namespace TaskOrdering.Services.Instance
{
    public class InstanceLoader
    {
        public async Task<Models.Instance.Instance> LoadInstanceAsync(string fileName)
        {
            var readPath = Path.Combine(Environment.CurrentDirectory, "App_Data", $"{fileName}.txt");
            
            var taskToScheduleList = new List<TaskToSchedule>();
            using (var file = new StreamReader(readPath, false))
            {
                string line;
                var counter = 0;
                while((line = await file.ReadLineAsync()) != null)
                {
                    var taskToScheduleParameters = line.Split(' ');

                    switch (taskToScheduleParameters.Length)
                    {
                        case 1:
                            continue;
                        case 3:
                        {
                            counter++;
                            var taskToSchedule = new TaskToSchedule
                            {
                                TimeToComplete = Convert.ToInt64(taskToScheduleParameters[0]),
                                ReadyTime = Convert.ToInt64(taskToScheduleParameters[1]),
                                Deadline = Convert.ToInt64(taskToScheduleParameters[2]),
                                Id = counter
                            };
                            taskToScheduleList.Add(taskToSchedule);
                            break;
                        }
                        default:
                            throw new InvalidOperationException("Invalid data format line: " + line);
                    }
                }

                return new Models.Instance.Instance
                {
                    TasksToSchedule = taskToScheduleList
                };
            }
        }
    }
}
