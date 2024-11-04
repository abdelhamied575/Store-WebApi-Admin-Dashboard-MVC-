﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core;
using StoreWeb.Core.Dtos.Orders;
using StoreWeb.Core.Entities.Order;
using StoreWeb.Core.Services.Contract;
using StoreWebApi.Errors;
using System.Security.Claims;

namespace StoreWebApi.Controllers
{
    
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IOrderService orderService,
                                IMapper mapper,
                                IUnitOfWork unitOfWork)
        {
            _orderService = orderService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }



        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto model)
        {

            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if(userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));

             var address = _mapper.Map<Address>(model.shipToAddress);


            var order = await _orderService.CreateOrderAsync(userEmail,model.BasketId,model.DeliveryMethodId, address);

            if(order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));



            return Ok(_mapper.Map<OrderToReturnDto>(order));


        }


        [Authorize]
        [HttpGet]
        public async Task <ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForSpecificUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));


            var orders = await  _orderService.GetOrdersForSpecificUserAsync(userEmail);

            if(orders is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders) );

        }


        [Authorize]
        [HttpGet("{orderId}")]
        public async Task <ActionResult<IEnumerable<OrderToReturnDto>>> GetOrdersForSpecificUser(int orderId)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));


            var order = await  _orderService.GetOrderByIdForSpecificUserAsync(userEmail, orderId);

            if(order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));

            return Ok(_mapper.Map<OrderToReturnDto>(order));

        }


        [HttpGet("DeliveryMethods")]
        public async Task <IActionResult> GetDeliveryMethods()
        {
            var deliveryMethods= await _unitOfWork.Repository<DeliveryMethod,int>().GetAllAsync();

            if(deliveryMethods is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
             
            return Ok(deliveryMethods);
        }




    }
}