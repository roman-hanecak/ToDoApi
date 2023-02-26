using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;

namespace ToDoApi.Entities.DTO
{
    public class TaskItemDto
    {
        public Guid PublicId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public string EndDate { get; set; }
        public Boolean Completed { get; set; }
    }
}