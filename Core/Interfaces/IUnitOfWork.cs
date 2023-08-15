using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    { 
      IGenericRepository<Product> products{get;}  

    IGenericRepository<ProductBrand> productBrand{get;}  

    IGenericRepository<ProductType> productType{get;}  

    Task Save();

    }
}