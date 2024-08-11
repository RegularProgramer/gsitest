using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class AspNetUserHelper
    {
        private ServiceRepository repository;

        public AspNetUserHelper()
        {
            repository = new ServiceRepository();
        }

        public List<AspNetUserViewModel> GetAll()
        {
            List<AspNetUserViewModel> list = new List<AspNetUserViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/AspNetUser/Get");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<AspNetUserViewModel>>(content);

                //Convert the object in the selected aspNetUser from the json string got from the backend


            }

            return list;

        }

        public AspNetUserViewModel Get(string id)
        {
            AspNetUserViewModel aspNetUser;


            HttpResponseMessage message = repository.GetResponse("api/AspNetUser/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUser = JsonConvert.DeserializeObject<AspNetUserViewModel>(content);

            //Convert the object in the selected aspNetUser from the json string got from the backend


            return aspNetUser;

        }

        public AspNetUserViewModel Create(AspNetUserViewModel aspNetUser)
        {
            AspNetUserViewModel aspNetUserViewModel;


            HttpResponseMessage message = repository.PostResponse("api/AspNetUser/", aspNetUser);

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUserViewModel = JsonConvert.DeserializeObject<AspNetUserViewModel>(content);

            //Convert the object in the selected aspNetUser from the json string got from the backend




            return aspNetUserViewModel;

        }


        public AspNetUserViewModel Update(AspNetUserViewModel aspNetUser)
        {
            AspNetUserViewModel aspNetUserViewModel;


            HttpResponseMessage message = repository.PutResponse("api/AspNetUser/", aspNetUser);

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUserViewModel = JsonConvert.DeserializeObject<AspNetUserViewModel>(content);

            //Convert the object in the selected aspNetUser from the json string got from the backend




            return aspNetUserViewModel;

        }

        public AspNetUserViewModel Delete(int id)
        {
            AspNetUserViewModel aspNetUser;


            HttpResponseMessage message = repository.DeleteResponse("api/AspNetUser/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            aspNetUser = JsonConvert.DeserializeObject<AspNetUserViewModel>(content);

            //Convert the object in the selected aspNetUser from the json string got from the backend




            return aspNetUser;

        }
    }
}
