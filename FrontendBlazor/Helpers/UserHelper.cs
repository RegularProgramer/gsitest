using FrontendBlazor.Models;
using MimeKit;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class UserHelper
    {
        private ServiceRepository repository;


        public UserHelper()
        {
            repository = new ServiceRepository();
        }

        public List<UserViewModel> GetAll()
        {
            List<UserViewModel> list = new List<UserViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/User");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<UserViewModel>>(content);

                //Convert the object in the selected user from the json string got from the backend


            }

            return list;

        }

        public List<TemplateViewModel> GetTemplates()
        {
            List<TemplateViewModel> list = new List<TemplateViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("/GetTemplates");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<TemplateViewModel>>(content);

                //Convert the object in the selected user from the json string got from the backend


            }

            return list;

        }

        public UserViewModel Get(int id)
        {
            UserViewModel user;


            HttpResponseMessage message = repository.GetResponse("api/User/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            user = JsonConvert.DeserializeObject<UserViewModel>(content);

            //Convert the object in the selected user from the json string got from the backend




            return user;

        }



        public async Task<qPolicyList> GetPolicy(int id)
        {
            qPolicyList qPolicyListViewModel = new qPolicyList(); ;


            HttpResponseMessage message =  repository.GetResponse("api/Scrap/GetPolicyClients/" + id.ToString());

            var content =  message.Content.ReadAsStringAsync().Result;

            qPolicyListViewModel.policyList = JsonConvert.DeserializeObject<List<ScrapQpolicyViewModel>>(content);

            //Convert the object in the selected user from the json string got from the backend

            return qPolicyListViewModel;

        }







        public UserViewModel Create(UserViewModel user)
        {
            UserViewModel userViewModel;


            HttpResponseMessage message = repository.PostResponse("api/User/", user);

            var content = message.Content.ReadAsStringAsync().Result;

            userViewModel = JsonConvert.DeserializeObject<UserViewModel>(content);

            //Convert the object in the selected user from the json string got from the backend




            return userViewModel;

        }


        public UserViewModel Update(UserViewModel user)
        {
            UserViewModel userViewModel;


            HttpResponseMessage message = repository.PutResponse("api/User/", user);

            var content = message.Content.ReadAsStringAsync().Result;

            userViewModel = JsonConvert.DeserializeObject<UserViewModel>(content);

            //Convert the object in the selected user from the json string got from the backend




            return userViewModel;

        }

        public UserViewModel Delete(int id)
        {
            UserViewModel user;


            HttpResponseMessage message = repository.DeleteResponse("api/User/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            user = JsonConvert.DeserializeObject<UserViewModel>(content);

            //Convert the object in the selected user from the json string got from the backend




            return user;

        }

        //SendMessageBoradcast

        public async Task<bool> SendMessageBroadcast(MessageModel model)
        {


            if (model != null)
            {
                HttpResponseMessage message = repository.PostResponseJsonFormat("api/Account/EmailBroadcast", model);

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




        public async Task<MimeMessage> GetPreview(MessageModel model)
        {


            if (model != null)
            {
                HttpResponseMessage message = repository.PostResponse("api/Account/GetPreview", model);

                var content = message.Content.ReadAsStringAsync().Result;

                if (message.IsSuccessStatusCode)
                {
                    

                    var res  = JsonConvert.DeserializeObject<MimeMessage>(content);


                    return res;

                }
                else
                {
                    throw new InvalidOperationException("Informacion invalida");

                }

            }



            return null ;
        }



    }
}
