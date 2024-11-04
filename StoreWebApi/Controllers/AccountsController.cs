using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StoreWeb.Core.Dtos.Auth;
using StoreWeb.Core.Entities.Identity;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Services.Services.Tokens;
using StoreWebApi.Errors;
using StoreWebApi.Extensions;
using System.Security.Claims;

namespace StoreWebApi.Controllers
{
    
    public class AccountsController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(IUserService userService,
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task <ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);

            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized, "Invalid Login"));

            return Ok(user);

             
        }

        [HttpPost("register")]
        public async Task <ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);

            if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Invalid Registration")); 

            return Ok(user);
             
        } 



        [HttpGet("GetCurrentUser")]
        [Authorize]
        public async Task <ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user= await _userManager.FindByEmailAsync(userEmail);

            if (user is null) return BadRequest(new ApiErrorResponse( StatusCodes.Status400BadRequest));
            
            return Ok(new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)

            });

             
        }

        
        [HttpGet("Address")]
        [Authorize]
        public async Task <ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            

            var user= await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null) return BadRequest(new ApiErrorResponse( StatusCodes.Status400BadRequest));

            var mappedAddress =  _mapper.Map<AddressDto>(user.Address);

            return Ok(mappedAddress);

             
        }
        [HttpPut("Address")]
        [Authorize]
        public async Task <ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address)
        {
            

            var user= await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null) return BadRequest(new ApiErrorResponse( StatusCodes.Status400BadRequest));

            var mappedAddress = _mapper.Map<AddressDto, Address>(address);

            mappedAddress.Id = user.Address.Id;

            user.Address = mappedAddress;
            
            var result=await _userManager.UpdateAsync(user);    
            if(!result.Succeeded) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(address);


        }







    }
}  
