using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Data.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        [Required]
        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(32,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(32,2)")]
        public decimal Total { get; set; }

        
        public int? InvoiceId { get; set; }

        public virtual Invoice Invoice { get; set; }

    }
}
