using ItemShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Transaction
{
    using Data.Models;
    using ItemShop.Features.Transaction.Models;
    using Microsoft.EntityFrameworkCore;

    public class TransactionService : ITransactionService
    {
        private readonly ItemShopDbContext _context;

        public TransactionService(ItemShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int custId, int prodId, int quantity, decimal rate)
        {
            var transaction = new Transaction
            {
                CustomerId = custId,
                ProductId = prodId,
                Quantity = quantity,
                Rate = rate,
                Total = quantity * rate
            };

            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
            return  transaction.Id;
        }

        public async Task<IEnumerable<TransactionListingModel>> GetAllTransactions()
        {
            return await _context.Transactions.Select(t => new TransactionListingModel
            {
                Id = t.Id,
                ProductName = t.Product.Name,
                CustomerName = t.Customer.Name,
                Quantity = t.Quantity,
                Rate = t.Rate,
                Total = t.Total,
                InvoiceId  = t.InvoiceId

            }).ToListAsync();
        }

        public async Task<TransactionListingModel> GetTransactionById(int id)
        {
            return await _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionListingModel
            {
                Id = t.Id,
                ProductName = t.Product.Name,
                CustomerName = t.Customer.Name,
                Quantity = t.Quantity,
                Rate = t.Rate,
                Total = t.Total,
                InvoiceId = t.InvoiceId

            }).FirstOrDefaultAsync();

        }

        public async Task<bool> DeleteTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null)
            {
                return false;
            }
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateTransaction(int id, int custId, int prodId, int quantity, decimal rate, int? invoiceId)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null)
            {
                return false;
            }

            transaction.CustomerId = custId;
            transaction.ProductId = prodId;
            transaction.Quantity = quantity;
            transaction.Rate = rate;
            transaction.Total = quantity * rate;
            if(invoiceId != null)
            {
                transaction.InvoiceId = invoiceId;
            }

            await _context.SaveChangesAsync();
            return true;
        }



    }
}
