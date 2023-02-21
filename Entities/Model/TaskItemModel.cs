using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Entities.Model
{
    public class TaskItemModel
    {
        [Required, StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public Boolean completed { get; set; }
    }
}