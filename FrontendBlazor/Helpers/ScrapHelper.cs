using FrontendBlazor.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FrontendBlazor.Helpers
{
    public class ScrapHelper
    {
        private ServiceRepository repository;

        public ScrapHelper()
        {
            repository = new ServiceRepository();
        }

        public async Task<string> GetPolicyInfoMessage(string id,string userId)
        {
            HttpResponseMessage response =  repository.GetResponse("api/Scrap/GetPolicyInfo/" + id.ToString() + "/"+userId.ToString());

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                dynamic jsonData = JObject.Parse(content);
                string message = jsonData.message;
                return message;
            }
            else
            {
                throw new Exception("Error al obtener el mensaje de la poliza");
            }
        }
    }

}
