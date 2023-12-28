using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("InvoiceDetails")]
    public class InvoiceDetails
    {
        public int InvoiceId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [NotMapped]
        public virtual Invoice Invoice { get; set; }
        [NotMapped]
        public virtual Product Product { get; set; }
    }
}
