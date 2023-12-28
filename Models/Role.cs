using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            RoleAccounts = new HashSet<RoleAccount>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public virtual ICollection<RoleAccount> RoleAccounts { get; set; }
    }
}
