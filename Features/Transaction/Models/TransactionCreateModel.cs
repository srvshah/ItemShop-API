using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction.Models
{
    public class TransactionCreateModel
    {

        [Required]
        public int CustomerId { get; set; }


        [Required]
        public int ProductId { get; set; }


        [Required]
        public int Quantity { get; set; }



    }
}
