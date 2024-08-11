using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class PolicyStateHelper
    {
        private ServiceRepository repository;


        public PolicyStateHelper()
        {
            repository = new ServiceRepository();
        }




        public List<PolicyStateViewModel> GetAll()
        {
            List<PolicyStateViewModel> list = new List<PolicyStateViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/PolicyState");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<PolicyStateViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public PolicyStateViewModel Get(int id)
        {
            PolicyStateViewModel policyState;


            HttpResponseMessage message = repository.GetResponse("api/policyState/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policyState = JsonConvert.DeserializeObject<PolicyStateViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyState;

        }

        public PolicyStateViewModel Create(PolicyStateViewModel policyState)
        {
            PolicyStateViewModel policyStateViewModel;


            HttpResponseMessage message = repository.PostResponse("api/policyState/", policyState);

            var content = message.Content.ReadAsStringAsync().Result;

            policyStateViewModel = JsonConvert.DeserializeObject<PolicyStateViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyStateViewModel;

        }


        public PolicyStateViewModel Update(PolicyStateViewModel policyState)
        {
            PolicyStateViewModel policyStateViewModel;


            HttpResponseMessage message = repository.PutResponse("api/policyState/", policyState);

            var content = message.Content.ReadAsStringAsync().Result;

            policyStateViewModel = JsonConvert.DeserializeObject<PolicyStateViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyStateViewModel;

        }

        public PolicyStateViewModel Delete(int id)
        {
            PolicyStateViewModel policyState;


            HttpResponseMessage message = repository.DeleteResponse("api/policyState/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policyState = JsonConvert.DeserializeObject<PolicyStateViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyState;

        }


    }


}

