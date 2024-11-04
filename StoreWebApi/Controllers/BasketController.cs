using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Entities.Basket;
using StoreWeb.Core.Repositories.Contract;
using StoreWeb.Core.Services.Contract;
using StoreWebApi.Errors;

namespace StoreWebApi.Controllers
{
  
    public class BasketController : BaseApiController
    {
        private readonly IBasketService _basketService;
        private readonly IMapper _mapper;

        public BasketController(IBasketService basketService, IMapper mapper)
        {
            _basketService = basketService;
            _mapper = mapper;
        }



        [HttpGet("{id}")]
        public async Task <ActionResult<CustomerBasket>> GetBasketById(string? id)
        {
            if (id is null) return BadRequest(new ApiErrorResponse(400, "Invalid Id"));


            var basket = await _basketService.GetBasketAsync(id);

            if (basket is null)  NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound)); 

            return Ok(basket);

        }



        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdateBasket(CustomerBasketDto? model)
        {
            if(model is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest)); 

            var basket = await _basketService.UpdateBasketAsync(model);

            if (basket is null) return BadRequest(new ApiErrorResponse(400));

            return Ok(basket);

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string? id)
        {
             if(id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

             var flag= await _basketService.DeleteBasketAsync(id);

            if(flag is false) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest)); 

            return NoContent();

        }



    }


}
