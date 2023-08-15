using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public class ProductSpecParams
    {
        private const int MaxPageSize=50;
        public  int pageIndex {get;set;}

        public int PageSize {get;set;}

        public int? BrandId {get;set;}

        public int? TypeId{get;set;}

        public string Sort{get;set;}

        private string _search{get;set;}

        public string Search{
              get=>_search;
              set=> _search=value.ToLower();

        }

    }
}