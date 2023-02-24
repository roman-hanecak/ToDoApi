using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.Domain;

namespace ToDoApi.Entities.DTO
{
    public class UserDto
    {
        public Guid? PublicId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }

    }
}