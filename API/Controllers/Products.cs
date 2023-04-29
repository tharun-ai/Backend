using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class Products:ControllerBase
    {   
        private readonly StoreContext _storecontext;
        public Products(StoreContext storecontext)
        {
            _storecontext=storecontext;
        }

        [HttpGet]
        public  async Task<ActionResult<List<Product>>> GetProducts(){
            var products= await _storecontext.Products.ToListAsync();
            return products;
        }

        [HttpGet("{id}")]
        public  async Task<ActionResult<Product>> GetProducts(int id){
            var products= await _storecontext.Products.FindAsync(id);
            if(products==null){
                return null;
            }
            return products;
        }
    }
}