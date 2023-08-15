
namespace API.helpers
{
    public class PaginationInput
    {
        private int pageIndex;

        public PaginationInput(int pageIndex, int pageSize)
        {
            PageNumber = pageIndex;
            PageSize = pageSize;
        }

        public int PageNumber{get;set;}

        public int PageSize {get;set;}
    }
}