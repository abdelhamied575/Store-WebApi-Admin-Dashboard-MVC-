using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Repository.Data.Contexts;
using StoreWebApi.Errors;

namespace StoreWebApi.Controllers
{
    
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;

        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // Get : /api/Buggy/notfound
        public async Task<IActionResult> GetNotFoundRequestError()
        {
            var brand= await _context.Brands.FindAsync(100);

            if (brand is null) return NotFound(new ApiErrorResponse(404,"Brand With Id : 100 Is Not Found"));

            return Ok(brand);

        }

          [HttpGet("servererror")] // Get : /api/Buggy/servererror
        public async Task<IActionResult> GetServerRequestError()
        {
            var brand= await _context.Brands.FindAsync(100);

            var brandToString = brand.ToString(); // Will Throw Exception (Null Reference Exception )

            return Ok(brandToString);

        }


        [HttpGet("badrequest")] // Get : /api/Buggy/badrequest
        public async Task<IActionResult> GetBadRequestError()
        {
            return BadRequest(new ApiErrorResponse(400));

        }

         [HttpGet("badrequest/{id}")] // Get : /api/Buggy/badrequest/ahmed
        public async Task<IActionResult> GetBadRequestError(int id) // Validation Error
        {

            return Ok();

        }

         [HttpGet("unathorized")] // Get : /api/Buggy/unathorized
        public async Task<IActionResult> GetUnathorizedError() // Validation Error
        {
            return Unauthorized(new ApiErrorResponse(401));

        }






    }
}
