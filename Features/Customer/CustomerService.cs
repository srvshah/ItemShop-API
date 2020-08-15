using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemShop.Data;

namespace ItemShop.Features.Customer
{
    using Data.Models;
    using ItemShop.Features.Customer.Models;
    using ItemShop.Features.Transaction.Models;
    using Microsoft.EntityFrameworkCore;

    public class CustomerService : ICustomerService
    {
        private readonly ItemShopDbContext _context;

        public CustomerService(ItemShopDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(string name, string address, string phone, Gender gender)
        {
            var customer = new Customer
            {
                Name = name,
                Address = address,
                Phone = phone,
                Gender = gender
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customer.Id;
        }

        public async Task<IEnumerable<CustomerListingModel>> GetAllCustomers()
        {
            return await _context.Customers.Select(c => new CustomerListingModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Gender = GetEnumValue(c.Gender)

            }).ToListAsync();

        }

        public async Task<CustomerDetailModel> GetCustomerById(int id)
        {
            return await _context.Customers.Where(c => id == c.Id).Select(c => new CustomerDetailModel
            {
                Id = c.Id,
                Name = c.Name,
                Phone = c.Phone,
                Address = c.Address,
                Gender = GetEnumValue(c.Gender)
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateCustomer(int id, string name, string phone, string address, Gender gender)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                return false;
            }
            customer.Name = name;
            customer.Address = address;
            customer.Phone = phone;
            customer.Gender = gender;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (customer == null)
            {
                return false;
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;

        }

        private static string GetEnumValue(Gender gender)
        {
            return ((int)gender).ToString();
        }

        public async Task<IEnumerable<TransactionListingModel>> GetCustomerTransactions(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Transactions)
                    .ThenInclude(t => t.Product)
                .Include(c => c.Transactions)
                    .ThenInclude(t => t.Customer)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            return customer.Transactions.Select(t => new TransactionListingModel
            {
                Id = t.Id,
                CustomerName = t.Customer.Name,
                ProductName = t.Product.Name,
                InvoiceId = t.InvoiceId,
                Quantity = t.Quantity,
                Rate = t.Rate,
                Total = t.Total,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            });
        }
    }
}
