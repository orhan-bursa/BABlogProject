using HS4_Blog_Project.Application.Models.DTOs;
using HS4_Blog_Project.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_Blog_Project.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) // if already signed in
            {
                return RedirectToAction("Index", ""); //Areas
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // AppUserService.Create(registerDTO)
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(registerDTO);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "");
                }

                //error
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong";
                }
            }

            return View();

        }

        public IActionResult Login(string returnURL = "/")
        {
            if (User.Identity.IsAuthenticated) // if already signed in
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController)); //Areas
            }
            ViewData["ReturnURL"] = returnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = "/")
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded) //SignInResult.Succeeded
                {
                    return RedirectToLocal(returnUrl);
                    //return RedirectToAction("index", nameof(Areas.Member.Controllers.HomeController));
                }
                ModelState.AddModelError("", "Invalid Login Attempt");
            }
            return View(model);

        }
        private IActionResult RedirectToLocal(string returnURL)
        {
            if (Url.IsLocalUrl(returnURL))
            {
                return RedirectToAction(returnURL);
            }
            else
            {
                return RedirectToAction("index", nameof(Areas.Member.Controllers.HomeController));
            }
        }
        [Authorize]
        public async Task<IActionResult> Edit(string username) // Edite asılınca UI 'a existing info gönderdik
        {
            if (username != "")
            {

                UpdateProfileDTO user = await _appUserService.GetByUserName(username);

                return View(user);
            }
            else
                return RedirectToAction("index", nameof(Areas.Member.Controllers.HomeController));

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(UpdateProfileDTO model) // Kullanıcı bilgileri degistirince update basınca model viewden gelir, updated versiyonu servise gönderip ordan DBye eklemek gerekicek
        {
            if (ModelState.IsValid)
            {
                await _appUserService.UpdateUser(model);
                //Mesaj yazılcak
                return RedirectToAction("Index", "Home"); //dogru update olursa yönlendirilcek
            }
            else
            {
                TempData["Error"] = "Your profile has not been updated!";
                return View(model);
            }
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _appUserService.LogOut();

            return RedirectToAction("Index", "Home");
        }
    }
}
