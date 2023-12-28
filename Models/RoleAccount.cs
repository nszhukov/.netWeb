using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Models
{
    [Table("RoleAccount")]
    public class RoleAccount
    { 
        public int RoleId { get; set; }
        public int AccountId { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public virtual Account Account { get; set; }
        [NotMapped]
        public virtual Role Role { get; set; }
    }
}