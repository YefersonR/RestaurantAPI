using Core.Application.DTO.Account;
using Core.Application.DTOs.Account;
using Core.Application.Enums;
using Core.Application.Interfaces.Services;
using Core.Domain.Settings;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Services
{
    public class AccountService : IAccountService 
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _jwtSettings;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JWTSettings> jwtSettings)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<AuthenticationResponse> Authentication(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.UserName}";
                
                return response;
            }
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password,false,lockoutOnFailure:false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid credentials for {request.UserName}";

                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Account no confirmed for {request.UserName}";

                return response;
            }

            JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user); 

            response.Id = user.Id;
            response.Email= user.Email;
            response.UserName= user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false); 
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var refreshToken = GenerateRefreshToken();
            response.RefreshToken = refreshToken.Token;

            return response;
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
            };
        }

        private string RandomTokenString()
        {
            using(var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[40];
                rngCryptoServiceProvider.GetBytes(randomBytes);
                return BitConverter.ToString(randomBytes).Replace("-","");
            }

        }

        public async Task<RegisterResponse> RegisterClients(RegisterRequest request,string origin)
        {
            RegisterResponse response = new();

            response.HasError = false;
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if(userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"userName {request.UserName} is already taken";
                return response;
            }
            var userWithSameEmail= await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already register";
                return response;
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.FirstName,
                LastName =request.LastName,
                UserName = request.UserName,
            };
            user.EmailConfirmed = true;
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to register the user";
                return response;
            }
            await _userManager.AddToRoleAsync(user,Roles.mesero.ToString());
            return response;
        }
        public async Task<RegisterResponse> RegisterAdmin(RegisterRequest request, string origin)
        {
            RegisterResponse response = new();

            response.HasError = false;
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"userName {request.UserName} is already taken";
                return response;
            }
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"Email {request.Email} is already register";
                return response;
            }
            var user = new ApplicationUser()
            {
                Email = request.Email,
                Name = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
            };
            user.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error ocurred trying to register the user";
                return response;
            }
            await _userManager.AddToRoleAsync(user, Roles.administrador.ToString());

            return response;
        }
 
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new();

            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}"; 

                return response;
            }
            var verificationUri = await SendForgotPasswordUrl(user, origin);
            
            return response;
        }
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No account registered with {request.Email}";
                return response;
            }
            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(user, request.Token,request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An error occurred while resetting password ";
                return response;
            }

            return response;
        }
        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "No accounts registered with this user";
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Account confirmed for {user.Email}. You can now use the app";
            }
            return $"An error occurred while confirming {user.Email}.";
        }

        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            string route = "User/ResetPassword";
            var uri = new Uri(string.Concat($"{origin}/{route}"));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "token", code);

            return verificationUrl;
        }

       public async Task SignOut()
       {
            await _signInManager.SignOutAsync();
       }

       private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
       {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var rolesClaims = new List<Claim>();
            foreach(var role in roles)
            {
                rolesClaims.Add(new Claim("roles",role));
            } 
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim("uid",user.Id),

                }
                .Union(userClaims)
                .Union(rolesClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials:signingCredentials);

            return jwtSecurityToken;
       }


    }
}
