﻿using BlogTutorial2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogTutorial2.Controllers
{
    public class AuthController : Controller
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            //redirect only admin to panel
            if (!result.Succeeded)
            {
                return View(vm);

            }

            var user = await _userManager.FindByNameAsync(vm.UserName);

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if(isAdmin)
            {
                return RedirectToAction("Private", "Panel");
            }

            return RedirectToAction("Index", "Home");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var user = new IdentityUser
            {
                UserName = vm.UserName,
                //Email = vm.UserName
            };
            var result = await _userManager.CreateAsync(user, vm.Password);


            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");

            }

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Panel");
        }

    }
}
