using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.Domain;

namespace ToDoApi.Entities.Model
{
    public class TaskListModel
    {

        //public Guid PublicId { get; set; }
        public string Title { get; set; }

        //public List<TaskItemModel> Tasks { get; set; }

        public TaskList ToDomain()
        {
            return new TaskList
            {
                Title = Title,
                //PublicId = PublicId,
                //TaskItems = Tasks.Select(x => x.ToDomain()).ToList()
            };
        }
    }


}