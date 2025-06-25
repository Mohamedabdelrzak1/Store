using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
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
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var Repo   = _unitOfWork.GetRepository<ProductBrand, int>(); //Connected Repository
            var Brands = await Repo.GetAllAsync();
            var BrandsDto = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(Brands);
            return BrandsDto;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(); //Connected Repository
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var Types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(); //Connected Repository
            var TypeDto = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypeDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int Id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Id); //Connected Repository
            return _mapper.Map<Product, ProductDto>(Product);
        }
    }
}
