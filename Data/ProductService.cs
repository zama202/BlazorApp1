using BlazorApp1.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Data
{
    public class ProductService
    {
        private readonly SqlDbContext _context;
        public ProductService(SqlDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            // Get Weather Forecasts  
            return await _context.TblProducts.AsNoTracking().ToListAsync();
        }


        public async Task<List<Product>> GetProductAsync(string product_code)
        {
            // Get Weather Forecasts  
            return await _context.TblProducts.Where(x => x.ProductCode == product_code).AsNoTracking().ToListAsync();
        }

        public Task<Product> CreateForecastAsync(Product objProduct)
        {
            _context.TblProducts.Add(objProduct);
            _context.SaveChanges();
            return Task.FromResult(objProduct);
        }

        public Task<bool> UpdateProductAsync(Product objProduct)
        {
            var ExistingProduct = _context.TblProducts
                .Where(x => x.ProductionId == objProduct.ProductionId)
                .FirstOrDefault();
            if (ExistingProduct != null)
            {
                ExistingProduct.ProductCode = objProduct.ProductCode;
                ExistingProduct.ProductDate = objProduct.ProductDate;
                ExistingProduct.ProductDescription = objProduct.ProductDescription;
                ExistingProduct.ProductQuantity = objProduct.ProductQuantity;
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<bool> DeleteProductAsync(Product objProduct)
        {
            var ExistingProduct =
                _context.TblProducts
                .Where(x => x.ProductionId == objProduct.ProductionId)
                .FirstOrDefault();
            if (ExistingProduct != null)
            {
                _context.TblProducts.Remove(ExistingProduct);
                _context.SaveChanges();
            }
            else
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }

    
}
