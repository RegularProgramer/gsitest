using FrontendBlazor.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using System.Text;


namespace FrontendBlazor.Helpers
{
    public class RegistrationHelper
    {

        private ServiceRepository repository;
        public RegistrationHelper() { repository = new ServiceRepository(); }

        public bool Register(RegistrationViewModel user)
        {
            RegistrationViewModel registrationViewModel;

            if (user != null)
            {
                user.Id = "";
                user.NormalizedUserName = user.UserName;
                user.NormalizedEmail = user.Email;
                user.EmailConfirmed = true;
                user.SecurityStamp = "";
                user.ConcurrencyStamp = "";
                user.PhoneNumberConfirmed = true;
                user.TwoFactorEnabled = false;
                user.LockoutEnd = DateTime.UtcNow;
                user.LockoutEnabled = false;
                user.AccessFailedCount = 0;

                HttpResponseMessage message = repository.PostResponseJsonFormat("api/Account/Register", user);

                var content = message.Content.ReadAsStringAsync().Result;

                if (message.ReasonPhrase == "OK")
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        public bool Login(LoginViewModel user)
        {
            LoginViewModel loginViewModel;

            if (user.Email != null && user.PasswordHash != null)
            {
                HttpResponseMessage message = repository.PostResponseJsonFormat("api/Account/Login", user);

                var content = message.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);
                if (message.ReasonPhrase == "OK")
                {
                    return true;
                }
                return false;
            }
            else
            {
                throw new InvalidOperationException("Informacion invalida");
                return false;
            }
        }


        public async Task<bool> ForgotPassword(ForgotPasswordModel model)
        {


            if (model.Email != null || !model.Email.Equals(""))
            {
                HttpResponseMessage message = repository.PostResponseJsonFormat("api/Account/GenerateForgotEmailToken", model);

                var content = await message.Content.ReadAsStringAsync();

                if (message.ReasonPhrase == "OK")
                {
                    return true;

                }
                else
                {
                    throw new InvalidOperationException("Informacion invalida");

                }

            }



            return false;
        }


        public async Task<bool> ResetPassword(ResetPasswordModel model)
        {


            if (model != null)
            {
                HttpResponseMessage message = repository.PostResponseJsonFormat("api/Account/ResetPassword", model);

                var content = message.Content.ReadAsStringAsync();

                if (message.IsSuccessStatusCode)
                {
                    return true;

                }
                else
                {
                    throw new InvalidOperationException("Informacion invalida");

                }

            }



            return false;
        }





    }
}