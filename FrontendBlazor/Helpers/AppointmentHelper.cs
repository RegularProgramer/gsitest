using FrontendBlazor.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace FrontendBlazor.Helpers
{
    public class AppointmentHelper
    {

        private ServiceRepository repository;

        public AppointmentHelper()
        {
            repository = new ServiceRepository();
        }




        public List<AppointmentViewModel> GetAll()
        {
            List<AppointmentViewModel> list = new List<AppointmentViewModel>(); //null


            HttpResponseMessage message = repository.GetResponse("api/Appointment");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<AppointmentViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }

        public AppointmentViewModel Get(int id)
        {
            AppointmentViewModel Appointment;


            HttpResponseMessage message = repository.GetResponse("api/Appointment/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            Appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return Appointment;

        }

        public AppointmentViewModel Create(AppointmentViewModel Appointment)
        {
            AppointmentViewModel AppointmentViewModel;


            HttpResponseMessage message = repository.PostResponse("api/Appointment/", Appointment);

            var content = message.Content.ReadAsStringAsync().Result;

            AppointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return AppointmentViewModel;

        }


        public AppointmentViewModel Update(AppointmentViewModel Appointment)
        {
            AppointmentViewModel AppointmentViewModel;

            HttpResponseMessage message = repository.PutResponse("api/Appointment/{id}", Appointment);

            var content = message.Content.ReadAsStringAsync().Result;

            AppointmentViewModel = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend

            return AppointmentViewModel;

        }

        public AppointmentViewModel GetByMailAndDate(string clientMail, DateTime appointmentDate, int appointmentHour)
        {
            AppointmentViewModel appointment;

            HttpResponseMessage message = repository.GetByMailAndDateResponse(clientMail, appointmentDate, appointmentHour);

            var content = message.Content.ReadAsStringAsync().Result;

            appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            return appointment;
        }


        public AppointmentViewModel Delete(int id)
        {
            AppointmentViewModel Appointment;


            HttpResponseMessage message = repository.DeleteResponse("api/Appointment/" + id.ToString());

            var content = message.Content.ReadAsStringAsync().Result;

            Appointment = JsonConvert.DeserializeObject<AppointmentViewModel>(content);

            //Convert the object in the selected type from the json string got from the backend




            return Appointment;

        }


    }
}
