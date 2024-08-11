using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FrontendBlazor.Controllers
{
    public class TicketController : Controller
    {

        private TicketHelper ticketHelper;

        public TicketController()
        {
            ticketHelper = new TicketHelper();
        }


        //// GET: TicketController
        //[SecurityModel]
        //public ActionResult Tickets()
        //{
        //    List<TicketViewModel> tickets = ticketHelper.GetAll();
        //    return View(tickets);


        //}

        // GET: TicketController
        [SecurityModel]
        public ActionResult Tickets(string sortOrder)
        {
            // Obtener los tickets desde el helper
            List<TicketViewModel> tickets = ticketHelper.GetAll();

            // Configurar los parámetros de ordenación
            ViewBag.CurrentSort = sortOrder ?? "id_desc"; // Asegurar que el valor predeterminado es "id_desc"
            ViewBag.IdTicketSortParm = sortOrder == "id_asc" ? "id_desc" : "id_asc";
            ViewBag.MailSortParm = sortOrder == "mail" ? "mail_desc" : "mail";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.QuerySortParm = sortOrder == "query" ? "query_desc" : "query";
            ViewBag.TicketStateSortParm = sortOrder == "state" ? "state_desc" : "state";

            // Ordenar los tickets en función del parámetro de ordenación
            switch (sortOrder)
            {
                case "id_asc":
                    tickets = tickets.OrderBy(t => t.IdTicket).ToList();
                    break;
                case "mail":
                    tickets = tickets.OrderBy(t => t.Mail).ToList();
                    break;
                case "mail_desc":
                    tickets = tickets.OrderByDescending(t => t.Mail).ToList();
                    break;
                case "name":
                    tickets = tickets.OrderBy(t => t.Name).ToList();
                    break;
                case "name_desc":
                    tickets = tickets.OrderByDescending(t => t.Name).ToList();
                    break;
                case "query":
                    tickets = tickets.OrderBy(t => t.QUERY).ToList();
                    break;
                case "query_desc":
                    tickets = tickets.OrderByDescending(t => t.QUERY).ToList();
                    break;
                case "state":
                    tickets = tickets.OrderBy(t => t.TicketState).ToList();
                    break;
                case "state_desc":
                    tickets = tickets.OrderByDescending(t => t.TicketState).ToList();
                    break;
                default:
                    tickets = tickets.OrderByDescending(t => t.IdTicket).ToList(); // Por defecto ordena por IdTicket descendente
                    break;
            }

            return View(tickets);
        }


        // GET: TicketController/Create
        public IActionResult Create()
        {
            TicketViewModel model = new TicketViewModel();
            return View(model);
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel ticket)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ticketHelper.Create(ticket);
                    return Json(new { success = true });
                }
                else
                {
                    var errors = ModelState.Where(e => e.Value.Errors.Any())
                                           .ToDictionary(
                                                kvp => kvp.Key,
                                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                                            );

                    return Json(new { success = false, errors = errors });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        //[SecurityModel]
        //// GET: TicketsController/Details/5
        //public ActionResult Details(int id)
        //{

        //    TicketViewModel tickets = ticketHelper.Get(id);
        //    return View(tickets);
        //}

        [SecurityModel]
        [HttpGet]
        public JsonResult Details(int id)
        {
            TicketViewModel ticket = ticketHelper.Get(id);
            return Json(ticket);
        }


        //public ActionResult Details(int id)
        //{
        //    TicketViewModel ticket = ticketHelper.Get(id);
        //    return PartialView("_TicketDetails", ticket);
        //}


        [HttpPost]
        public JsonResult UpdateQueryStatus([FromBody] TicketViewModel ticket)
        {
            if (ticket == null)
            {
                return Json(new { success = false, error = "Ticket data is null." });
            }

            TicketViewModel updatedTicket = ticketHelper.Update(ticket);

            return Json(updatedTicket);
        }




        [SecurityModel]
        // GET: TicketController/DeleteConfirmation/5
        public ActionResult DeleteConfirmation(int id)
        {
            TicketViewModel ticket = ticketHelper.Get(id);
            return View(ticket);
        }


        [SecurityModel]
        // GET: TicketController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                ticketHelper.Delete(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
