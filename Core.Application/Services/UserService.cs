using AutoMapper;
using Core.Application.DTOs.Account;
using Core.Application.Interfaces.Services;
using Core.Application.ViewModels.User;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        
        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        
        public async Task<AuthenticationResponse> Login(LoginViewModel loginViewModel)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginViewModel);
            AuthenticationResponse response = await _accountService.Authentication(loginRequest);    
            
            return response;
        }
        public async Task<RegisterResponse> Register(UserSaveViewModel userSaveViewModel,string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(userSaveViewModel);
            return await _accountService.RegisterClients(registerRequest,origin);
        }
        public async Task<string> ConfirmEmail(string userId,string token)
        {
            return await _accountService.ConfirmAccount(userId, token);
        }
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel,string origin)
        {
            ForgotPasswordRequest PasswordRequest = _mapper.Map<ForgotPasswordRequest>(forgotPasswordViewModel);
            return await _accountService.ForgotPassword(PasswordRequest,origin);
        }
        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            ResetPasswordRequest PasswordRequest = _mapper.Map<ResetPasswordRequest>(resetPasswordViewModel);
            return await _accountService.ResetPassword(PasswordRequest);
        }
        public async Task SingOut()
        {
            await _accountService.SignOut();
        }
    }
}
