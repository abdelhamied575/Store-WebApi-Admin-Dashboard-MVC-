using AdminDashboard.Models;
using AdminDashboard.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core;
using StoreWeb.Core.Dtos.Products;
using StoreWeb.Core.Entities;
using StoreWeb.Services.Helper;

namespace AdminDashboard.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDocumentService _documentService;

        public ProductController(IUnitOfWork unitOfWork,IMapper mapper,IDocumentService documentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _documentService = documentService;
        }


        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Repository<Product, int>().GetAllAsync();

            var mappedProducts=_mapper.Map<IReadOnlyList<ProductDto>>(products);


            return View( mappedProducts);
        }


        [HttpGet]
		public async Task<IActionResult> Create(int id)
        {
            var product = await _unitOfWork.Repository<Product, int>().GetAsync(id);

            var mappedProduct = _mapper.Map<ProductFormViewModel>(product);

            return View(mappedProduct);

        }

        
        [HttpPost]
		public async Task<IActionResult> Create(ProductFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Picture is not null)
                {
                    model.PictureUrl = await _documentService.UploadFileAsync(model.Picture, "products");
                }

                var mappedProduct = _mapper.Map<Product>(model);

                await _unitOfWork.Repository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction("Index");


            }

            return View(model);



        }






	}
}
