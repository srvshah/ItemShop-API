using ItemShop.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Invoice
{
    using Data.Models;
    using ItemShop.Features.Invoice.Models;
    using ItemShop.Features.Transaction.Models;
    using System.Globalization;

    public class InvoiceService: IInvoiceService
    {
        

        private readonly ItemShopDbContext _context;

        public InvoiceService(ItemShopDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Create(int custId)
        {
            
            var transactions = await _context.Transactions
                .Where(t => t.CustomerId == custId && t.InvoiceId == null)
                .ToListAsync();

            if(transactions.Count() == 0)
            {
                return null;
            }

            var invoice = new Invoice
            {
                InvoiceDate = DateTime.Now,
                InvoiceTotal = transactions.Sum(t => t.Total)
            };

            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();

            transactions.ForEach(t => t.InvoiceId = invoice.Id);

            await _context.SaveChangesAsync();
            return invoice.Id;

        }

        public async Task<IEnumerable<InvoiceListingModel>> GetAllInvoices()
        {
            return await _context.Invoices
                .Include(i => i.Transactions)
                    .ThenInclude(t => t.Customer)
                .Select(i => new InvoiceListingModel
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                InvoiceTotal = i.InvoiceTotal  ,
                CustomerId = i.Transactions.FirstOrDefault().Customer.Id,
                CustomerName = i.Transactions.FirstOrDefault().Customer.Name
            }).ToListAsync();
        }

        public async Task<InvoiceListingModel> GetInvoiceById(int id)
        {
            return await _context.Invoices.Where(i => i.Id == id)
                .Include(i => i.Transactions)
                    .ThenInclude(t => t.Customer)
                .Select(i => new InvoiceListingModel 
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                InvoiceTotal = i.InvoiceTotal ,
                CustomerId = i.Transactions.FirstOrDefault().Customer.Id,
                CustomerName = i.Transactions.FirstOrDefault().Customer.Name

                }).FirstOrDefaultAsync();
        }

       

        public async Task<bool> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if(invoice == null)
            {
                return false;
            }
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TransactionListingModel>> GetInvoiceTransactions(int id)
        {
            var invoice = await _context.Invoices
                .Include(i => i.Transactions)
                    .ThenInclude(t => t.Product)
                .Include(i => i.Transactions)
                    .ThenInclude(t => t.Customer)
                .Where(i => i.Id == id).FirstOrDefaultAsync();

            return invoice.Transactions.Select(t => new TransactionListingModel
            {
                Id = t.Id,
                CustomerName = t.Customer.Name,
                ProductName = t.Product.Name,
                InvoiceId = t.InvoiceId,
                Quantity = t.Quantity,
                Rate = t.Rate,
                Total = t.Total
            });
           

        }
    }
}
