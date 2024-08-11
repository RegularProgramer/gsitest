using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{
    public class LogCambioHelper
    {

        private ServiceRepository repository;


        public LogCambioHelper()
        {
            repository = new ServiceRepository();
        }




        public List<LogCambioViewModel> GetAll()
        {
            List<LogCambioViewModel> list = new List<LogCambioViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/LogCambio");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<LogCambioViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }


        public List<LogViewModel> GetAllLogs()
        {
            List<LogViewModel> list = new List<LogViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/Log");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<LogViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }





        public LogCambioViewModel Get(int id)
        {
            LogCambioViewModel log;


            HttpResponseMessage message = repository.GetResponse("api/LogCambio/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            log = JsonConvert.DeserializeObject<LogCambioViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return log;

        }

        public LogCambioViewModel Create(LogCambioViewModel log)
        {
            LogCambioViewModel logViewModel;


            HttpResponseMessage message = repository.PostResponse("api/LogCambio/", log);

            var content = message.Content.ReadAsStringAsync().Result;

            logViewModel = JsonConvert.DeserializeObject<LogCambioViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return logViewModel;

        }


        public LogViewModel Create(LogViewModel log)
        {
            LogViewModel logViewModel;


            HttpResponseMessage message = repository.PostResponse("api/Log/", log);

            var content = message.Content.ReadAsStringAsync().Result;

            logViewModel = JsonConvert.DeserializeObject<LogViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return logViewModel;

        }


        public LogCambioViewModel Update(LogCambioViewModel log)
        {
            LogCambioViewModel logViewModel;


            HttpResponseMessage message = repository.PutResponse("api/LogCambio/", log);

            var content = message.Content.ReadAsStringAsync().Result;

            logViewModel = JsonConvert.DeserializeObject<LogCambioViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return logViewModel;

        }

        public LogCambioViewModel Delete(int id)
        {
            LogCambioViewModel log;


            HttpResponseMessage message = repository.DeleteResponse("api/LogCambio/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            log = JsonConvert.DeserializeObject<LogCambioViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return log;

        }

    }
}
