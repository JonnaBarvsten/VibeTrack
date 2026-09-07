using Microsoft.AspNetCore.Identity;
using VibeTrack.Application.DTOs.Auth;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;

namespace VibeTrack.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public AuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if(user != null)
            {
                var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (isValidPassword)
                {
                    return new AuthResponseDto 
                    {
                        IsSuccess = true,
                        Message = "Inloggningen lyckades!"
                    };
                }

                return new AuthResponseDto 
                { 
                    IsSuccess = false,
                    Message = "Felaktig e-post eller lösenord."
                };
            }

            return new AuthResponseDto 
            {
                IsSuccess = false,
                Message = "Felaktig e-post eller lösenord."
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                var authResponseDto = new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Användaren skapades framgångsrikt!"
                };

                return authResponseDto;
            }

            var error = result.Errors.FirstOrDefault()?.Description ?? "Registrering Misslyckades";

            return new AuthResponseDto { IsSuccess = false, Message = error };
        }
    }
}
