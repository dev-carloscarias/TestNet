using Prueba_NET.Application.Interfaces;
using Prueba_NET.Domain.Entities;
using Prueba_NET.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_NET.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AplicationDbContext _context;

        public ProductRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
