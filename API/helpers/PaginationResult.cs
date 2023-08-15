
namespace API.helpers
{
    public class PaginationResult<T> where T:class
    {
      

        public PaginationResult(int currentPage, int pageSize, int totalNoOfRecords, int totalPages, List<T> products) 
        {
            CurrentPage = currentPage;
         PageSize = pageSize;
         TotalNoOfRecords = totalNoOfRecords;
        TotalPages = totalPages;
        Products=products;
   
        }
                public int CurrentPage{get;set;}

        public int PageSize{get;set;}

        public int TotalNoOfRecords {get;set;}


        public int TotalPages { get; }
        public List<T> Products { get; }

        public bool HasPrevious=>CurrentPage>1;

        public bool HasNext =>CurrentPage<TotalPages;
    }
}