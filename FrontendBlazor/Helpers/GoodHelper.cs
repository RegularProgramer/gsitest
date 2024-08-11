using FrontendBlazor.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace FrontendBlazor.Helpers
{
    public class GoodHelper
    {

        private ServiceRepository repository;

        public GoodHelper()
        {
            repository = new ServiceRepository();
        }




        public List<GoodViewModel> GetAll()
        {
            List<GoodViewModel> list = new List<GoodViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/Good");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<GoodViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public GoodViewModel Get(int id)
        {
            GoodViewModel good;


            HttpResponseMessage message = repository.GetResponse("api/Good/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            good = JsonConvert.DeserializeObject<GoodViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return good;

        }

        public GoodViewModel Create(GoodViewModel good)
        {
            GoodViewModel goodViewModel;


            HttpResponseMessage message = repository.PostResponse("api/Good/", good);

            var content = message.Content.ReadAsStringAsync().Result;

            goodViewModel = JsonConvert.DeserializeObject<GoodViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return goodViewModel;

        }


        public GoodViewModel Update(GoodViewModel good)
        {
            GoodViewModel goodViewModel;


            HttpResponseMessage message = repository.PutResponse("api/Good/", good);

            var content = message.Content.ReadAsStringAsync().Result;

            goodViewModel = JsonConvert.DeserializeObject<GoodViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return goodViewModel;

        }

        public GoodViewModel Delete(int id)
        {
            GoodViewModel good;


            HttpResponseMessage message = repository.DeleteResponse("api/Good/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            good = JsonConvert.DeserializeObject<GoodViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return good;

        }


    }
}
