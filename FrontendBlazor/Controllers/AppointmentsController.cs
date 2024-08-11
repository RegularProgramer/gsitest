using Frontend.Controllers;
using FrontendBlazor.Helpers;
using FrontendBlazor.Helpers.ServiceEmail;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Tls;
using System.Globalization;

namespace FrontendBlazor.Controllers
{
    public class AppointmentsController : Controller
    {
        private AppointmentHelper appointmentHelper;
        private readonly IEmailService emailService;

        public AppointmentsController(IEmailService _emailService)
        {
            appointmentHelper = new AppointmentHelper();
            emailService = _emailService;
        }

        IEnumerable<AppointmentSpace> dummyDataList = new List<AppointmentSpace>
{
    new AppointmentSpace
    {
        idAppointmentSpace = 1,
        appointmentDate = new DateTime(2024, 5, 1),
        Duration = 60,
        adminId = "admin123",
        Hour = 12,
        status = true,

    },
    new AppointmentSpace
    {
         idAppointmentSpace = 2,
        appointmentDate = new DateTime(2024, 5, 2),
        Duration = 30,
        adminId = "admin456",
        Hour = 9,
        status = false
    },
    new AppointmentSpace
    {
         idAppointmentSpace = 3,
        appointmentDate = new DateTime(2024, 5, 3),
        Duration = 45,
        adminId = "admin789",
        Hour = 15, 
        status = true
    },
    new AppointmentSpace
    {
         idAppointmentSpace = 4,
        appointmentDate = null,
        Duration = null,
        adminId = null,
        Hour = null,
        status = null
    },
    new AppointmentSpace
    {
         idAppointmentSpace = 5,
        appointmentDate = new DateTime(2024, 5, 5),
        Duration = 90,
        Hour =18,
        adminId = "admin101",
        status = false
    },
     new AppointmentSpace
    {
         idAppointmentSpace = 6,
        appointmentDate = new DateTime(2024, 5, 5),
        Duration = 90,
        Hour =20,
        adminId = "admin101",
        status = false
    },
        new AppointmentSpace
    {
         idAppointmentSpace = 6,
        appointmentDate = new DateTime(2024, 6, 2),
        Duration = 90,
        Hour =20,      
        adminId = "admin101",
        status = false
    }
};
        AppointmentSpaceViewModel schedule = new AppointmentSpaceViewModel
        {
            Duration = 10,
            IdAppointmentSpace = 0,
            IdUser = "",
            Schedule = "lunes,martes,miércoles,jueves,viernes"
        };




