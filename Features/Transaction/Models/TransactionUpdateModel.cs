using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction.Models
{
    public class TransactionUpdateModel: TransactionCreateModel
    {
        public int? InvoiceId { get; set; }
    }
}
