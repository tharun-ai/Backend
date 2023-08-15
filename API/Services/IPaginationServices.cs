using API.helpers;

namespace Core.Interfaces
{
    public interface IPaginationServices<T,R> where T:class
    {
        PaginationResult<T> GetPagination(List<R> source,PaginationInput input);
    }
}