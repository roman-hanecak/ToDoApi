using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Model
{
    public class TaskListModel
    {
        public string? Title { get; set; }

        public TaskList ToDomain()
        {
            return new TaskList
            {
                Title = Title,
            };
        }
    }
}