        // GET: AppointmentsController
        public ActionResult Index()
        {
            int count = 0;
            List <AppointmentDay> listDays = new List<AppointmentDay>();
            foreach(AppointmentSpace appointmentSpace in dummyDataList)
            {


                // Concatenar ":00" y agregar el sufijo correspondiente
                string hourWithSuffix;
                if (appointmentSpace.Hour < 12)
                {
                   
                        hourWithSuffix = $"{appointmentSpace.Hour}:00 am";
            
                    
                }
                else if (appointmentSpace.Hour == 12)
                {
                    hourWithSuffix = "12:00 md";
                }
                else
                {
                    hourWithSuffix = $"{appointmentSpace.Hour - 12}:00 pm";
                }

                // Asignar el valor calculado a una propiedad o utilizarlo como se necesite
                appointmentSpace.FormattedHour = hourWithSuffix;

                appointmentSpace.dayOfWeek = appointmentSpace.appointmentDate?.ToString("dddd" ,new  CultureInfo("es-CR")) ;
                if (appointmentSpace.DayId == null)
                {
                    try
                    {
                        appointmentSpace.DayId = listDays.Where(x => x.Date == appointmentSpace.appointmentDate).First().Id;
                        AppointmentDay appointmentDay = listDays.Where(x => x.Id == appointmentSpace.DayId).FirstOrDefault();
                        appointmentDay.Spaces.Add(appointmentSpace);
                    }
                    catch (Exception e)
                    {
                        listDays.Add(new AppointmentDay
                        {
                            Date = appointmentSpace.appointmentDate,
                            DayText = appointmentSpace.appointmentDate?.ToString("dddd", new CultureInfo("es-CR")),
                            Id = count,
                            Spaces = new List<AppointmentSpace> { appointmentSpace }


                        }); ;
                        count++;
                        
                        ;
                    }
                }
                else{

                    
                }

             }

            //return listDays
            //return View(dummyDataList);

            return View(listDays);
        }
        [Route("AppointmentsOptions")]
        public ActionResult AppointmentsOptions()
        {
            List<AppointmentDay> listDays = new List<AppointmentDay>();
            List<AppointmentViewModel> appointments = appointmentHelper.GetAll();
            string[] scheduleDays = schedule.Schedule.Split(',');
            DateTime today = DateTime.Now;
            DateTime limit  = today.AddDays(20);
            int start = 8;
            int end = 18;
            int count = 0;
            AppointmentDay actualDay = new AppointmentDay();
            while(count <= 20)
            {
                DateTime temp = today.AddDays(count);
                //Check if there are any appointments in this day
                List<int> verfHours = appointments
                             .Where(x => x.AppointmentDate.Date == temp.Date)
                             .Select(x => x.AppointmentHour)
                             .ToList();
                if (scheduleDays.Contains(temp.ToString("dddd", new CultureInfo("es-CR"))))
                {
                    List<AppointmentHour> hours = new List<AppointmentHour>();
                    for (int i = start; i <= end; i++) // Puede ser dependiendo de la hora y con un decimal, se le suma el duration
                    {
                        //Ignore if hour already exist
                        if (verfHours.Contains(i))
                        {
                            continue;
                        }
                        int hour = today.Hour;

                        if(i <= hour && today.Date == temp.Date)
                        {
                            continue;
                        }
                        // Concatenar ":00" y agregar el sufijo correspondiente
                        string hourWithSuffix;
                        if (i < 12)
                        {

                            hourWithSuffix = $"{i}:00 am";


                        }
                        else if (i == 12)
                        {
                            hourWithSuffix = "12:00 md";
                        }
                        else
                        {
                            hourWithSuffix = $"{i - 12}:00 pm";
                        }

                        // Asignar el valor calculado a una propiedad o utilizarlo como se necesite

                        hours.Add(new AppointmentHour
                        {
                            textHour = hourWithSuffix,
                            hour = i
                        });
                    }




                    actualDay = new AppointmentDay
                    {
                        Date = temp,
                        DayText = temp.ToString("dddd", new CultureInfo("es-CR")),
                        Id = count,
                        Hours = hours
                    };
                    if (actualDay.Hours.Count == 0)
                    {
                        count++;
                    }
                    else
                    {
                        listDays.Add(actualDay);
                        count++;
                        ;
                    }
                }
                else
                {
                    count++;
                    continue;
                    
                }

                
            }
                



            return View(listDays);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(DateTime date , int hour, string clientCell,string clientMail,string Comment, string Place )
        {
            try
            {
                var message =   appointmentHelper.Create( new AppointmentViewModel
                {
                    AppointmentDate = date,
                    AppointmentHour = hour,
                    ClientCell = clientCell,
                    ClientMail = clientMail,
                    IdUser = "5c79befe-be3b-44f5-8fed-e49526fed171",
                    StatusAppointmentId = 1,
                    Comment = Comment,
                    Place = Place   
                }
                ); ;
                return Ok(new { message });

            }
            catch(Exception ex)
            {
                return StatusCode(500, new { error = $"Error al obtener el mensaje de la póliza: {ex.Message}" });
            }
        }


        // GET: AppointmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AppointmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        //// GET: AppointmentsController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // GET: AppointmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                // Aquí deberías cargar los detalles de la cita con el ID proporcionado
                var appointment = appointmentHelper.Get(id);

                // Verificar si la cita existe
                if (appointment == null)
                {
                    // Si no se encuentra la cita, puedes redirigir a una página de error o mostrar un mensaje adecuado
                    return View("NotFound");
                }

                // Pasar los detalles de la cita a la vista
                return View(appointment);
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una respuesta adecuada
                return View("Error");
            }
        }


