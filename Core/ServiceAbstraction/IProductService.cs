using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IProductService
    {
        // GetAll Products 
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        // get Product By Id 
        Task<ProductDto> GetProductByIdAsync(int Id);
        // get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        // get All Brands 
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();


    }
}
