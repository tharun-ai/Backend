
using API.Dtos;
using API.Errors;
using API.helpers;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class Products:ControllerBase
    {   
       

        private readonly IUnitOfWork _unitOfWork;

        private readonly IPaginationServices<ProductToReturnDto,Product> _paginationService;
        public Products(IUnitOfWork unitofwork,IPaginationServices<ProductToReturnDto,Product> paginationService)
        {
           _unitOfWork=unitofwork;
           _paginationService=paginationService;
         }

        
        [HttpGet]
        public  async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParams productparams){

            var products=new List<Product>();
            if(productparams.Search!=null){
                switch(productparams.Sort){
                case "priceDesc":
                products= (List<Product>)await _unitOfWork.products.ListAllSync(p=>p.Name.ToLower().Contains(productparams.Search.ToLower()),orderBy: q => q.OrderByDescending(p => p.Price),includes: new List<string> { "ProductBrand", "ProductType" } );
                break;
                case "priceAsc":
                products= (List<Product>)await _unitOfWork.products.ListAllSync(p=>p.Name.ToLower().Contains(productparams.Search.ToLower()),orderBy: q => q.OrderBy(p => p.Price),includes: new List<string> { "ProductBrand", "ProductType" } );
                break;
                default:
                products= (List<Product>)await _unitOfWork.products.ListAllSync(p=>p.Name.ToLower().Contains(productparams.Search.ToLower()),orderBy: q => q.OrderBy(p => p.Name),includes: new List<string> { "ProductBrand", "ProductType" } );
                break;
              }
            }
            else{
               products= (List<Product>)await _unitOfWork.products.ListAllSync(orderBy: q => q.OrderByDescending(p => p.Name),includes: new List<string> { "ProductBrand", "ProductType" } );
            }
           
            var results=_paginationService.GetPagination(products.ToList(),new PaginationInput(productparams.pageIndex,productparams.PageSize));
            
            return Ok(results);

        }      

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorReponse),StatusCodes.Status404NotFound)]
        public  async Task<ActionResult<ProductToReturnDto>> GetProduct(int id){
            
           var product= await _unitOfWork.products.GetByIDAsync(p=>p.Id==id);
           return Ok(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<ProductBrand>> GetProductsBrands(){

            return  Ok(await _unitOfWork.productBrand.ListAllSync());
        }

         [HttpGet("types")]
        public async Task<ActionResult<ProductType>> GetProductTypes(){

            return  Ok(await _unitOfWork.productType.ListAllSync());
        }
    }
}