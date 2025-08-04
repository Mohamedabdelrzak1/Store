using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork ,IMapper _mapper) : IProductService
    {

        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync( ProductSpecificationsprameter Specprameter)
        {
            var spec = new ProductWithBrandsAndTypenSpecifications(Specprameter);
            // Get All Products Throught ProductRepository
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec); //Connected Repository


            var speccount = new ProductWithCountSpecifications(Specprameter);



            var count = await _unitOfWork.GetRepository<Product, int>().CountAsync(speccount);


            // Mapping IEnumerable<Product> To IEnumerablr<ProductDto> : Automapperl 
            
               var result =  _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);

            return new PaginationResponse<ProductDto>(Specprameter.pageIndex, Specprameter.pageSize ,count,result);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int Id)
        {
            var spec = new ProductWithBrandsAndTypenSpecifications(Id);
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec); //Connected Repository
            if (Product is null) throw new ProductNotFoundExceptions(Id);

            var result = _mapper.Map<Product, ProductDto>(Product);
            return result;
        }

        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(); //Connected Repositor
            var result = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return result;

        }

       
        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(); //Connected Repository
            var result = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return result;
        }

       
    }
}
