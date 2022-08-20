using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
using UrunKatalogProjesi.Data.Configurations;
using UrunKatalogProjesi.Data.Entities;
using UrunKatalogProjesi.Data.Helper;
using UrunKatalogProjesi.Data.Models;
using UrunKatalogProjesi.Data.UnitofWork;
using UrunKatalogProjesi.Data.Dto;
using UrunKatalogProjesi.Data.Repositories;
using Microsoft.AspNetCore.Http;

namespace UrunKatalogProjesi.Service.Services
{
    public class AccountService : BaseService<AppUserDto,AppUser>, IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly SystemOptionConfig userOptionsConfig;
        private readonly IUnitofWork _unitofWork;
        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper, IUnitofWork unitofWork, IAccountRepository accountRepository, IOptions<SystemOptionConfig> options, IHttpContextAccessor httpContextAccessor) : base(accountRepository,unitofWork,mapper,httpContextAccessor)
        {
            _accountRepository = accountRepository;
            this.userManager = userManager;
            userOptionsConfig = options.Value;
            _unitofWork = unitofWork;
            _signInManager = signInManager;
        }

        public async Task<ResponseEntity> LoginProcess(LoginRequest loginRequest)
        {
            var appUser = await userManager.FindByNameAsync(loginRequest.UserName);
            if (appUser == null)
                return new ResponseEntity("You have entered an invalid username or password.");
            if(appUser.UserStatus == UserStatuses.Block)
            {
                return new ResponseEntity("This account has been blocked.");
            }
            var loginResult = await _signInManager.PasswordSignInAsync(loginRequest.UserName, loginRequest.Password, false, false);
            if(loginResult.Succeeded)
            {
                await userManager.ResetAccessFailedCountAsync(appUser);
                return new ResponseEntity(appUser);
            }
            var result = await userManager.AccessFailedAsync(appUser);
            if(!result.Succeeded)
            {
                return new ResponseEntity("Access Failed Error. Error: "+ result.Errors.ErrorToString());
            }
            int failCount = await userManager.GetAccessFailedCountAsync(appUser);
            if(failCount == userOptionsConfig.BlockAccessFailedCount)
            {
                appUser.UserStatus = UserStatuses.Block;
                try
                {
                    _unitofWork.CommitAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Login Failed Process Commit Fail. Error :" + ex.Message);
                }
                BackgroundJob.Jobs.FireAndForgetJobs.EmailSendJob(EmailTypes.Block, appUser);
                return new ResponseEntity("The account status is block.");
            }
            else
            {
                return new ResponseEntity($"You have entered an invalid username or password. You have {userOptionsConfig.BlockAccessFailedCount - failCount} last entries left for successful login.");
            }
        }
    }
}
