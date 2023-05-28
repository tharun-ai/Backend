using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
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
        public Products(IProductRepository repo)
        {
            _repo=repo;        }

        [HttpGet]
        public  async Task<ActionResult<List<Product>>> GetProducts(){
            var products= await _repo.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public  async Task<ActionResult<Product>> GetProducts(int id){
            var products= await _repo.GetProductByIdAysnc(id);
            if(products==null){
                return null;
            }
            return products;
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductsBrands(){

            return  Ok(await _repo.GetProductBrandsAsync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes(){

            return  Ok(await _repo.GetProductTypesAsync());
        }
    }
}