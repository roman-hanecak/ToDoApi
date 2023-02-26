using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using ToDoApi.Entities.Domain;

namespace ToDoApi.Entities.Model
{
    public class TaskItemModel
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public string CreatedDate { get; set; }

        public string EndDate { get; set; }

        [Required]
        public Boolean Completed { get; set; }

        public TaskItem ToDomain()
        {
            return new TaskItem
            {
                Title = Title,
                PublicId = Guid.NewGuid(),
                Description = Description,
                CreatedDate = CreatedDate,
                EndDate = EndDate,
                Completed = Completed
            };
        }
    }
}