﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(32,2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public IEnumerable<Transaction> Transactions { get; }
    }
}
