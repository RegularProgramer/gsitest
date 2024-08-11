using FrontendBlazor.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace FrontendBlazor.Helpers
{
    public class LoginHelper
    {
		
		private ServiceRepository repository;
		public LoginHelper() { repository = new ServiceRepository();}
		public LoginViewModel Authenticate(LoginViewModel login)
		{
			LoginViewModel loginViewModel;

			HttpResponseMessage message = repository.PostResponse("api/Account/Login", login);

			var content = message.Content.ReadAsStringAsync().Result;

			loginViewModel = JsonConvert.DeserializeObject<LoginViewModel>(content);

			//Convert the object in the selected type from the json string got from the backend



			return loginViewModel;

		}

        public AspNetUserViewModel Create(AspNetUserViewModel user)
        {
            AspNetUserViewModel aspNetUserViewModel;


            HttpResponseMessage message = repository.PostResponse("api/AspNetUser/", user);

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUserViewModel = JsonConvert.DeserializeObject<AspNetUserViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend

            return aspNetUserViewModel;

        }
    }
}
