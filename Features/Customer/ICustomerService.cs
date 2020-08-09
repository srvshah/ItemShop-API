using ItemShop.Data.Models;
using ItemShop.Features.Customer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Customer
{
    public interface ICustomerService
    {
        Task<int> Create(string name, string address, string phone, Gender gender);
        Task<IEnumerable<CustomerListingModel>> GetAllCustomers();
        Task<CustomerDetailModel> GetCustomerById(int id);
        Task<bool> UpdateCustomer(int id, string name, string phone, string address, Gender gender);
        Task<bool> DeleteCustomer(int id);
    }
}
