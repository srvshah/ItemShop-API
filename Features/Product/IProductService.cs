using ItemShop.Features.Product.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Product
{
    public interface IProductService
    {
        Task<int> Create(string name, string description, string imageUrl, decimal price); 
        Task<IEnumerable<ProductListingModel>> GetAllProducts();
        Task<ProductDetailModel> GetProductById(int id);
        Task<bool> UpdateProduct(int id, string name, string description, string imageUrl, decimal price);
        Task<bool> DeleteProduct(int id);
    }
}
