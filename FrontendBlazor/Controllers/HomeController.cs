
using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Cryptography;
using NuGet.Protocol;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace FrontendBlazor.Controllers
{
    public class HomeController : Controller
    {

        private RegistrationHelper registrationHelper;

        private readonly ILogger<HomeController> _logger;

        private readonly UserHelper userHelper;

        private readonly AspNetUserHelper aspNetUserHelper;

        private readonly PolicyStateHelper policyStateHelper;

        private readonly PolicyTypeHelper policyTypeHelper;
        
        private readonly PolicyHelper policyHelper;

        private readonly AspNetUserRoleHelper aspNetUserRoleHelper;

        private readonly IEnumerable<DniViewModel> listDni; 
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            registrationHelper = new RegistrationHelper();
            userHelper = new UserHelper();
            aspNetUserHelper = new AspNetUserHelper();
            policyTypeHelper = new PolicyTypeHelper();
            policyStateHelper = new PolicyStateHelper();
            policyHelper = new PolicyHelper();
            aspNetUserRoleHelper = new AspNetUserRoleHelper();
            listDni = policyHelper.GetAllDni();

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Index()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            HttpContext.Session.Clear();
            return View();
        }

        [SecurityModel]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Login()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            HttpContext.Session.Clear();
            return View();
        }

        [SecurityModel]
        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous, HttpGet("ForgotPassword")]
        public ActionResult ForgotPassword()
        {
            // ForgotPasswordModel model = new ForgotPasswordModel();
            return View();
        }

        [HttpPost("ForgotPassword"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            List<AspNetUserViewModel> users = aspNetUserHelper.GetAll();

            if (ModelState.IsValid)
            {
                ModelState.Clear(); 
                if (model.Email != null)
                {
                    try
                    {
                        AspNetUserViewModel user = users.Where(X => X.Email == model.Email).First();

                        await registrationHelper.ForgotPassword(model);

                        model.EmailSent = true;


                    }
                    catch (Exception e)
                    {
                        model.Err = true;
                        return View(model);

                    }
                    

                    


                }

                return View(model);
            }

            return View(model);

        }

        [AllowAnonymous, HttpGet("ResetPassword")]
        public ActionResult ResetPassword(string uid, string token)
        {

            ResetPasswordModel model = new ResetPasswordModel
            {
                Token = token,
                UserId = uid,

            };

            return View(model);
        }

        [HttpPost("ResetPassword"), AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {

            if (ModelState.IsValid)
            {
                model.Token = model.Token.Replace(' ', '+');
                var result = await registrationHelper.ResetPassword(model);

                if (result == true)
                {
                    ModelState.Clear();

                    return View(model);

                }
                /*
              foreach(var error in result)
              {
                  ModelState.AddModelError("", error.Description);
              }*/

            }


            return View(model);
        }



        public ActionResult Seguros_List()
        {
            return View();
        }

        public ActionResult Seguros_List_Pers()
        {
            return View();
        }

        public ActionResult SeguroRT()
        {
            return View();
        }

        public ActionResult SeguroPatrimonial()
        {
            return View();
        }
        public ActionResult SeguroTransporte()
        {
            return View();
        }

        public ActionResult SeguroExpImp()
        {
            return View();
        }

        public ActionResult SeguroRespCivil()
        {
            return View();
        }

        public ActionResult SeguroSalud()
        {
            return View();
        }

        public ActionResult SeguroVivienda()
        {
            return View();
        }

        public ActionResult SeguroAuto()
        {
            return View();
        }

        public ActionResult SeguroViaje()
        {
            return View();
        }

        public ActionResult SeguroGastMed()
        {
            return View();
        }

        public ActionResult SeguroVida()
        {
            return View();
        }

        public ActionResult SeguroEstud()
        {
            return View();
        }

        public ActionResult Forms()
        {
            return View();
        }

        [SecurityModel]
        [HttpGet]
        public ActionResult SearchPolicy()
        {
            QPolicyViewModel qPolicyViewModel = new QPolicyViewModel();

            var clientList = aspNetUserRoleHelper.GetAll().Where(x => x.RoleId == "2").Count() ;
            var contracts = policyHelper.GetAll().Count();

            ViewBag.clients = clientList;
            ViewBag.contracts = contracts;

            return View(qPolicyViewModel);
        }




        public ActionResult aa1a()
        {
            QPolicyViewModel  a= new QPolicyViewModel();
            return View();
        }






        // GET: PolicyController/Delete/5
        [HttpGet]
        public async Task<IActionResult> PolicySearched(string id)
        {
            try
            {
                string userId = HttpContext.Session.GetString("Email");
                var qPolicy = await policyHelper.GetSeacrhedPolicy(id, userId);

                if (qPolicy.PolicyNumber != null)
                {

                    qPolicy.policyType = policyTypeHelper.Get(qPolicy.PolicyTypeId).PolicyName;
                    qPolicy.dniViewModels = listDni;
                    qPolicy.Found = true;
                    return PartialView("PolicySearched", qPolicy);
                }

                else
                {
                    QPolicyViewModel qPolicy2 = new QPolicyViewModel();
                    qPolicy2.Found = false;
                    qPolicy2.policyType = qPolicy.policyType;
                    return PartialView("PolicySearched", qPolicy2);

                }
            }
            catch (Exception e)
            {
                QPolicyViewModel qPolicy = new QPolicyViewModel();
                qPolicy.Found = false;
                qPolicy.policyType = e.Message;
                return PartialView("PolicySearched", qPolicy);

            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PolicySearched(QPolicyViewModel qPolicyViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    policyHelper.Create(qPolicyViewModel);

                    var newPolicy = new QPolicyViewModel();
                    newPolicy.Found = false;
                    return RedirectToAction("SearchPolicy", newPolicy);
                }
                else
                {
                    var newPolicy = new QPolicyViewModel();
                    newPolicy.Found = false;
                    return RedirectToAction("SearchPolicy", newPolicy);
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckPass(string password)
        {
            // Define your password requirements
            int lengthRequirement = 8;
            bool uppercaseRequirement = true;
            bool lowercaseRequirement = true;
            bool numberRequirement = true;

            // Perform password validation
            var requirements = new System.Collections.Generic.List<string>();

            // Check length
            if (password.Length < lengthRequirement)
            {
                requirements.Add("La contraseña debe tener al menos " + lengthRequirement + " caracteres de longitud.");
            }

            // Check for uppercase letters
            if (uppercaseRequirement && !Regex.IsMatch(password, "[A-Z]"))
            {
                requirements.Add("La contraseña debe contener al menos una letra mayúscula");
            }

            // Check for lowercase letters
            if (lowercaseRequirement && !Regex.IsMatch(password, "[a-z]"))
            {
                requirements.Add("La contraseña debe contener al menos una letra minúscula.");
            }

            // Check for numbers
            if (numberRequirement && !Regex.IsMatch(password, "\\d"))
            {
                requirements.Add("La contraseña debe contener al menos un número.");
            }

            return Content(string.Join("<br>", requirements));

        }
    }
}