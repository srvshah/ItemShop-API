using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction.Models
{
    public class TransactionDetailModel : TransactionListingModel
    {
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
