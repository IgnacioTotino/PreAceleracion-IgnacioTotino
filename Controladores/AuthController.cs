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


namespace ChallengeDisney.Controladores
{   
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        public AuthController(UserManager<User> context, IMailService context2)
        {
            _userManager = context;
            _mailService = context2;
        }

        [HttpPost]
        [Route("Registro")]
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

            var result = await _userManager.CreateAsync(user, model.Password); //culo probando si enverdad funciona
            var chocolate = "rico :) ";
            
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

    }
}
