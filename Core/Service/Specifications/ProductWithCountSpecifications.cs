using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithCountSpecifications :BaseSpecifications< Product,int >
    {
        public ProductWithCountSpecifications(ProductSpecificationsprameter Specprameter) 
            
            :base(


                  p =>
                 ( string.IsNullOrEmpty(Specprameter.Search) || p.Name.ToLower().Contains(Specprameter.Search.ToLower())) &&
                 (!Specprameter.BrandId.HasValue || p.BrandId == Specprameter.BrandId) &&
                 (!Specprameter.TypeId.HasValue || p.TypeId == Specprameter.TypeId)


                 )
        {
            
        }
    }
}
