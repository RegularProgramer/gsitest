using FrontendBlazor.Models;
using MimeKit;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class AspNetUserRoleHelper
    {
        private ServiceRepository repository;

        public AspNetUserRoleHelper()
        {
            repository = new ServiceRepository();
        }

        public List<AspNetUserRoleViewModel> GetAll()
        {
            List<AspNetUserRoleViewModel> list = new List<AspNetUserRoleViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/AspNetUserRole/Get");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<AspNetUserRoleViewModel>>(content);

                //Convert the object in the selected aspNetUserRole from the json string got from the backend


            }

            return list;

        }

        public AspNetUserRoleViewModel Get(string id)
        {
            AspNetUserRoleViewModel aspNetUserRole;


            HttpResponseMessage message = repository.GetResponse("api/AspNetUserRole/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUserRole = JsonConvert.DeserializeObject<AspNetUserRoleViewModel>(content);

            //Convert the object in the selected aspNetUserRole from the json string got from the backend




            return aspNetUserRole;

        }
    }
}
