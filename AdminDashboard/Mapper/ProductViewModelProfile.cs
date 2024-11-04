using AdminDashboard.Models;
using AutoMapper;
using StoreWeb.Core.Entities;

namespace AdminDashboard.Mapper
{
	public class ProductViewModelProfile:Profile
	{

        public ProductViewModelProfile()
        {
            CreateMap<Product, ProductFormViewModel>().ReverseMap();
        }

    }
}
