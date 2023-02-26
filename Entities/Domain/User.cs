using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ToDoApi.Entities.DTO;

namespace ToDoApi.Entities.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid? PublicId { get; set; }
        [Required, StringLength(50)]
        public string? FirstName { get; set; }
        [Required, StringLength(50)]
        public string? LastName { get; set; }
        [Required, StringLength(50)]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Image { get; set; }

        //foreign key

        public virtual ICollection<TaskList>? TaskLists { get; set; }

        public UserDto ToDto()
        {
            return new UserDto
            {
                PublicId = PublicId,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Image = Image
            };
        }
    }
}