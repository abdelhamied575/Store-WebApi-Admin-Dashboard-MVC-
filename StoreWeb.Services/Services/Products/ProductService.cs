using AutoMapper;
using StoreWeb.Core;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Entities;
using StoreWeb.Core.Helper;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core.Specifications;
using StoreWeb.Core.Specifications.Products;
using StoreWeb.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Services.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<ProductDto>> GetAllProductAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);

            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

            var countSpec = new ProductWithCountSpecifications(productSpec);

            var count= await _unitOfWork.Repository<Product,int>().CountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, mappedProducts ) ;
        }   
        

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);

            var product =  await _unitOfWork.Repository<Product,int>().GetWithSpecAsync(spec);
            var MappedProduct=_mapper.Map<ProductDto>(product);
            return MappedProduct;
        }

        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        {
            return _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        }


        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        {
           return _mapper.Map<IEnumerable<TypeBrandDto>>(await   _unitOfWork.Repository<ProductType, int>().GetAllAsync());
        }

        
    }
}
