using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Entities.Domain
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public Guid? PublicId { get; set; }

        [Required, StringLength(50)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime EndDate { get; set; }
        [Required]
        public Boolean Completed { get; set; }

        //foreign key
        //public int ItemListId { get; set; }
        //public virtual ItemList ItemList { get; set; }

        //public ItemDto toDto() { }
    }
}