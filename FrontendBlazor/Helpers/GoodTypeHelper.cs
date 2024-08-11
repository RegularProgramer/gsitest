using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class GoodTypeHelper
    {
       

            private ServiceRepository repository;

            public GoodTypeHelper()
            {
                repository = new ServiceRepository();
            }




            public List<GoodTypeViewModel> GetAll()
            {
                List<GoodTypeViewModel> list = new List<GoodTypeViewModel>(); //null


                HttpResponseMessage message = repository.GetResponse("api/GoodType");
                if (message != null)
                {
                    var content = message.Content.ReadAsStringAsync().Result;

                    list = JsonConvert.DeserializeObject<List<GoodTypeViewModel>>(content);

                    //Convert the object in the selected type from the json string got from the backend


                }

                return list;

            }

            public GoodTypeViewModel Get(int id)
            {
            GoodTypeViewModel good;


                HttpResponseMessage message = repository.GetResponse("api/GoodType/" + id.ToString());

                var content = message.Content.ReadAsStringAsync().Result;

                good = JsonConvert.DeserializeObject<GoodTypeViewModel>(content);

                //Convert the object in the selected type from the json string got from the backend




                return good;

            }

            public GoodTypeViewModel Create(GoodTypeViewModel good)
            {
            GoodTypeViewModel goodViewModel;


                HttpResponseMessage message = repository.PostResponse("api/GoodType/", good);

                var content = message.Content.ReadAsStringAsync().Result;

                goodViewModel = JsonConvert.DeserializeObject<GoodTypeViewModel>(content);

                //Convert the object in the selected type from the json string got from the backend




                return goodViewModel;

            }


            public GoodTypeViewModel Update(GoodTypeViewModel good)
            {
            GoodTypeViewModel goodViewModel;


                HttpResponseMessage message = repository.PutResponse("api/GoodType/", good);

                var content = message.Content.ReadAsStringAsync().Result;

                goodViewModel = JsonConvert.DeserializeObject<GoodTypeViewModel>(content);

                //Convert the object in the selected type from the json string got from the backend




                return goodViewModel;

            }

            public GoodTypeViewModel Delete(int id)
            {
            GoodTypeViewModel good;


                HttpResponseMessage message = repository.DeleteResponse("api/GoodType/" + id.ToString());

                var content = message.Content.ReadAsStringAsync().Result;

                good = JsonConvert.DeserializeObject<GoodTypeViewModel>(content);

                //Convert the object in the selected type from the json string got from the backend




                return good;

            }

        }
}
