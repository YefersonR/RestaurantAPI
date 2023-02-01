using Core.Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructura.Identity.Seeds
{
    public static class DefaultSuperAdmin
    {
        public static async Task Seeds(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser applicationAdmin = new()
            {
                Name = "SuperAdmin",
                LastName = "SuperAdministrador",
                UserName = "SuperAdmin",
                Email = "yefersonrubio@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "8096534321",
                PhoneNumberConfirmed = true,


            };

            if (userManager.Users.All(user => user.Id != applicationAdmin.Id))
            {
                var user = await userManager.FindByEmailAsync(applicationAdmin.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(applicationAdmin, "SuperAdmin123!");
                    await userManager.AddToRoleAsync(applicationAdmin, Roles.administrador.ToString());
                    await userManager.AddToRoleAsync(applicationAdmin, Roles.mesero.ToString());
                }
            }
        }
    }
}
