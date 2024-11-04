using AutoMapper;
using StoreWeb.Core.Dtos.Auth;
using StoreWeb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Core.Mapping.Auth
{
    public class AuthProfile:Profile
    {

        public AuthProfile()
        {
            CreateMap<Address,AddressDto>().ReverseMap();
        }

    }
}
