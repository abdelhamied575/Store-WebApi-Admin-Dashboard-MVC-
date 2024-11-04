using Microsoft.AspNetCore.Identity;
using StoreWeb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWeb.Repository.Identity.DataSeed
{
    public static class StoreIdentityDbContextSeed
    {



        public static async Task SeedAppUserAsync(UserManager<AppUser> _userManager)
        {
            if (_userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Email = "abdelhamiedb@gmail.com",
                    DisplayName = "Abdelhamied Belal",
                    UserName = "A.Belal",
                    PhoneNumber = "01026420147",
                    Address = new Address()
                    {
                        FirstName = "Abdelhamied",
                        LastName = "Belal",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "Eltyran"
                    }
                };


                await _userManager.CreateAsync(user, "P@ssW0rd");
            }

        }


    }
}
