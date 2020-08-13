using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Invoice.Models
{
    public class InvoiceListingModel
    {

        public int Id { get; set; }
     
        public DateTime InvoiceDate { get; set; }
       
        public decimal InvoiceTotal { get; set; }

      
    }
}
