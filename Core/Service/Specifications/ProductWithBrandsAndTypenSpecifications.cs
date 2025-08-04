using Domain.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class ProductWithBrandsAndTypenSpecifications : BaseSpecifications<Product,int>
    {
        public ProductWithBrandsAndTypenSpecifications(int id ) :base(p => p.Id == id)
        {
            ApplyIncludes();
        }

        public ProductWithBrandsAndTypenSpecifications(ProductSpecificationsprameter Specprameter) 
           : base(

                  p =>
                 ( string.IsNullOrEmpty( Specprameter.Search) || p.Name.ToLower().Contains(Specprameter.Search.ToLower()))&&
                 (!Specprameter.BrandId.HasValue || p.BrandId == Specprameter.BrandId) &&
                 (!Specprameter.TypeId.HasValue || p.TypeId == Specprameter.TypeId)

                )
        {
            ApplyIncludes();
            ApplySort(Specprameter.Sort);

            ApplayPagination(Specprameter.pageIndex, Specprameter.pageSize);

        }

        private void ApplyIncludes()
        {
            AddInclude(p => p.productBrand);
            AddInclude(p => p.productType);
        }

        private void ApplySort(string? sort)
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "namedesc":
                        AddOrderByDescending(P => P.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

        }
    }
}
