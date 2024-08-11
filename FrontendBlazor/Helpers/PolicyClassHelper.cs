using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class PolicyClassHelper
    {
        private ServiceRepository repository;


        public PolicyClassHelper()
        {
            repository = new ServiceRepository();
        }




        public List<PolicyClassViewModel> GetAll()
        {
            List<PolicyClassViewModel> list = new List<PolicyClassViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/PolicyClass");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<PolicyClassViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public PolicyClassViewModel Get(int id)
        {
            PolicyClassViewModel policyClass;


            HttpResponseMessage message = repository.GetResponse("api/PolicyClass/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policyClass = JsonConvert.DeserializeObject<PolicyClassViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyClass;

        }

        public PolicyClassViewModel Create(PolicyClassViewModel company)
        {
            PolicyClassViewModel classViewModel;


            HttpResponseMessage message = repository.PostResponse("api/PolicyClass/", company);

            var content = message.Content.ReadAsStringAsync().Result;

            classViewModel = JsonConvert.DeserializeObject<PolicyClassViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return classViewModel;

        }


        public PolicyClassViewModel Update(PolicyClassViewModel classPolicy)
        {
            PolicyClassViewModel classViewModel;


            HttpResponseMessage message = repository.PutResponse("api/PolicyClass/", classPolicy);

            var content = message.Content.ReadAsStringAsync().Result;

            classViewModel = JsonConvert.DeserializeObject<PolicyClassViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return classViewModel;

        }

        public PolicyClassViewModel Delete(int id)
        {
            PolicyClassViewModel policyClassViewModel;


            HttpResponseMessage message = repository.DeleteResponse("api/PolicyClass/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policyClassViewModel = JsonConvert.DeserializeObject<PolicyClassViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyClassViewModel;

        }


    }
}
