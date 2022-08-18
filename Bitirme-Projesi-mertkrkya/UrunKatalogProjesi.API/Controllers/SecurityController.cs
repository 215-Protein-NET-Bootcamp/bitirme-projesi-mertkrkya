using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrunKatalogProjesi.Core.Entities;
using UrunKatalogProjesi.Core.Models;
using UrunKatalogProjesi.Data.Dto;
using UrunKatalogProjesi.Service.Services;

namespace UrunKatalogProjesi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAuthenticationService _authenticationService;
        public SecurityController(IAuthenticationService authenticationService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _authenticationService = authenticationService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);
                if (!loginResult.Succeeded)
                {
                    return BadRequest();
                }
                var user = await _userManager.FindByNameAsync(loginRequest.UserName);
                var result = await _authenticationService.CreateTokenAsync(user);
                if (!result.isSuccess)
                    return BadRequest(result);
                return Ok(result);
            }
            return BadRequest(ModelState);
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterDto input)
        {
            if (ModelState.IsValid)
            {
                var newUser = new AppUser
                {
                    UserName = input.UserName,
                    Email = input.Email,
                    EmailConfirmed = true,
                    TwoFactorEnabled = false
                };

                var registerUser = await _userManager.CreateAsync(newUser, input.Password);
                if (registerUser.Succeeded)
                {
                    await _signInManager.SignInAsync(newUser, isPersistent: false);
                    var user = await _userManager.FindByNameAsync(newUser.UserName);
                    var result = await _authenticationService.CreateTokenAsync(user);
                    if (!result.isSuccess)
                        return BadRequest(result);
                    return Ok(result);
                }
                return BadRequest(new ResponseEntity(registerUser.Errors)); //Düzenle
            }
            return BadRequest(ModelState);

        }
    }
}
