using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public int? ParrentId { get; set; }

        [NotMapped]
        public virtual Category Parent { get; set; }
        [NotMapped]
        public virtual ICollection<Category> InverseParent { get; set; }
        [NotMapped]
        public virtual ICollection<Product> Products { get; set; }
    }
}
