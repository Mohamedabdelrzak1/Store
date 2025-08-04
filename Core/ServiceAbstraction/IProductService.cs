using Shared;
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
        // Get All Products 
        Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecificationsprameter Specprameter);
        // Get Product By Id 
        Task<ProductDto?> GetProductByIdAsync(int Id);
        // Get All Types
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();
        // Get All Brands 
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();


    }
}
