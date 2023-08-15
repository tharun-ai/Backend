using API.helpers;
using AutoMapper;
using Core.Interfaces;

namespace API.Services
{
    public class PaginationService<T, R> : IPaginationServices<T, R> where T : class
    {  
        private readonly IMapper _mapper;

        public PaginationService(IMapper mapper){
            _mapper=mapper;
        }
        public  PaginationResult<T> GetPagination(List<R> source, PaginationInput input)
        {
           var currentPage=input.PageNumber;
           var totalNoOfRecords=source.Count;

           var pageSize=input.PageSize;

           var results=source.Skip(pageSize*(currentPage-1)).Take(pageSize).ToList();

           var items=_mapper.Map<List<T>>(results);

           var totalPages=(int) Math.Ceiling(totalNoOfRecords/(double)pageSize);

           PaginationResult<T> paginationResult=new PaginationResult<T>(currentPage,pageSize,totalNoOfRecords,totalPages,items);

           return paginationResult;

        }
        
    }
}