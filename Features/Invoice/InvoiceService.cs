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

    public class InvoiceService: IInvoiceService
    {
        

        private readonly ItemShopDbContext _context;

        public InvoiceService(ItemShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(int custId)
        {
            var transactions = await _context.Transactions
                .Where(t => t.CustomerId == custId && t.InvoiceId == null)
                .ToListAsync();

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
            return await _context.Invoices.Select(i => new InvoiceListingModel
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                InvoiceTotal = i.InvoiceTotal
            }).ToListAsync();
        }

        public async Task<InvoiceListingModel> GetInvoiceById(int id)
        {
            return await _context.Invoices.Where(i => i.Id == id).Select(i => new InvoiceListingModel 
            {
                Id = i.Id,
                InvoiceDate = i.InvoiceDate,
                InvoiceTotal = i.InvoiceTotal
                
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
    }
}
