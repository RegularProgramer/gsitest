using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class AspNetRoleHelper
    {
        private ServiceRepository repository;

        public AspNetRoleHelper()
        {
            repository = new ServiceRepository();
        }

        public List<AspNetRoleViewModel> GetAll()
        {
            List<AspNetRoleViewModel> list = new List<AspNetRoleViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/AspNetRole/Get");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<AspNetRoleViewModel>>(content);

                //Convert the object in the selected aspNetRole from the json string got from the backend


            }

            return list;

        }

        public AspNetRoleViewModel Get(int id)
        {
            AspNetRoleViewModel aspNetRole;


            HttpResponseMessage message = repository.GetResponse("api/AspNetRole/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetRole = JsonConvert.DeserializeObject<AspNetRoleViewModel>(content);

            //Convert the object in the selected user from the json string got from the backend




            return aspNetRole;

        }

        public AspNetRoleViewModel Create(AspNetRoleViewModel aspNetRole)
        {
            AspNetRoleViewModel aspNetRoleViewModel;


            HttpResponseMessage message = repository.PostResponse("api/AspNetRole/", aspNetRole);

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetRoleViewModel = JsonConvert.DeserializeObject<AspNetRoleViewModel>(content);

            //Convert the object in the selected aspNetRole from the json string got from the backend




            return aspNetRoleViewModel;

        }


        public AspNetRoleViewModel Update(AspNetRoleViewModel aspNetRole)
        {
            AspNetRoleViewModel aspNetRoleViewModel;


            HttpResponseMessage message = repository.PutResponse("api/AspNetRole/", aspNetRole);

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetRoleViewModel = JsonConvert.DeserializeObject<AspNetRoleViewModel>(content);

            //Convert the object in the selected aspNetRole from the json string got from the backend




            return aspNetRoleViewModel;

        }

        public AspNetRoleViewModel Delete(int id)
        {
            AspNetRoleViewModel aspNetRole;


            HttpResponseMessage message = repository.DeleteResponse("api/AspNetRole/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetRole = JsonConvert.DeserializeObject<AspNetRoleViewModel>(content);

            //Convert the object in the selected aspNetRole from the json string got from the backend




            return aspNetRole;

        }
    }
}
