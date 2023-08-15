using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.helpers
{
    public class MappingProfile :Profile
    { 
        public MappingProfile(){
              CreateMap<Product,ProductToReturnDto>().ForMember(d=>d.ProductBrand,o=>o.MapFrom(s=>s.ProductBrand.Name))
              .ForMember(d=>d.ProductType,o=>o.MapFrom(s=>s.ProductType.Name));
        }
      
    }
}