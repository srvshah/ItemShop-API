﻿using ItemShop.Data.Models;
using ItemShop.Features.Transaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction
{
    public interface ITransactionService
    {
        Task<int> Create(int custId, int prodId, int quantity);
        Task<IEnumerable<TransactionListingModel>> GetAllTransactions();
        Task<TransactionDetailModel> GetTransactionById(int id);
        Task<bool> DeleteTransaction(int id);
        Task<bool> UpdateTransaction(int id, int custId, int prodId, int quantity, string transactionStatus);
        
    }
}
