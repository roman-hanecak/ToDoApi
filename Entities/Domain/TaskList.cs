using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ToDoApi.Entities.Domain
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid? PublicId { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        // one-to-many
        public virtual ICollection<TaskItem> TaskItems { get; set; }
    }
}