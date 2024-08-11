using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using FrontendBlazor.Models;

namespace FrontendBlazor.Helpers
{
    public class ServiceRepository
    {
        public HttpClient Client { get; set; }

        public ServiceRepository()
        {
            Client = new HttpClient();
            //Client.BaseAddress = new Uri("http://www.gsibackend.somee.com/");
            Client.BaseAddress = new Uri("https://segurosbackendapp.azurewebsites.net");
            //Client.BaseAddress = new Uri("https://localhost:44322");
            Client.DefaultRequestHeaders.Add("ApiKey", "ec6f9d11");

        }
        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage PutResponse(string url, object model)
        {
            return Client.PutAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage PostResponseJsonFormat(string url, object model)
        {
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return Client.PostAsJsonAsync(url, model).Result;
        }

        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }

        public HttpResponseMessage GetByMailAndDateResponse(string clientMail, DateTime appointmentDate, int appointmentHour)
        {
            var parameters = new Dictionary<string, string>
    {
        { "clientMail", clientMail },
        { "appointmentDate", appointmentDate.ToString("yyyy-MM-dd") }, // Asegúrate de formatear correctamente la fecha
        {"appointmentHour", appointmentHour.ToString() }
    };

            var uri = "api/Appointment/GetByMailAndDate?" + string.Join("&", parameters.Select(kv => $"{kv.Key}={kv.Value}"));

            return Client.GetAsync(uri).Result;
        }



    }
}
