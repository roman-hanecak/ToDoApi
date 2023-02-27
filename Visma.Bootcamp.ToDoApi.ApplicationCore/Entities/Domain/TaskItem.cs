using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.Domain
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid? PublicId { get; set; }

        [Required, StringLength(50)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }

        public string? CreatedDate { get; set; }

        public string? EndDate { get; set; }
        [Required]
        public Boolean Completed { get; set; }

        //foreign key
        [Required]
        public int TaskListId { get; set; }
        public virtual TaskList? TaskList { get; set; }

        public TaskItemDto ToDto()
        {
            return new TaskItemDto
            {
                PublicId = PublicId,
                Title = Title,
                Description = Description,
                CreatedDate = CreatedDate,
                EndDate = EndDate,
                Completed = Completed
            };
        }
    }
}