using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Helper;
using StoreWeb.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Services.Contract
{
    public interface IProductService 
    {

        Task<PaginationResponse<ProductDto>>GetAllProductAsync(ProductSpecParams productSpec );
        Task<IEnumerable<TypeBrandDto>>GetAllTypesAsync();
        Task<IEnumerable<TypeBrandDto>>GetAllBrandsAsync();

        Task<ProductDto> GetProductByIdAsync(int id);






    }
}
