using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction.Models
{
    public class TransactionListingModel
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }
       
        public int Quantity { get; set; }
     
        public decimal Rate { get; set; }

        public decimal Total { get; set; }

        public int? InvoiceId { get; set; }
        
    }
}
