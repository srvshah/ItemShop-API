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

        public async Task<int> Create(int custId, int prodId, int quantity)
        {
            var product = await _context.Products.FindAsync(prodId);
            var transaction = new Transaction
            {
                CustomerId = custId,
                ProductId = prodId,
                Quantity = quantity,
                Rate = product.Price,
                Total = quantity * product.Price
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

        public async Task<TransactionDetailModel> GetTransactionById(int id)
        {
            return await _context.Transactions.Where(t => t.Id == id).Select(t => new TransactionDetailModel
            {
                Id = t.Id,
                ProductName = t.Product.Name,
                CustomerName = t.Customer.Name,
                Quantity = t.Quantity,
                Rate = t.Rate,
                Total = t.Total,
                InvoiceId = t.InvoiceId  ,
                CustomerId = t.CustomerId,
                ProductId = t.ProductId

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

        public async Task<bool> UpdateTransaction(int id, int custId, int prodId, int quantity)
        {
            var product = await _context.Products.FindAsync(prodId);
            if (product == null)
            {
                return false;
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if(transaction == null)
            {
                return false;
            }

            transaction.CustomerId = custId;
            transaction.ProductId = prodId;
            transaction.Quantity = quantity;
            transaction.Total = quantity * product.Price;

            await _context.SaveChangesAsync();
            return true;
        }



    }
}
