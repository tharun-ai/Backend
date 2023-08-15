using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams ProductParams)
        :base(x=>
           (string.IsNullOrEmpty(ProductParams.Search) || x.Name.ToLower().Contains(ProductParams.Search)) &&
           (!ProductParams.BrandId.HasValue || x.ProductBrandId==ProductParams.BrandId) && (!ProductParams.TypeId.HasValue)  || (x.ProductTypeId==ProductParams.TypeId) 
        )
        {
            AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
            AddOrderBy(x=>x.Name);
            AddingPaging(ProductParams.PageSize*ProductParams.pageIndex-1,ProductParams.PageSize,true);

            if(!string.IsNullOrEmpty(ProductParams.Sort)){
               switch (ProductParams.Sort){
                    case "priceASC":
                    AddOrderBy(p=>p.Price);
                    break;
                   case "priceDesc":
                    AddOrderByDescedingOrder(p=>p.Price);
                    break;
                    default:
                    AddOrderBy(p=>p.Name);
                    break;

               }

            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id==id){
             AddInclude(x=>x.ProductType);
            AddInclude(x=>x.ProductBrand);
        }
    }
}