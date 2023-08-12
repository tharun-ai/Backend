using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class Products:ControllerBase
    {   
        private readonly IProductRepository _repo;

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;

        private readonly IGenericRepository<ProductType>  _productTypeRepo;
        public Products(IGenericRepository<Product> productsRepo,IGenericRepository<ProductBrand> productBrandRepo,IGenericRepository<ProductType>  productTypeRepo)
        {
            _productsRepo=productsRepo;
            _productBrandRepo=productBrandRepo;
            _productTypeRepo=productTypeRepo;
         }

        [HttpGet]
        public  async Task<ActionResult<List<ProductToReturnDto>>> GetProducts(){
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products= await _productsRepo.ListAsync(spec);

            return products.Select(product=>new ProductToReturnDto{
                  Id=product.Id,
             Name=product.Name,
             Description=product.Description,
             PictureUrl=product.PictureUrl,
             Price=product.Price,
             ProductBrand=product.ProductBrand.Name,
             ProductType=product.ProductType.Name
            }).ToList();
            //return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorReponse),StatusCodes.Status404NotFound)]
        public  async Task<ActionResult<ProductToReturnDto>> GetProduct(int id){
            //var products= await _productsRepo.GetByIDAsync(id);
            // if(products==null){
            //     return null;
            // }
            // return products;
            var spec=new ProductsWithTypesAndBrandsSpecification(id);
           var product= await _productsRepo.GetEntityWithSpec(spec);

           if(product==null){
            return NotFound(new ErrorReponse(404));
           }

           return new ProductToReturnDto{
             Id=product.Id,
             Name=product.Name,
             Description=product.Description,
             PictureUrl=product.PictureUrl,
             Price=product.Price,
             ProductBrand=product.ProductBrand.Name,
             ProductType=product.ProductType.Name
           };
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductsBrands(){

            return  Ok(await _productBrandRepo.ListAllSync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes(){

            return  Ok(await _productTypeRepo.ListAllSync());
        }
    }
}