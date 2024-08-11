using FrontendBlazor.Models;
using Newtonsoft.Json;

namespace FrontendBlazor.Helpers
{

    public class TicketHelper
    {
        private ServiceRepository repository;

        public TicketHelper()
        {
            repository = new ServiceRepository();
        }

        public List<TicketViewModel> GetAll()
        {
            List<TicketViewModel> list = new List<TicketViewModel>(); 


            HttpResponseMessage message = repository.GetResponse("api/Tickets");
            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                list = JsonConvert.DeserializeObject<List<TicketViewModel>>(content);

                //Convert the object in the selected type from the json string got from the backend


            }

            return list;

        }


        public TicketViewModel Get(int id)
        {

            TicketViewModel ticket = new TicketViewModel();
            HttpResponseMessage responseMessage = repository.GetResponse("api/Tickets/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                ticket = JsonConvert.DeserializeObject<TicketViewModel>(content);
            }
            return ticket;
        }


        public TicketViewModel Create(TicketViewModel ticketViewModel)
        {
            TicketViewModel ticket =  new TicketViewModel();
            HttpResponseMessage message = repository.PostResponse("api/Tickets", ticketViewModel);

            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;

                ticket = JsonConvert.DeserializeObject<TicketViewModel>(content)!;
            }

            return ticket;

        }

        public TicketViewModel Update(TicketViewModel ticket)
        {
            TicketViewModel ticketViewModel;

            HttpResponseMessage message = repository.PutResponse("api/Tickets/" + ticket.IdTicket, ticket);

            var content = message.Content.ReadAsStringAsync().Result;

            ticketViewModel = JsonConvert.DeserializeObject<TicketViewModel>(content);

            return ticketViewModel;
        }



        public void Delete(int id)
        {

            HttpResponseMessage message = repository.DeleteResponse("api/Tickets/" + id.ToString());

            if (message != null)
            {
                var content = message.Content.ReadAsStringAsync().Result;
            }

        }
    }
}
