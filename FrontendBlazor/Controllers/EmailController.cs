using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using FrontendBlazor.Models;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;
using FrontendBlazor.Helpers;
using System.Security.Cryptography;
using System.Text;
using System.Globalization;
using FrontendBlazor.Helpers.ServiceEmail;

namespace Frontend.Controllers
{
    public class EmailController : Controller
    {

        private readonly IConfiguration _config;
        private AppointmentHelper _appointmentHelper;
        private readonly IEmailService emailService;

        public EmailController(IConfiguration config, IEmailService _emailService)
        {
            _config = config;
            _appointmentHelper = new AppointmentHelper();
            emailService = _emailService;
        }


        public IActionResult ConfirmEmailSuccess()
        {
            // Muestra la vista ConfirmEmailSuccess
            return View("ConfirmEmailSuccess");
        }

        public IActionResult ConfirmEmailFailure()
        {
            return View("Error");
        }

        //[HttpPost]
        //public async Task<IActionResult> SendEmail(AppointmentViewModel model)
        //{
        //    try
        //    {
        //        var smtpSettings = _config.GetSection("SmtpSettings");
        //        var smtpServer = smtpSettings["Server"];
        //        var smtpPort = int.Parse(smtpSettings["Port"]);
        //        var smtpUsername = smtpSettings["Username"];
        //        var smtpPassword = smtpSettings["Password"];

        //        // Lee la plantilla HTML desde un archivo
        //        string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailTemplates", "AppointmentConfirmation.html");
        //        string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

        //        string appointmentHour = model.AppointmentHour != null ? model.AppointmentHour.ToString() : "Hora no especificada";


        //        // Reemplaza los marcadores de posición con los valores reales
        //        emailBody = emailBody.Replace("{ClientMail}", model.ClientMail)
        //                             .Replace("{AppointmentDate}", model.AppointmentDate.ToString("dd/MM/yyyy"))
        //                             .Replace("{AppointmentHour}", appointmentHour);


        //        //string confirmationLink = $"{Request.Scheme}://{Request.Host}/Email/CancelAppointment/{appointmentId}";
        //        //emailBody = emailBody.Replace("{ConfirmationLink}", confirmationLink);

        //        //string editLink = $"{Request.Scheme}://{Request.Host}/Email/EditAppointment?clientMail={model.ClientMail}&appointmentDate={model.AppointmentDate:yyyy-MM-dd}";
        //        //emailBody = emailBody.Replace("{ConfirmationLink}", editLink);

        //        string cancelLink = $"{Request.Scheme}://{Request.Host}/Email/CancelAppointment?clientMail={model.ClientMail}&appointmentDate={model.AppointmentDate:yyyy-MM-dd}&appointmentHour={model.AppointmentHour}";
        //        emailBody = emailBody.Replace("{ConfirmationLink}", cancelLink);



        //        using (var client = new SmtpClient(smtpServer, smtpPort))
        //        {
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        //            client.EnableSsl = true;

        //            var message = new MailMessage();
        //            message.From = new MailAddress("correopruebadani1@gmail.com");
        //            //Correo del agente de seguros
        //            message.To.Add("daniel.rave0903@gmail.com");
        //            message.To.Add(model.ClientMail);
        //            message.Subject = "Nueva Cita Agendada";
        //            message.Body = emailBody;
        //            message.IsBodyHtml = true; // Indica que el cuerpo del mensaje es HTML

        //            await client.SendMailAsync(message);
        //        }

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción y devolver una respuesta adecuada
        //        // Puedes registrar el error para fines de diagnóstico
        //        // También puedes devolver una vista de error personalizada o un mensaje de error
        //        // Aquí un ejemplo básico de cómo devolver un mensaje de error

        //        return Json(new { success = false, errorMessage = "Ocurrió un error al enviar el correo electrónico. Por favor, inténtelo de nuevo más tarde." });
        //    }
        //}


        //[HttpGet]
        //public IActionResult CancelAppointment(string clientMail, DateTime appointmentDate, int appointmentHour)
        //{
        //    try
        //    {
        //        // Obtener la cita por correo y fecha
        //        var appointment = _appointmentHelper.GetByMailAndDate(clientMail, appointmentDate, appointmentHour);

        //        if (appointment != null)
        //        {
        //            // Actualizar el estado de la cita a "Cancelado" (IdAppointmentStatus = 4)
        //            appointment.StatusAppointmentId = 4;
        //            _appointmentHelper.Update(appointment);

        //            // Redirigir a una vista de confirmación o a la lista de citas
        //            return View();
        //        }
        //        else
        //        {
        //            // Si no se encuentra la cita, redirigir a una página de no encontrado
        //            return View("NotFound");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción y devolver una vista de error
        //        return View("Error");
        //    }
        //}

