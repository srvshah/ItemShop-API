using ItemShop.Features.Transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction
{
    public interface ITransactionService
    {
        Task<int> Create(int custId, int prodId, int quantity, decimal rate);
        Task<IEnumerable<TransactionListingModel>> GetAllTransactions();
        Task<TransactionListingModel> GetTransactionById(int id);
        Task<bool> DeleteTransaction(int id);
        Task<bool> UpdateTransaction(int id, int custId, int prodId, int quantity, decimal rate, int? invoiceId);
    }
}
