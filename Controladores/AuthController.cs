using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeDisney.Entidades;
using ChallengeDisney.ViewModel.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ChallengeDisney.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace ChallengeDisney.Controladores
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _singInManager;
        private readonly IMailService _mailService;
        private IConfiguration _configuration;

        private string WriteToken(JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);

        public AuthController(UserManager<User> context, RoleManager<IdentityRole> context2, SignInManager<User> context3, IMailService context4, IConfiguration configuration)
        {
            _userManager = context;
            _roleManager = context2;
            _singInManager = context3;
            _mailService = context4;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AuthPostRegisterVM model)
        {
            var userExist = await _userManager.FindByNameAsync(model.UserName);

            if (userExist != null)
            {
                return BadRequest(new
                {
                    Status = "Error",
                    Message = $"The user {model.UserName} already exist, try anotherone"
                });
            }

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                PasswordHash = model.Password,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, model.Password); 
                    
            //manejo de errores
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Status = "Error",
                    Message = $"User creation failed, theres was an internal error. Errors: {string.Join(",",result.Errors.Select(x => x.Description))}"

                });
            }

            await _mailService.SendEmail(user);

            return Ok(new
            {
                Status = "Succes",
                Message = $"The user {model.UserName} is now created."

            });
        }
        [HttpPost]
        [Route("AdminRegister")]
        [AllowAnonymous]
        public async Task<IActionResult> RolesRegister(AuthPostRegisterVM model)
        {
            try
            {
                var userExist = await _userManager.FindByNameAsync(model.UserName);

                if (userExist != null)
                {
                    return BadRequest(new
                    {
                        Status = "Error",
                        Message = $"The user {model.UserName} already exist, try anotherone"
                    });
                }

                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    PasswordHash = model.Password,
                    IsActive = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                //manejo de errores
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        Status = "Error",
                        Message = $"User creation failed, theres was an internal error. Errors: {string.Join(",", result.Errors.Select(x => x.Description))}"

                    });
                }

                if (!await _roleManager.RoleExistsAsync("User")) await _roleManager.CreateAsync(new IdentityRole("User"));
                if (!await _roleManager.RoleExistsAsync("Admin")) await _roleManager.CreateAsync(new IdentityRole("Admin"));

                await _userManager.AddToRoleAsync(user, "Admin");

                await _mailService.SendEmail(user);

                return Ok(new
                {
                    Status = "Succes",
                    Message = $"The user {model.UserName} is now created."

                });
            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo: {e.Message}");
            }
        }
            

        //login
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestVM login) //TODO: arreglar
        {
            try
            {
                var result = await _singInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);

                if (result.Succeeded)
                {
                    var currentUser = await _userManager.FindByNameAsync(login.UserName);

                    if (currentUser.IsActive)
                    {
                        return Ok(await GetToken(currentUser));
                    }
                }

                return StatusCode(StatusCodes.Status401Unauthorized, new
                {
                    Status = "Error",
                    Message = $"El usuario: {login.UserName} no esta autorizado."
                });

            }catch (Exception e)
            {
                return StatusCode(500, $"Hubo un error de tipo: {e.Message}");
            }
        }

        private async Task<LoginResponseVM> GetToken(User currenUser)
        {
            var userRoles = await _userManager.GetRolesAsync(currenUser);

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, currenUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));

            var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));

            var token = new JwtSecurityToken(
                
                issuer: "https://locallhost:5001",
                audience: "https://locallhost:5001",
                expires : DateTime.Now.AddHours(2),
                claims: authClaims,
                signingCredentials: new SigningCredentials( authSigninKey, SecurityAlgorithms.HmacSha256)
                );

            return new LoginResponseVM
            {
                Token = WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
