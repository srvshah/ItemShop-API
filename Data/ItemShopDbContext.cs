using System;
using System.Collections.Generic;
using System.Text;
using ItemShop.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItemShop.Data
{
    public class ItemShopDbContext : IdentityDbContext<User>
    {
        public ItemShopDbContext(DbContextOptions<ItemShopDbContext> options)
            : base(options)
        {
        }
    }
}
