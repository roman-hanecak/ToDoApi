using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ToDoApi.Entities.DTO;

namespace ToDoApi.Entities.Domain
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid PublicId { get; set; }
        [Required, StringLength(100)]
        public string Title { get; set; }
        // one-to-many

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<TaskItem> TaskItems { get; set; }

        public TaskListDto ToDto()
        {
            return new TaskListDto
            {
                Title = Title,
                PublicId = PublicId
                //TasksList = TaskItems?.Select(x => x.ToDto()).ToList()
            };
        }
    }
}