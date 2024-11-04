using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Basket;
using StoreWeb.Core.Services.Contract;
using StoreWebApi.Errors;
using Stripe;

namespace StoreWebApi.Controllers
{
   
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("{basketId}")]
        [Authorize]
        public async Task<ActionResult<CustomerBasketDto>> CreatePaymentIntent(string basketId)
        {
            if (basketId is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var basket = await _paymentService.CreateOrUpdatePaymentIntentIdAsync(basketId);

            if(basket is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));    

            return Ok(basket);


        }



        const string endpointsecrt = "whsec_3c996710f65950a0e0871215c8e8b5e9b747c4018a3a2db2fdf68399a04213f5";

        [HttpPost("webhook")] // http://localhost:5108/api/Payment/webhook

        public async Task<IActionResult> Index()
        {
            var json=await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            try
            {

                var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], endpointsecrt);


                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;


                // Handle The Event
                if (stripeEvent.Type == "payment_intent.payment_failed")
                {
                    // Update DB
                    await _paymentService.UpdatePaymentIntentForSuccedOrFaild(paymentIntent.Id, false);

                }
                else if (stripeEvent.Type == "payment_intent.succeeded")
                {
                    // Update DB
                    await _paymentService.UpdatePaymentIntentForSuccedOrFaild(paymentIntent.Id, true);

                }
                else
                {
                    Console.WriteLine("Unhandled event type {0}",stripeEvent.Type);
                }

                return Ok();

            }
            catch (StripeException e)
            {

                return BadRequest();
            }




        }















    }
}
