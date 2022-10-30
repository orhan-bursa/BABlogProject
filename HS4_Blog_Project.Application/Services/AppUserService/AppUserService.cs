using AutoMapper;
using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Domain.Entities;
using HS4_Blog_Project.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Application.Services.AppUserService
{
    public class AppUserService : IAppUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAppUserRepository _appUserRepository;
        public AppUserService(IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IAppUserRepository appUserRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _appUserRepository = appUserRepository;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            //Need mapping
            var user = _mapper.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<UpdateProfileDTO> GetByUserName(string userName)
        {
            var result = await _appUserRepository.GetFilteredFirstOrDefault(
                selector: x => new UpdateProfileDTO
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    Email = x.Email,
                    ImagePath = x.ImagePath
                },
                expression: x => x.UserName == userName);

            return result;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            //Update işlemlerinde önce id ile ilgili nesne ram'e çekilir. UI'dan gelen güncel nesne ile degisiklikler yapılır en son save changes ile veritabanına güncellemeyi göndeririz.
            var user = await _appUserRepository.GetDefault(x => x.Id == model.Id); //DB'deki userı çektik

            if (model.UploadPath != null) //Resim eklenmişse resmi modify edip kaydetmemiz lazım
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream()); //Upload path'den resmi oku
                image.Mutate(x => x.Resize(600, 560));
                Guid guid = Guid.NewGuid();

                image.Save($"wwwroot/images/{guid}.jpg"); //image istenen klasöre guid.jpg ismiyle kaydedildi
                user.ImagePath = $"/images/{guid}.jpg";

                await _appUserRepository.Update(user); //imagepath updated on DB
            }

            if (model.Password != null) //
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password); //Password UI'dan gelen ile DBden gelene eklendi
                await _userManager.UpdateAsync(user);
            }

            if (model.UserName != null)
            {
                var isUserNameExists = await _userManager.FindByNameAsync(model.UserName);

                if (isUserNameExists == null) //DB'de böyle bir username yok
                {
                    await _userManager.SetUserNameAsync(user, model.UserName); //yeni username DB usera eklendi
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }

            if (model.Email != null)
            {
                var isUserEmailExists = await _userManager.FindByNameAsync(model.UserName);

                if (isUserEmailExists == null) //DB'de böyle bir username yok
                {
                    await _userManager.SetUserNameAsync(user, model.Email); //yeni username DB usera eklendi
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
            }
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
