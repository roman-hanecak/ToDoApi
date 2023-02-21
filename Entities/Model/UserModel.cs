using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Entities.Model
{
    public class UserModel
    {
        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required]
        public byte[] Password { get; set; } = new byte[0];
    }
}