using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class PolicyHelper
    {
        private ServiceRepository repository;


        public PolicyHelper()
        {
            repository = new ServiceRepository();
        }



        //Basic Policy API CALLS
        
        public List<QPolicyViewModel> GetAll()
        {
            List<QPolicyViewModel> list = new List<QPolicyViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/Qpolicy");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<QPolicyViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public async Task<QPolicyViewModel> Get(int id)
        {
            QPolicyViewModel direccion;


            HttpResponseMessage message = repository.GetResponse("api/Qpolicy/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            direccion = JsonConvert.DeserializeObject<QPolicyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return direccion;

        }







        public  async Task<QPolicyViewModel> Create(QPolicyViewModel policy)
        {
            QPolicyViewModel policyViewModel;


            HttpResponseMessage message = repository.PostResponse("api/Qpolicy/", policy);

            var content = message.Content.ReadAsStringAsync().Result;

            policyViewModel = JsonConvert.DeserializeObject<QPolicyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policyViewModel;

        }


        public   QPolicyViewModel Update(QPolicyViewModel policy)
        {
            QPolicyViewModel policyViewModel;

            try
            {
                HttpResponseMessage message =  repository.PutResponse("api/Qpolicy/"+policy.IdPolicy, policy);

                var content = message.Content.ReadAsStringAsync().Result;

                policyViewModel = JsonConvert.DeserializeObject<QPolicyViewModel>(content);

                //Convert the object in the selected type from the json string got from the backend
            }catch(Exception e)
            {

                return new QPolicyViewModel();
            }



            return policyViewModel;

        }

        public QPolicyViewModel Delete(int id)
        {
            QPolicyViewModel policy;


            HttpResponseMessage message = repository.DeleteResponse("api/Qpolicy/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            policy = JsonConvert.DeserializeObject<QPolicyViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return policy;

        }


        //For Scrappings 
            
        public  async Task<QPolicyViewModel> GetSeacrhedPolicy(string id , string userId)
        {
            QPolicyViewModel policy = new QPolicyViewModel();

            try
            {

                HttpResponseMessage message = repository.GetResponse("api/Scrap/GetPolicyInfoJson/" + id.ToString()+"/"+userId.ToString());

                var content = message.Content.ReadAsStringAsync().Result;

                try
                {

                    policy = JsonConvert.DeserializeObject<QPolicyViewModel>(content);

                    //Convert the object in the selected type from the json string got from the backend
                }
                catch (Exception e)
                {
                    policy.policyType = content;

                    return policy;
                }



                return policy;
            }catch(Exception e)
            {
                policy.policyLine = e.Message;

                return policy;
            }
        }

        public string UpdatePolicy(int id)
        {
                
                

            HttpResponseMessage message = repository.GetResponse("api/GetPolicyInfoJson/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            string direccion = JsonConvert.DeserializeObject<string>(content);

            //Convert the object in the selected type from the json string got from the backend




            return direccion;

        }


        //DNI HELPERS 

        public List<DniViewModel> GetAllDni()
        {
            List<DniViewModel> list = new List<DniViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/Dni");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<DniViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public async Task<DniViewModel> GetDni(int id)
        {
            DniViewModel dni;


            HttpResponseMessage message = repository.GetResponse("api/Dni/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            dni = JsonConvert.DeserializeObject<DniViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return dni;

        }








    }


}

