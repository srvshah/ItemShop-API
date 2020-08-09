using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemShop.Features.Product
{
    using Data.Models;
    using ItemShop.Data;
    using ItemShop.Features.Product.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProductService : IProductService
    {
        private readonly ItemShopDbContext _context;

        public ProductService(ItemShopDbContext context)
        {
            _context = context;
        }


        public async Task<int> Create(string name, string description, string imageUrl, decimal price)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price
               
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;

        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductListingModel>> GetAllProducts()
        {
            return await _context.Products.Select(p => new ProductListingModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price
            }).ToListAsync();
        }

        public async Task<ProductDetailModel> GetProductById(int id)
        {
            return await _context.Products.Where(p => id == p.Id).Select(p => new ProductDetailModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                Description = p.Description
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateProduct(int id, string name, string description, string imageUrl, decimal price)
        {
            var product = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if(product == null)
            {
                return false;
            }
            product.Name = name;
            product.Description = description;
            product.ImageUrl = imageUrl;
            product.Price = price;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
