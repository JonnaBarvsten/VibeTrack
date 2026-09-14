using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VibeTrack.Application.DTOs.Auth;
using VibeTrack.Application.Interfaces;
using VibeTrack.Domain.Entities;

namespace VibeTrack.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userManager = userManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null)
            {
                var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);

                if (isValidPassword)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email!, user.UserName!, roles);

                    return new AuthResponseDto
                    {
                        IsSuccess = true,
                        Message = "Inloggningen lyckades!",
                        Token = token
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
            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);

            if (existingUser != null)
            {
                var foundUserDto = new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "E-postadressen är redan registrerad."
                };

                return foundUserDto;
            }

            var newUser = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            var result = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, "User");
                var roles = await _userManager.GetRolesAsync(newUser);
                var token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Email!, newUser.UserName!, roles);

                var authResponseDto = new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Användaren skapades framgångsrikt!",
                    Token = token
                };

                return authResponseDto;
            }

            var error = result.Errors.FirstOrDefault()?.Description ?? "Registrering Misslyckades";

            return new AuthResponseDto { IsSuccess = false, Message = error };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);


            if (user == null)
            {
                return false;
            }

            await _userManager.DeleteAsync(user);

            return true;
        }
    }
}