        // POST: AppointmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AppointmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AppointmentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult UpdateAppointmentStatus(string clientMail, DateTime appointmentDate, int appointmentHour)
        {
            try
            {
                var appointment = appointmentHelper.GetByMailAndDate(clientMail, appointmentDate, appointmentHour);

                if (appointment != null)
                {
                    // Log para verificar que la cita fue encontrada
                    Console.WriteLine($"Cita encontrada: Id={appointment.IdAppointment}");

                    appointment.StatusAppointmentId = 4; // Asumiendo que 4 es el ID para "Cancelado"
                    var updatedAppointment = appointmentHelper.Update(appointment);

                    if (updatedAppointment != null && updatedAppointment.StatusAppointmentId == 4)
                    {
                        return Json(new { success = true });
                    }
                    else
                    {
                        // Log para verificar que la actualización falló
                        Console.WriteLine("No se pudo actualizar la cita.");
                        return Json(new { success = false, errorMessage = "No se pudo actualizar la cita." });
                    }
                }
                else
                {
                    // Log para verificar que la cita no fue encontrada
                    Console.WriteLine("Cita no encontrada.");
                    return Json(new { success = false, errorMessage = "Cita no encontrada." });
                }
            }
            catch (Exception ex)
            {
                // Log para verificar el error
                Console.WriteLine($"Error: {ex.Message}");
                return Json(new { success = false, errorMessage = $"Error: {ex.Message}" });
            }
        }


        // GET: AppointmentsController/Calendar
        public ActionResult Calendar()
        {
            return View();
        }

        [HttpPost]
        public Task<IActionResult> AddAppointment(DateTime appointmentDate, int appointmentHour, string clientCell, string clientMail, string Comment , string Place)
        {
            try
            {
                appointmentHelper.Create(new AppointmentViewModel
                {
                    AppointmentDate = appointmentDate,
                    AppointmentHour = appointmentHour,
                    ClientCell = clientCell,
                    ClientMail = clientMail,
                    IdUser = "5c79befe-be3b-44f5-8fed-e49526fed171",
                    StatusAppointmentId = 1,
                    Comment =Comment,
                    Place = Place
                }
                );
                return Task.FromResult<IActionResult>(RedirectToAction("Calendar"));

            }
            catch (Exception ex)
            {
                return Task.FromResult<IActionResult>(StatusCode(500, new { error = $"Error al intentar crear el nuevo registro de la cita" }));
            }
        }

        [HttpPost]
        public Task<IActionResult> CancelAppointment(int IdAppointment)
        {
            try
            {
                var appointment = appointmentHelper.Get(IdAppointment); ;



                if (appointment != null)
                {

                    appointment.StatusAppointmentId = 4; 
                    var updatedAppointment = appointmentHelper.Update(appointment);

                    if (updatedAppointment != null && updatedAppointment.StatusAppointmentId == 4)
                    {
                        emailService.SendCancellationConfirmationEmail(appointment.ClientMail, appointment.AppointmentDate, appointment.AppointmentHour).Wait();
                        return Task.FromResult<IActionResult>(RedirectToAction("Calendar"));
                    }
                    else
                    {
                        return Task.FromResult<IActionResult>(StatusCode(500, new { error = $"No se pudo actualizar la cita." }));
                    }
                }
                else
                {
                    return Task.FromResult<IActionResult>(StatusCode(500, new { error = $"Cita no encontrada." }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Task.FromResult<IActionResult>(StatusCode(500, new { success = false, errorMessage = $"Error: {ex.Message}" }));
            }
        }

        [HttpPost]
        public Task<IActionResult> UpdateAppointment(AppointmentViewModel editedAppointment)
        {
            try
            {
                var appointment = appointmentHelper.Get(editedAppointment.IdAppointment); ;

                if (appointment != null)
                {

                    appointment.AppointmentDate = editedAppointment.AppointmentDate;
                    appointment.AppointmentHour = editedAppointment.AppointmentHour;
                    appointment.ClientCell = editedAppointment.ClientCell;
                    appointment.ClientMail = editedAppointment.ClientMail;
                    appointment.Comment = editedAppointment.Comment;
                    appointment.Place = editedAppointment.Place;
                    var updatedAppointment = appointmentHelper.Update(appointment);

                    if (updatedAppointment != null)
                    {
                        return Task.FromResult<IActionResult>(RedirectToAction("Calendar"));
                    }
                    else
                    {
                        return Task.FromResult<IActionResult>(StatusCode(500, new { error = $"No se pudo actualizar la cita." }));
                    }
                }
                else
                {
                    return Task.FromResult<IActionResult>(StatusCode(500, new { error = $"Cita no encontrada." }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return Task.FromResult<IActionResult>(StatusCode(500, new { success = false, errorMessage = $"Error: {ex.Message}" }));
            }
        }
    }
}
