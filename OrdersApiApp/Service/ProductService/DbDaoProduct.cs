﻿using Microsoft.EntityFrameworkCore;
using OrdersApiApp.Model;
using OrdersApiApp.Model.Entity;

namespace OrdersApiApp.Service.ProductService
{
    public class DbDaoProduct : IDaoTemplate<Product>
    {
        public ApplicationDbContext context;

        public DbDaoProduct(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<Product> Add(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> Delete(int id)
        {
            Product? product = await context.Products.FirstOrDefaultAsync(t => t.Id == id);
            if (product != null)
            {
                context.Remove(product);
                await context.SaveChangesAsync();
            }
            return product != null;
        }

        public async Task<List<Product>> GetAll()
        {
            await context.Products.LoadAsync();
            return await context.Products.ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await context.Products.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Product> Update(Product product)
        {
            context.Products.Update(product);
            await context.SaveChangesAsync();
            return product;
        }
    }
}
