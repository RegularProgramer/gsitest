using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class PolicyTypeHelper
    {
        private ServiceRepository repository;


        public PolicyTypeHelper()
        {
            repository = new ServiceRepository();
        }




        public List<PolicyTypeViewModel> GetAll()
        {
            List<PolicyTypeViewModel> list = new List<PolicyTypeViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/PolicyType");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<PolicyTypeViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public PolicyTypeViewModel Get(int id)
        {
            PolicyTypeViewModel type;


            HttpResponseMessage message = repository.GetResponse("api/PolicyType/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            type = JsonConvert.DeserializeObject<PolicyTypeViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return type;

        }

        public PolicyTypeViewModel Create(PolicyTypeViewModel policy)
        {
            PolicyTypeViewModel policyViewModel;


            HttpResponseMessage message = repository.PostResponse("api/PolicyType/", policy);

            var content = message.Content.ReadAsStringAsync().Result;

            policyViewModel = JsonConvert.DeserializeObject<PolicyTypeViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyViewModel;

        }


        public PolicyTypeViewModel Update(PolicyTypeViewModel policy)
        {
            PolicyTypeViewModel policyViewModel;


            HttpResponseMessage message = repository.PutResponse("api/PolicyType/", policy);

            var content = message.Content.ReadAsStringAsync().Result;

            policyViewModel = JsonConvert.DeserializeObject<PolicyTypeViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyViewModel;

        }

        public PolicyTypeViewModel Delete(int id)
        {
            PolicyTypeViewModel policy;


            HttpResponseMessage message = repository.DeleteResponse("api/PolicyType/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policy = JsonConvert.DeserializeObject<PolicyTypeViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policy;

        }


    }


}

