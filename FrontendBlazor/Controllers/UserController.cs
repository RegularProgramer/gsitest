using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using NuGet.Common;
using Org.BouncyCastle.Asn1.Nist;
using System;
using System.Reflection;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class UserController : Controller
    {
        private UserHelper userHelper;
        private AspNetUserHelper aspNetUserHelper;
        private AspNetRoleHelper aspNetRoleHelper;
        private AspNetUserRoleHelper aspNetUserRoleHelper;
        private LogCambioHelper logCambioHelper;
        public UserController()
        {

            userHelper = new UserHelper();
            aspNetUserHelper = new AspNetUserHelper();
            aspNetRoleHelper = new AspNetRoleHelper();
            aspNetUserRoleHelper = new AspNetUserRoleHelper();
            logCambioHelper = new LogCambioHelper();

        }

        // GET: User
        public ActionResult UserManagement()
        {
            List<AspNetUserViewModel> users = aspNetUserHelper.GetAll();
            List<AspNetRoleViewModel> roles = aspNetRoleHelper.GetAll();
            List<AspNetUserRoleViewModel> userRoles = aspNetUserRoleHelper.GetAll();
            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;

            return View(users);
        }

        public ActionResult Polizas()
        {
            return View();
        }

        public ActionResult Comunicacion()
        {



            return View();

        }

        [HttpGet("Avisos") ]
        public ActionResult Avisos()
        {
            var list = aspNetUserHelper.GetAll();

            Console.WriteLine(list);

            MessageModel msg = new MessageModel();
            
            msg.aspNetUserViewModels = list;

            msg.Templates = userHelper.GetTemplates();
                
            
    
            return View(msg);
        }
        [HttpPost("Avisos")]
        public async Task<IActionResult> Avisos(MessageModel model )
            {
            var list = aspNetUserHelper.GetAll();

            model.aspNetUserViewModels = list;

            model.Templates = userHelper.GetTemplates();

            
            
            
            if (model != null)
            {
                model.PlaceHolders =
                  new List<KeyValuePair<string, string>>()
                    {
                    new KeyValuePair<string, string>("{{Content}}", model.Content)


                    };

            //    var address = new List<MailboxAddress>();

            //    address.AddRange(model.Address.Select(x => new MailboxAddress("email", x)));

             //   model.To = address;

       
            }
            else
            {

            }

            if (ModelState.IsValid)
            {

                var result = await userHelper.SendMessageBroadcast(model);

                if (result == true)
                {
                    ModelState.Clear();

                    return View(model);

                }
                else
                {

                    return View(result);
                }
                /*
              foreach(var error in result)
              {
                  ModelState.AddModelError("", error.Description);
              }*/

            }
            
            
            
            return View(model);
        }


        [HttpPost("GetPreview")]
        public ActionResult GetPreview(MessageModel model) {

            //Returns a MimeMessage
            var content =  userHelper.GetPreview(model);
          //model.ContentPreview = content.Result.HtmlBody().Result;
            return View(content);
        
        }


        public ActionResult Correo()
        {
            return View();
        }


        public ActionResult PolizaData()
        {
            return View();
        }

        public async Task<IActionResult> Logs()
        {
            List<LogViewModel> list = logCambioHelper.GetAllLogs();
            foreach( var item in list)
            {
                item.UserName = aspNetUserHelper.Get(item.userId).UserName;
            }
            return View(list);
        }

        public void generateLog(int id, string action)
        {
            var newLog = new LogViewModel
            {
                ActionMake = action,
                userId = HttpContext.Session.GetString("UserId"),
                modObject = id,
                TableReference = "ASPNETUSERS",
                LogTimestamp = DateTime.Now,
                IdUser = HttpContext.Session.GetString("UserId")

            };

            logCambioHelper.Create(newLog);

        }
    }
}