        //metodos para codificar la data del link
        private string GenerateSignedData(string data, string secretKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                string signature = Convert.ToBase64String(hash);
                string encodedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(data));
                return $"{encodedData}.{signature}";
            }
        }

        private (string data, bool isValid) VerifySignedData(string signedData, string secretKey)
        {
            var parts = signedData.Split('.');
            if (parts.Length != 2)
                return (null, false);

            string encodedData = parts[0];
            string signature = parts[1];

            string decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(encodedData));
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(decodedData));
                string computedSignature = Convert.ToBase64String(hash);
                return (decodedData, signature == computedSignature);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(AppointmentViewModel model)
        {
            try
            {
                // Configuración de SMTP
                var smtpSettings = _config.GetSection("SmtpSettings");
                var smtpServer = smtpSettings["Server"];
                var smtpPort = int.Parse(smtpSettings["Port"]);
                var smtpUsername = smtpSettings["Username"];
                var smtpPassword = smtpSettings["Password"];

                var secretKey = _config.GetValue<string>("AppSettings:SecretKey");

                // Generar datos y firmar
                string data = $"{model.ClientMail}|{model.AppointmentDate:yyyy-MM-dd}|{model.AppointmentHour}";
                string signedData = GenerateSignedData(data, secretKey);

                // Generar el enlace con los datos codificados y firmados
                string cancelLink = $"{Request.Scheme}://{Request.Host}/Email/CancelAppointment?data={WebUtility.UrlEncode(signedData)}";

                // Leer la plantilla HTML desde un archivo
                string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "emailTemplates", "AppointmentConfirmation.html");
                string emailBody = await System.IO.File.ReadAllTextAsync(templatePath);

                // Reemplazar los marcadores de posición con los valores reales
                string appointmentHour = model.AppointmentHour != null ? model.AppointmentHour.ToString() : "Hora no especificada";
                emailBody = emailBody.Replace("{ClientMail}", model.ClientMail)
                                     .Replace("{AppointmentDate}", model.AppointmentDate.ToString("yyyy-MM-dd"))
                                     .Replace("{AppointmentHour}", appointmentHour)
                                     .Replace("{AppointmentPlace}", model.Place)
                                     .Replace("{AppointmentComment}", model.Comment)
                                     .Replace("{ConfirmationLink}", cancelLink);

                // Configurar y enviar correo electrónico
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    client.EnableSsl = true;

                    var message = new MailMessage();
                    message.From = new MailAddress("correopruebadani1@gmail.com");
                    message.To.Add("daniel.rave0903@gmail.com");
                    message.To.Add(model.ClientMail);
                    message.Subject = "Nueva Cita Agendada";
                    message.Body = emailBody;
                    message.IsBodyHtml = true;

                    await client.SendMailAsync(message);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errorMessage = "Ocurrió un error al enviar el correo electrónico. Por favor, inténtelo de nuevo más tarde." });
            }
        }


        //[HttpGet]
        //public IActionResult CancelAppointment(string data)
        //{
        //    try
        //    {
        //        string secretKey = _config.GetValue<string>("AppSettings:SecretKey");
        //        var (decodedData, isValid) = VerifySignedData(data, secretKey);

        //        if (!isValid)
        //        {
        //            return BadRequest("Invalid signature.");
        //        }

        //        var parts = decodedData.Split('|');
        //        if (parts.Length != 3)
        //        {
        //            return BadRequest("Invalid data format.");
        //        }

        //        string clientMail = parts[0];
        //        DateTime appointmentDate;
        //        if (!DateTime.TryParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentDate))
        //        {
        //            return BadRequest("Invalid appointment date format.");
        //        }
        //        int appointmentHour;
        //        if (!int.TryParse(parts[2], out appointmentHour))
        //        {
        //            return BadRequest("Invalid appointment hour format.");
        //        }

        //        // Obtener la cita por correo y fecha
        //        var appointment = _appointmentHelper.GetByMailAndDate(clientMail, appointmentDate, appointmentHour);

        //        if (appointment != null)
        //        {
        //            // Actualizar el estado de la cita a "Cancelado" (IdAppointmentStatus = 4)
        //            appointment.StatusAppointmentId = 4;
        //            _appointmentHelper.Update(appointment);

        //            // Redirigir a una vista de confirmación o a la lista de citas
        //            return View();
        //        }
        //        else
        //        {
        //            // Si no se encuentra la cita, redirigir a una página de no encontrado
        //            return View("NotFound");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Manejar la excepción y devolver una vista de error
        //        return View("Error");
        //    }
        //}

        //Cancelar la cita y enviar un correo de confirmación de la cancelación
        [HttpGet]
        public async Task<IActionResult> CancelAppointment(string data)
        {
            try
            {
                string secretKey = _config.GetValue<string>("AppSettings:SecretKey");
                var (decodedData, isValid) = VerifySignedData(data, secretKey);

                if (!isValid)
                {
                    return BadRequest("Invalid signature.");
                }

                var parts = decodedData.Split('|');
                if (parts.Length != 3)
                {
                    return BadRequest("Invalid data format.");
                }

                string clientMail = parts[0];
                DateTime appointmentDate;
                if (!DateTime.TryParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out appointmentDate))
                {
                    return BadRequest("Invalid appointment date format.");
                }
                int appointmentHour;
                if (!int.TryParse(parts[2], out appointmentHour))
                {
                    return BadRequest("Invalid appointment hour format.");
                }

                // Obtener la cita por correo y fecha
                var appointment = _appointmentHelper.GetByMailAndDate(clientMail, appointmentDate, appointmentHour);

                if (appointment != null)
                {
                    // Actualizar el estado de la cita a "Cancelado" (IdAppointmentStatus = 4)
                    appointment.StatusAppointmentId = 4;
                    _appointmentHelper.Update(appointment);

                    // Enviar correo de confirmación de cancelación
                    await emailService.SendCancellationConfirmationEmail(clientMail, appointmentDate, appointmentHour);

                    // Redirigir a una vista de confirmación o a la lista de citas
                    return View();
                }
                else
                {
                    // Si no se encuentra la cita, redirigir a una página de no encontrado
                    return View("NotFound");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción y devolver una vista de error
                return View("Error");
            }
        }
    }
}
