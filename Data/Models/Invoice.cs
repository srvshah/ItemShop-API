using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItemShop.Data.Models
{
    public class Invoice
    {
        public int Id { get; set; }

        [Required]   
        public DateTime InvoiceDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(32,2)")]
        public decimal InvoiceTotal { get; set; }

        public IEnumerable<Transaction> Transactions { get; }
    }
}