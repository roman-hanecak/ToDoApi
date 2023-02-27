using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Visma.Bootcamp.ToDoApi.ApplicationCore.Entities.DTO
{
    public class TaskListDto
    {
        public string? Title { get; set; }

        public Guid PublicId { get; set; }
    }
}