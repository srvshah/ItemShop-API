using ItemShop.Features.Invoice.Models;
using ItemShop.Features.Transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Invoice
{
    public interface IInvoiceService
    {
        Task<int?> Create(int custId);
        Task<IEnumerable<InvoiceListingModel>> GetAllInvoices();
        Task<InvoiceListingModel> GetInvoiceById(int id);
        Task<bool> DeleteInvoice(int id);
        Task<IEnumerable<TransactionListingModel>> GetInvoiceTransactions(int id);
    }
}
