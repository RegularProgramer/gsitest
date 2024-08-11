using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

namespace FrontendBlazor.Controllers
{
    public class RegistrationController : Controller
    {
        private RegistrationHelper registrationHelper;
        private AspNetUserHelper aspNetUserHelper;
        private AspNetUserRoleHelper aspNetUserRoleHelper;

        public RegistrationController()
        {
            registrationHelper = new RegistrationHelper();
            aspNetUserHelper = new AspNetUserHelper();
            aspNetUserRoleHelper = new AspNetUserRoleHelper();
        }
        public ActionResult SignUp()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        // POST: RegistrationController/SetSessionData
        [HttpPost]
        public ActionResult SetSessionData(string Token, string UserId)
        {
            var token = Token;
            var userId = UserId;
            AspNetUserViewModel user = aspNetUserHelper.Get(userId);
            AspNetUserRoleViewModel userRole = aspNetUserRoleHelper.Get(userId);

            HttpContext.Session.SetString("UserId", user?.Id!);
            HttpContext.Session.SetString("Email", user?.Email!);
            HttpContext.Session.SetString("UserName", user?.UserName!);
            HttpContext.Session.SetString("NormalizedUserName", user?.NormalizedUserName!);
            HttpContext.Session.SetString("Identification", user?.Identification!);
            HttpContext.Session.SetString("Token", token);
            HttpContext.Session.SetString("RoleId", userRole?.RoleId!);
            HttpContext.Session.SetString("Login", "true");
            return Ok();
        }

        //// POST: RegistrationController/Register
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Register(RegistrationViewModel model, string passwordConfirmation)
        //{
        //    try { 
        //    // Validate passwordConfirmation
        //    if (model.PasswordHash != passwordConfirmation)
        //    {
        //        ModelState.AddModelError("", "Las contraseñas no coinciden.");
        //        return RedirectToAction("SignUp", "Registration");
        //    }

        //    // Process registration
        //    // ...
        //        if (registrationHelper.Register(model))
        //        {
        //            return RedirectToAction("Login", "Registration");
        //        }
        //        else
        //        {
        //            return RedirectToAction("SignUp", "Registration");
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        // Print or log the error message
        //        Console.WriteLine($"Error: {ex.Message}");

        //        // Handle the custom exception
        //        TempData["ErrorMessage"] = ex.Message;
        //        return RedirectToAction("SignUp", "Registration");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return RedirectToAction("SignUp", "Registration");
        //    }
        //}

        //// POST: RegistrationController/Login
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Login(LoginViewModel model)
        //{
        //    try
        //    {
        //        if (registrationHelper.Login(model))
        //        {
        //            return RedirectToAction("Dashboard", "Home");
        //        }
        //        else
        //        {
        //            return RedirectToAction("Login", "Registration");
        //        }

        //    }

        //    catch (InvalidOperationException ex)
        //    {
        //        // Print or log the error message
        //        Console.WriteLine($"Error: {ex.Message}");

        //        // Handle the custom exception
        //        TempData["ErrorMessage"] = ex.Message; // Store the exception message in TempData
        //        return RedirectToAction("Login", "Registration");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle other exceptions
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return RedirectToAction("Login", "Registration");
        //    }
        //}
    }
}
