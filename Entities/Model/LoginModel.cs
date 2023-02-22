using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Entities.Model
{
    public class LoginModel
    {
        [Required, StringLength(50)]
        public string email { get; set; }

        [Required, StringLength(50)]
        public string password { get; set; }
    }
}