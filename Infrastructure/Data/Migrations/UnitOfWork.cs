using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Migrations
{
    public class UnitOfWork : IUnitOfWork
    {   
        private readonly  StoreContext _dbConttext;

         private IGenericRepository<Product> _products;  

          private IGenericRepository<ProductBrand> _productBrand;  

        private IGenericRepository<ProductType> _productType;
        public UnitOfWork(StoreContext DbContext){
            _dbConttext=DbContext;
        }
   
        
        public IGenericRepository<Product> products => _products ??=new GenericRepository<Product>(_dbConttext);

        public IGenericRepository<ProductBrand> productBrand => _productBrand ??=new GenericRepository<ProductBrand>(_dbConttext);

        public IGenericRepository<ProductType> productType => _productType ??=new GenericRepository<ProductType>(_dbConttext);

        public void Dispose()
        {
            _dbConttext.Dispose();
            GC.SuppressFinalize(this);
        }

        

        public async Task Save()
        {
            await _dbConttext.SaveChangesAsync();
        }
    }
}