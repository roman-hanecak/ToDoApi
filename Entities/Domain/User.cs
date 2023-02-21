using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Entities.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid? PublicId { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [Required, StringLength(50)]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; } = new byte[0];
        [Required]
        public byte[] PasswordSalt { get; set; } = new byte[0];

        public byte[] PasswordHash { get; set; } = new byte[0];

        public virtual ICollection<TaskList> TaskLists { get; set; }
    }
}