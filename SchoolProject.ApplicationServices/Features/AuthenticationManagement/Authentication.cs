using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Core.Domain;
using SchoolProject.Core.Domain.Core.Dto.AuthDtos;
using SchoolProject.Core.Domain.Core.Entities;
using SchoolProject.Core.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;
using IdentityRole = Microsoft.AspNetCore.Identity.IdentityRole;
using MediatR;
using SchoolProject.ApplicationServices.Features.AuthenticationManagement.Event;

namespace SchoolProject.ApplicationServices.Features.AuthenticationManagement
{
    [ApiController]
    [Route("api/[controller]")]
    public class Authentication : ControllerBase
    {
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager;
        /* private readonly ILogger<AuthenticateController> _logger;*/
        private readonly IConfiguration _configuration;
        private readonly IPublisher _publish;

        public Authentication(Microsoft.AspNetCore.Identity.UserManager<User> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, IConfiguration configuration,
            IPublisher publish)
        {
            this._userManager = userManager;
            this.roleManager = roleManager;
            /*Ilogger<AuthenticateController> logger;*/
            _configuration = configuration;
            _publish = publish;
        }

        [HttpPost]
        [Route("loginUser")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaim = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaim.Add(new Claim(ClaimTypes.Role,userRole));
                }
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:Validissuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaim,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        //this endpoint is now working
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exists" });
            }
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            user.Id = Guid.NewGuid();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! please check your details and try again" });
            return Ok(new Response { Status = "success", Message = "User created successfully!" });
            await _publish.Publish(new UserRegistratedEvent()
            {
                To = "",
                ActivationDate = DateTime.Now,
                AdminName = "",
                AdminEmail = "",
                Firstname = ""
            });
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExist = await _userManager.FindByNameAsync(model.Username);
            if (userExist != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already Exists" });
            }
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            user.Id = Guid.NewGuid();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! please check your details and try again" });
            if (!await roleManager.RoleExistsAsync(UserRole.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRole.Admin));
            if (!await roleManager.RoleExistsAsync(UserRole.Staff))
                await roleManager.CreateAsync(new(UserRole.Staff));

            if (await roleManager.RoleExistsAsync(UserRole.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRole.Admin);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        /*[HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User does not exist" });

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                var errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.Description);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "" });
            }
            return Ok(new Response { Status = "Success", Message = "Password successfully changed" });
        }*/
    }
}
